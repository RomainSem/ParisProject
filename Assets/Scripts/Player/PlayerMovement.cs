using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    #region Exposed

    [Range(0f, 1000f)]
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
        _playerCoverScript = GetComponent<PlayerCover>();
        _animator = GetComponentInChildren<Animator>();
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
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }

    #endregion

    #region Methods

    public void Move(float speed)
    {
        ComputeHeading();
        Rotate();
        if (_heading.magnitude >= 0.1f)
        {
            _heading.Normalize();
            _rigidbdy.AddForce(_heading * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        else
        {
            _rigidbdy.velocity = Vector3.zero;
        }
    }

    public void Run()
    {
        _animator.SetBool("IsRunning", true);
    }

    public void RunEnd()
    {
          _animator.SetBool("IsRunning", false);
    }

    public void ComputeHeading()
    {
        _heading.x = Vector3.Project(PlayerInput._leftStickDirection, forward).x;
        _heading.z = Vector3.Project(PlayerInput._leftStickDirection, right).z;
        _heading.y = 0;
        _heading = _heading.normalized;
    }

    public void Rotate()
    {
        if (_playerCoverScript.IsTakingCover || _heading.magnitude < 0.1f) return;
        transform.forward = _heading;
    }


    #endregion

    #region Private & Protected

    Rigidbody _rigidbdy;
    Vector3 right;
    Vector3 forward;
    bool _isMoving;
    bool _isRunning;
    PlayerAim _playerAimScript;
    PlayerCover _playerCoverScript;
    Vector3 _heading;
    Animator _animator;

    public float MoveSpeed { get => _speed; set => _speed = value; }
    public Vector3 Heading { get => _heading; set => _heading = value; }
    public bool IsMoving { get => _isMoving; set => _isMoving = value; }
    public float WalkSpeed { get => _walkSpeed; }
    public float RunSpeed { get => _runSpeed; }
    public bool IsRunning { get => _isRunning; private set => _isRunning = value; }

    #endregion
}
