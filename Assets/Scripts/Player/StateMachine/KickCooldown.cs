using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickCooldown : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerAimScript = animator.gameObject.GetComponentInParent<PlayerAim>();
        _pistolLaser = animator.gameObject.GetComponentInChildren<LineRenderer>();
        _playerInputScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInput>();
        _playerMovementScript = animator.gameObject.GetComponentInParent<PlayerMovement>();
        _isKickingAnim = true;
        _playerInputScript.enabled = false;
        _playerMovementScript.enabled = false;
        _playerAimScript.enabled = false;
        _pistolLaser.enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerAimScript.enabled = true;
        _playerInputScript.enabled = true;
        _playerMovementScript.enabled = true;
        _pistolLaser.enabled = true;
        _isKickingAnim = false;
        _playerInputScript.IsKicking = false;
    }


    bool _isKickingAnim;
    PlayerAim _playerAimScript;
    LineRenderer _pistolLaser;
    PlayerInput _playerInputScript;
    PlayerMovement _playerMovementScript;

    public bool IsKickingAnim { get => _isKickingAnim; set => _isKickingAnim = value; }
}
