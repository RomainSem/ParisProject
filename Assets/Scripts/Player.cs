using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Exposed

    [SerializeField] float _moveSpeed = 1;


    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        _animator.SetBool("IsSleeping", false);
    }

    void Update()
    {

        Move();



    }


    #endregion

    #region Methods

    void Move()
    {
        _animator.SetFloat("moveSpeed", _moveSpeed);
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (direction != Vector3.zero)
        {
            Vector3 rightMovement = right * _moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * _moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
            transform.forward += heading;
            transform.position += rightMovement;
            transform.position += upMovement;
        }
        else
        {
            _animator.SetFloat("moveSpeed", 0f);
        }

    }

    

    #endregion

    #region Private & Protected

    Vector3 right;
    Vector3 forward;
    Animator _animator;

    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }


    #endregion
}
