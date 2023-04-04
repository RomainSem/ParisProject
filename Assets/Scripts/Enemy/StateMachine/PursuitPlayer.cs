using UnityEngine;
using UnityEngine.AI;

public class PursuitPlayer : StateMachineBehaviour
{

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = animator.gameObject;
        _agent = _enemy.GetComponent<NavMeshAgent>();
        _agent.isStopped = false;
        _enemyBehaviour = _enemy.GetComponent<EnemyBehaviour>();
       // _playerDetectedScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDetected>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.speed = 5f;
        _agent.SetDestination(_player.transform.position);
        Debug.Log(Vector3.Distance(_enemy.transform.position, _player.transform.position) <= 1);
        Debug.LogError(_enemyBehaviour.PlayerDetectedScript);
        if (Vector3.Distance(_enemy.transform.position, _player.transform.position) <= 1)
        {
            
            if (_enemyBehaviour.PlayerDetectedScript != null)
            {
                _enemyBehaviour.PlayerDetectedScript.IsPlayerCloseToEnemy = true;
            }
        }
        if (_enemyBehaviour.PlayerDetectedScript != null)
        {
            animator.SetBool("IsPlayerCloseToEnemy", _enemyBehaviour.PlayerDetectedScript.IsPlayerCloseToEnemy);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.velocity = Vector3.zero;
        _agent.isStopped = true;
    }


    GameObject _enemy;
    GameObject _player;
    NavMeshAgent _agent;
    EnemyBehaviour _enemyBehaviour;
}
