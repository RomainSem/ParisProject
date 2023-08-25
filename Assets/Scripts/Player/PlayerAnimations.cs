using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    #region Exposed

    [SerializeField] EnemyInventory _enemyInventory;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMovScript = GetComponent<PlayerMovement>();
        _playerAimScript = GetComponent<PlayerAim>();
        _playerCoverScript = GetComponent<PlayerCover>();
        _playerInputScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInput>();
        _rgbd = GetComponent<Rigidbody>();
        if (_goToBedScript != null)
        {
            _goToBedScript = GameObject.FindGameObjectWithTag("Bed").GetComponent<GoToBedScript>();
        }
    }

    void Update()
    {
        if (_rgbd.velocity.magnitude <= 0.1f)
        {
            _animator.SetFloat("moveSpeed", 0);
        }
        else
        {
            _animator.SetFloat("moveSpeed", _playerMovScript.MoveSpeed);
        }
        //_animator.SetBool("IsAiming", _playerAimScript.IsAiming);
        _animator.SetBool("IsTakingCover", _playerCoverScript.IsTakingCover);
        if (_goToBedScript != null)
        {
            _animator.SetBool("IsSleeping", _goToBedScript.IsSleeping);
        }
        // transform the world forward into local space:
        if (_playerMovScript.MoveSpeed <= 0.1f)
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

        _animator.SetBool("IsKicking", _playerInputScript.IsKicking);

        if (_playerMovScript.IsMoving)
        {
            _animator.SetBool("IsLootingEnemy", false);
        }
        else
        {
            _animator.SetBool("IsLootingEnemy", _enemyInventory.IsLootMenuOpen);
        }
    }

    #endregion

    #region Methods

    #endregion

    #region Private & Protected

    Animator _animator;
    PlayerMovement _playerMovScript;
    PlayerAim _playerAimScript;
    PlayerInput _playerInputScript;
    PlayerCover _playerCoverScript;
    GoToBedScript _goToBedScript;
    Rigidbody _rgbd;


    #endregion
}
