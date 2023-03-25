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
        if (_goToBedScript != null)
        {
            _goToBedScript = GameObject.FindGameObjectWithTag("Bed").GetComponent<GoToBedScript>();
        }
    }

    void Update()
    {
        _animator.SetFloat("moveSpeed", _playerMovScript.MoveSpeed);
        if (_goToBedScript != null)
        {
            _animator.SetBool("IsSleeping", _goToBedScript.IsSleeping);
        }
    }

    #endregion

    #region Methods

    #endregion

    #region Private & Protected

    Animator _animator;
    PlayerMovement _playerMovScript;
    GoToBedScript _goToBedScript;

    #endregion
}
