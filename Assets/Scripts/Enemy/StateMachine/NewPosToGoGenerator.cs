using UnityEngine;
using UnityEngine.AI;

public class NewPosToGoGenerator : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemy = animator.gameObject;
        _coneDetection = _enemy.GetComponentInChildren<ConeDetection>();
        _randomPosScript = _enemy.transform.parent.GetComponentInChildren<RandomPosInCircle>();
        _agent = _enemy.GetComponent<NavMeshAgent>();
        _enemyBehaviourScript = _enemy.GetComponent<EnemyBehaviour>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GoToRandomPosInCircle();
        if (_enemyBehaviourScript.IsEnemyRayHittingPlayer)
        {
            if (_coneDetection != null)
            {
                animator.SetBool("IsPlayerDetected", _coneDetection.IsDetectedByEnemy);
            }
        }
        animator.SetBool("IsGoneToRandomPos", true);
    }

    private void GoToRandomPosInCircle()
    {
        if (_randomPosScript.IsPosGenerated)
        {
            _agent.SetDestination(_randomPosScript.RandomPos);
            if (Vector3.Distance(_enemy.transform.position, _randomPosScript.RandomPos) <= 1f)
            {
                    _randomPosScript.IsPosGenerated = false;
            }
        }
    }


    NavMeshAgent _agent;

    GameObject _enemy;
    GameObject _player;

    ConeDetection _coneDetection;
    EnemyBehaviour _enemyBehaviourScript;
    RandomPosInCircle _randomPosScript;

}
