using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Exposed

    [Range(1f, 10f)]
    [SerializeField] float _walkSpeed = 1f;
    [Range(1f, 50f)]
    [SerializeField] float _runSpeed = 4f;


    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _rigidbdy = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Camera.main.transform.right;
        //right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        _animator.SetBool("IsSleeping", false);
    }

    void Update()
    {
        _rigidbdy.velocity = Vector3.zero;
        Move();
        Aim();




    }

    private void FixedUpdate()
    {

    }


    #endregion

    #region Methods

    void Move()
    {
        _animator.SetFloat("moveSpeed", _walkSpeed);
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //direction = direction.normalized * Time.deltaTime;
        if (direction != Vector3.zero)
        {

            if (Input.GetAxis("Fire3") == 1)
            {
                Run();
            }
            else
            {
                Walk();
            }
        }
        else
        {
            _animator.SetFloat("moveSpeed", 0f);
        }
    }

    void Walk()
    {
        Vector3 rightMovement = right * _walkSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * _walkSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward += heading;
        transform.position += rightMovement;
        transform.position += upMovement;
        Debug.Log(heading);
    }

    void Run()
    {
        _animator.SetFloat("moveSpeed", _runSpeed);
        Vector3 rightMovement = right * _runSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * _runSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward += heading;
        transform.position += rightMovement;
        transform.position += upMovement;
        Debug.Log(heading);
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

    #endregion

    #region Private & Protected

    Rigidbody _rigidbdy;
    Vector3 right;
    Vector3 forward;
    Animator _animator;
    int _rotationSpeed = 1000;

    public float MoveSpeed { get => _walkSpeed; set => _walkSpeed = value; }
    public float RunSpeed { get => _runSpeed; set => _runSpeed = value; }

    

    #endregion
}
