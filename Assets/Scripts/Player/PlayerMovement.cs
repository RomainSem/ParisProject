using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Exposed

    [Range(1f, 10f)]
    [SerializeField] float _walkSpeed = 1f;
    [Range(1f, 10f)]
    [SerializeField] float _runSpeed = 4f;
    [Range(1f, 50f)]
    [SerializeField] float _speed;


    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _rigidbdy = GetComponent<Rigidbody>();
    }

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void Update()
    {
        Move();
        _rigidbdy.velocity = Vector3.zero;
    }

    #endregion

    #region Methods

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (direction != Vector3.zero && direction.magnitude > 0.1f)
        {
            Vector3 rightMovement = right * MoveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * MoveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
            transform.forward = heading;
            _rigidbdy.MovePosition(transform.position += heading * MoveSpeed * Time.deltaTime);

            if (Input.GetAxis("Fire3") == 1)
            {
                MoveSpeed = _runSpeed;
            }
            else
            {
                MoveSpeed = _walkSpeed;
            }
        }
        else
        {
            MoveSpeed = 0f;
        }
    }

    #endregion

    #region Private & Protected

    Rigidbody _rigidbdy;
    Vector3 right;
    Vector3 forward;

    public float MoveSpeed { get => _speed; set => _speed = value; }

    #endregion
}
