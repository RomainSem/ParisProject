using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWait : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _coneDetectionScript = animator.gameObject.GetComponentInChildren<ConeDetection>();
        _enemyBehaviourScript = animator.gameObject.GetComponent<EnemyBehaviour>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_enemyBehaviourScript.IsEnemyRayHittingPlayer)
        {
            if (_coneDetectionScript != null)
            {
                animator.SetBool("IsPlayerDetected", _coneDetectionScript.IsDetectedByEnemy);
            }
        }
    }


    ConeDetection _coneDetectionScript;
    EnemyBehaviour _enemyBehaviourScript;
    GameObject _player;

}
