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
        _playerDetectedScript = _player.GetComponent<PlayerDetected>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.speed = 5f;
        _agent.SetDestination(_player.transform.position);
        if (Vector3.Distance(_enemy.transform.position, _player.transform.position) <= 3)
        {
            if (_playerDetectedScript != null)
            {
                _playerDetectedScript.IsPlayerCloseToEnemy = true;
            }
        }
        if (_playerDetectedScript != null)
        {
            animator.SetBool("IsPlayerCloseToEnemy", _playerDetectedScript.IsPlayerCloseToEnemy);
        }
    }


    GameObject _enemy;
    GameObject _player;
    NavMeshAgent _agent;
    PlayerDetected _playerDetectedScript;
}
