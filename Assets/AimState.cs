using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : StateMachineBehaviour
{
    PlayerMovement _playerMovement;
    PlayerAim _playerAim;
    InputReader _inputReader;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _inputReader = GameObject.Find("GameManager").GetComponent<InputReader>();
        _playerMovement = animator.GetComponentInParent<PlayerMovement>();
        _playerAim = animator.GetComponentInParent<PlayerAim>();
        _inputReader.AimEvent += _playerAim.AimEnd;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerMovement.Move(_playerMovement.WalkSpeed);
        _playerAim.Aiming();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _inputReader.AimEvent -= _playerAim.AimEnd;

    }


}
