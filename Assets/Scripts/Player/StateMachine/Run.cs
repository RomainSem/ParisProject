using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : StateMachineBehaviour
{
    InputReader _inputReader;
    PlayerMovement _playerMovement;
    Locomotion _locomotion;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _locomotion = animator.GetBehaviour<Locomotion>();
        _inputReader = GameObject.Find("GameManager").GetComponent<InputReader>();
        _playerMovement = animator.GetComponentInParent<PlayerMovement>();
        _inputReader.RunEndEvent += _playerMovement.RunEnd;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //_playerMovement.Run();
        _playerMovement.Move(_playerMovement.RunSpeed);
        _locomotion.UpdateAnimator();
    }



    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _inputReader.RunEndEvent -= _playerMovement.RunEnd;
    }


}
