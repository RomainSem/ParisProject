using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : StateMachineBehaviour
{

    InputReader _inputReader;
    PlayerMovement _playerMovement;
    PlayerAim _playerAim;
    Animator _animator;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _animator = animator;
        _inputReader = GameObject.Find("GameManager").GetComponent<InputReader>();
        _playerMovement = animator.GetComponentInParent<PlayerMovement>();
        _playerAim = animator.GetComponentInParent<PlayerAim>();
        _inputReader.RunStartEvent += _playerMovement.Run;
        _inputReader.AimEvent += _playerAim.Aim;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerMovement.Move(_playerMovement.WalkSpeed);
        UpdateAnimator();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _inputReader.RunStartEvent -= _playerMovement.Run;
        _inputReader.AimEvent -= _playerAim.Aim;
    }


    public void UpdateAnimator()
    {
        if (_inputReader.MovementValue.y == 0)
        {
            _animator.SetFloat("headingZ", 0);
        }
        else
        {
            float value = _inputReader.MovementValue.y > 0 ? 1f : -1f;
            _animator.SetFloat("headingZ", value);
        }


        if (_inputReader.MovementValue.x == 0)
        {
            _animator.SetFloat("headingX", 0);
        }
        else
        {
            float value = _inputReader.MovementValue.x > 0 ? 1f : -1f;
            _animator.SetFloat("headingX", value);
        }
    }
}
