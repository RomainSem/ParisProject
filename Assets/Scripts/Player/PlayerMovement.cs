using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    #region Exposed

    [Range(1f, 1000f)]
    [SerializeField] float _walkSpeed = 1f;
    [Range(1f, 100f)]
    [SerializeField] float _runSpeed = 4f;
    [Range(1f, 500f)]
    [SerializeField] float _speed;


    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _rigidbdy = GetComponent<Rigidbody>();
        _playerAimScript = GetComponent<PlayerAim>();
    }

    void Start()
    {
        forward = Camera.main.transform.forward;
        //forward = Input.mousePosition;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void Update()
    {
        //Move();
        //_rigidbdy.velocity = Vector3.zero;
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            ComputeHeading();
            Move();
            if (!_playerAimScript.IsAiming)
            {
               // ROTATE
               Rotate();
            }
        }
        else
        {
            MoveSpeed = 0f;
        }
    }

    #endregion

    #region Methods

    void Move()
    {
        //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //if (direction != Vector3.zero && direction.magnitude > 0.1f)
        //{
        //Vector3 rightMovement = right * /*MoveSpeed * Time.fixedDeltaTime **/ Input.GetAxis("Horizontal");
        //Vector3 upMovement = forward * /*MoveSpeed * Time.fixedDeltaTime **/ Input.GetAxis("Vertical");
        //_heading = Vector3.Normalize(rightMovement + upMovement);
        //transform.forward = _heading;
        if (Input.GetAxis("Fire3") == 1 && !_playerAimScript.IsAiming)
        {
            IsRunning = true;
            MoveSpeed = _runSpeed;
        }
        else
        {
            IsRunning = false;
            MoveSpeed = _walkSpeed;
        }
        //_rigidbdy.AddForce((upMovement + rightMovement) * MoveSpeed, ForceMode.Force);
        if (_heading.magnitude >= 0.1f)
        {
            //_rigidbdy.MovePosition(transform.position += _heading * MoveSpeed * Time.fixedDeltaTime);
            // TEST ADDFORCE MODE VELOCITY
            _rigidbdy.velocity = _heading * MoveSpeed /** Time.fixedDeltaTime*/;
        }
        else
        {
            _rigidbdy.velocity = Vector3.zero;
        }
        //}
    }

    void ComputeHeading()
    {
        _heading.x = Vector3.Project(PlayerInput._leftStickDirection, forward).x;
        _heading.z = Vector3.Project(PlayerInput._leftStickDirection, right).z;
        _heading.y = 0;
        _heading = _heading.normalized;
    }

    void Rotate()
    {
        transform.forward = _heading;
    }


    #endregion

    #region Private & Protected

    Rigidbody _rigidbdy;
    Vector3 right;
    Vector3 forward;
    bool _isRunning;
    bool _isMoving;
    PlayerAim _playerAimScript;
    Vector3 _heading;

    public float MoveSpeed { get => _speed; set => _speed = value; }
    public bool IsRunning { get => _isRunning; set => _isRunning = value; }
    public Vector3 Heading { get => _heading; set => _heading = value; }

    #endregion
}
