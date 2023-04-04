using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = animator.gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemyBehaviour = _enemy.GetComponent<EnemyBehaviour>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(_enemy.transform.position, _player.transform.position) <= 2)
        {
            Vector3 lookVector = _player.transform.position - _enemy.transform.position;
            lookVector.y = 0;
            //_enemy.transform.LookAt(lookVector);
            _enemy.transform.rotation = Quaternion.LookRotation(lookVector);
            //_enemy.transform.rotation = Quaternion.LookRotation(_player.transform.position - _enemy.transform.position);
            _enemyBehaviour.PlayerDetectedScript.IsPlayerCloseToEnemy = true;
        }
        else
        {
            _enemyBehaviour.PlayerDetectedScript.IsPlayerCloseToEnemy = false;
        }
        animator.SetBool("IsPlayerCloseToEnemy", _enemyBehaviour.PlayerDetectedScript.IsPlayerCloseToEnemy);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


    GameObject _enemy;
    GameObject _player;
    EnemyBehaviour _enemyBehaviour;
}
