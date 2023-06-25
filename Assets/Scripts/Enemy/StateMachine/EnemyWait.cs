using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWait : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = animator.gameObject.GetComponent<NavMeshAgent>();
        _coneDetectionScript = animator.gameObject.GetComponentInChildren<ConeDetection>();
        _enemyBehaviourScript = animator.gameObject.GetComponent<EnemyBehaviour>();
        _agent.speed = 0f;
        animator.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_coneDetectionScript != null)
        {
            animator.SetBool("IsPlayerDetected", _coneDetectionScript.IsDetectedByEnemy);
        }
    }


    ConeDetection _coneDetectionScript;
    EnemyBehaviour _enemyBehaviourScript;
    GameObject _player;
    NavMeshAgent _agent;

}
