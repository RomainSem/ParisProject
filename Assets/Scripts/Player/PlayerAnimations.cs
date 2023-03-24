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
        _animator = GetComponent<Animator>();
        _playerMovScript = GetComponent<PlayerMovement>();
        _goToBedScript = GameObject.FindGameObjectWithTag("Bed").GetComponent<GoToBedScript>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        _animator.SetFloat("moveSpeed", _playerMovScript.MoveSpeed);
        _animator.SetBool("IsSleeping", _goToBedScript.IsSleeping);
    }

    private void FixedUpdate()
    {
        
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
