using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursuitPlayer : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = animator.gameObject;
        _agent = _enemy.GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_player.transform.position);
        if (Vector3.Distance(_enemy.transform.position, _player.transform.position) <= 1)
        {
            _playerDetectedScript.IsPlayerCloseToEnemy = true;
        }
        animator.SetBool("IsPlayerCloseToEnemy", _playerDetectedScript.IsPlayerCloseToEnemy);
    }


    GameObject _enemy;
    GameObject _player;
    NavMeshAgent _agent;
    PlayerDetected _playerDetectedScript;
}