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
        _isKickingAnim = true;
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
        Debug.LogWarning("EXIT ANIMATOR");
        _playerAimScript.enabled = true;
        _pistolLaser.enabled = true;
        _isKickingAnim = false;
    }


    bool _isKickingAnim;
    PlayerAim _playerAimScript;
    LineRenderer _pistolLaser;

    public bool IsKickingAnim { get => _isKickingAnim; set => _isKickingAnim = value; }
}
