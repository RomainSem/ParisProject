using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    #region Exposed



    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMovScript = GetComponent<PlayerMovement>();
        _playerAimScript = GetComponent<PlayerAim>();
        if (_goToBedScript != null)
        {
            _goToBedScript = GameObject.FindGameObjectWithTag("Bed").GetComponent<GoToBedScript>();
        }
    }

    void Update()
    {
        _animator.SetFloat("moveSpeed", _playerMovScript.MoveSpeed);
        _animator.SetBool("IsAiming", _playerAimScript.IsAiming);
        if (_goToBedScript != null)
        {
            _animator.SetBool("IsSleeping", _goToBedScript.IsSleeping);
        }
        // transform the world forward into local space:
        if (_playerMovScript.MoveSpeed <= 0)
        {
            _animator.SetFloat("headingX", 0);
            _animator.SetFloat("headingZ", 0);
        }
        else
        {
            Vector3 relative = transform.InverseTransformDirection(_playerMovScript.Heading);
            _animator.SetFloat("headingX", relative.x);
            _animator.SetFloat("headingZ", relative.z);
        }
    }

    #endregion

    #region Methods

    #endregion

    #region Private & Protected

    Animator _animator;
    PlayerMovement _playerMovScript;
    PlayerAim _playerAimScript;
    GoToBedScript _goToBedScript;

    #endregion
}
