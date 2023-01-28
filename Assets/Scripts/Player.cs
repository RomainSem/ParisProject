using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    #region Exposed

    [SerializeField] float _moveSpeed = 1;
    [Range(1.1f, 5)]
    [SerializeField] float _runSpeed = 1.3f;

    // MouseClick
    //[SerializeField] InputAction _mouseClickAction;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        //_mainCamera = Camera.main;
        //_characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        //_rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //_newPosition = transform.position;
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Camera.main.transform.right;
        //right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        _animator.SetBool("IsSleeping", false);
    }

    void Update()
    {
        Move();
        // WalkToClick();
        Aim();



    }

    private void FixedUpdate()
    {

    }


    #endregion

    #region Methods

    void Move()
    {
        _animator.SetFloat("moveSpeed", _moveSpeed);
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction = Vector3.ClampMagnitude(direction, 1);
        if (direction != Vector3.zero)
        {
            Vector3 rightMovement = right * _moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * _moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
            transform.forward += heading;
            transform.position += rightMovement;
            transform.position += upMovement;
            if (Input.GetAxis("Fire3") == 1)
            {
                _animator.SetFloat("moveSpeed", _runSpeed);
            }
        }
        else
        {
            _animator.SetFloat("moveSpeed", 0f);
        }
    }

    void Aim()
    {
        if (Input.GetMouseButton(1))
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(cameraRay, out hit))
            {
                Vector3 PointToLookAt = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                Quaternion rotation = Quaternion.LookRotation(PointToLookAt - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime);
            }

        }
    }

    //void WalkToClick()
    //{
    //    float step = 0;

    //    // Move via le clic
    //    if (Input.GetMouseButtonDown(0))
    //    {

    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // le ray
    //        RaycastHit hit; // La collision du raycast

    //        if (Physics.Raycast(ray, out hit) /*&& hit.collider.tag == "Floor"*/)// si le rayon touche quelque chose on le stock dans hit
    //        {
    //            _newPosition = hit.point;
    //            transform.LookAt(new Vector3(_newPosition.x, transform.position.y, _newPosition.z));
    //            if (Time.timeSinceLevelLoad - _lastClickTime < _catchTime)
    //            {
    //                step = _runSpeed;
    //                _animator.SetFloat("moveSpeed", _runSpeed);
    //                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(_newPosition.x, transform.position.y, _newPosition.z), step);
    //            }
    //            else
    //            {
    //                step = _moveSpeed;
    //                _animator.SetFloat("moveSpeed", _moveSpeed);
    //            }
    //            Debug.Log(step);
    //        }
    //    }
    //    if (Vector3.Distance(transform.position, _newPosition) < 1)
    //    {
    //        _animator.SetFloat("moveSpeed", 0f);
    //    }
    //    transform.position = Vector3.MoveTowards(transform.position, new Vector3(_newPosition.x, transform.position.y, _newPosition.z), step);
    //}

    //void ClickToMove(InputAction.CallbackContext context)
    //{
    //    Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
    //    if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider)
    //    {
    //        if (_coroutine != null)
    //        {
    //            StopCoroutine(_coroutine);
    //        }
    //        _coroutine = StartCoroutine(PlayerMoveTowards(hit.point));
    //        _targetPosition= hit.point;
    //    }
    //}

    //private IEnumerator PlayerMoveTowards(Vector3 target)
    //{
    //    float playerDistanceToFloor = transform.position.y - target.y;
    //    target.y += playerDistanceToFloor;
    //    while (Vector3.Distance(transform.position, target) > 1f)
    //    {
    //        // Ignore les collisions apparemment
    //        Vector3 destination = Vector3.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);
    //        //transform.position = destination;

    //        // autre méthode :  Character Controller
    //        Vector3 direction = target - transform.position;
    //        Vector3 movement = direction.normalized * _moveSpeed * Time.deltaTime;
    //        _characterController.Move(movement);

    //        // autre méthode : Rigidbody
    //        //_rigidBody.velocity = direction.normalized * _moveSpeed;
    //        yield return null;
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(_targetPosition, 1);
    //}

    //private void OnEnable()
    //{
    //    _mouseClickAction.Enable();
    //    _mouseClickAction.performed += ClickToMove;
    //}

    //private void OnDisable()
    //{
    //    _mouseClickAction.performed -= ClickToMove;
    //    _mouseClickAction.Disable();
    //}



    #endregion

    #region Private & Protected


    Vector3 right;
    Vector3 forward;
    Animator _animator;
    int _rotationSpeed = 1000;

    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float RunSpeed { get => _runSpeed; set => _runSpeed = value; }

    // MouseClick
    //Vector3 _newPosition;
    //private Rigidbody _rigidBody;
    //float _lastClickTime;
    //float _catchTime = 0.25f;
    //Camera _mainCamera;
    //Coroutine _coroutine;
    //Vector3 _targetPosition;

    //CharacterController _characterController;

    #endregion
}
