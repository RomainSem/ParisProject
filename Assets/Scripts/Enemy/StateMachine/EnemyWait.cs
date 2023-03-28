using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWait : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerDetectedScript = _player.GetComponent<PlayerDetected>();
        _enemyBehaviourScript = animator.gameObject.GetComponent<EnemyBehaviour>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_enemyBehaviourScript.IsEnemyRayHittingPlayer)
        {
            if (_playerDetectedScript != null)
            {
                animator.SetBool("IsPlayerDetected", _playerDetectedScript.IsDetectedByEnemy);
            }
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    PlayerDetected _playerDetectedScript;
    EnemyBehaviour _enemyBehaviourScript;
    GameObject _player;

}
