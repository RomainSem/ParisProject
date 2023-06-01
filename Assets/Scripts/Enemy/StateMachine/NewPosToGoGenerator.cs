using UnityEngine;
using UnityEngine.AI;

public class NewPosToGoGenerator : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = animator.gameObject;
        _coneDetection = _enemy.GetComponentInChildren<ConeDetection>();
        _randomPosScript = _enemy.transform.parent.Find("RandomPosInCircle").GetComponent<RandomPosInCircle>();
        _agent = _enemy.GetComponent<NavMeshAgent>();
        _enemyBehaviourScript = _enemy.GetComponent<EnemyBehaviour>();
        _animator = _enemy.GetComponent<Animator>();
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
    }

    //public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    _animator.SetBool("IsArrived", false);
    //}

    private void GoToRandomPosInCircle()
    {
        if (_randomPosScript.IsPosGenerated)
        {
            _agent.speed = 3f;
            _agent.SetDestination(_randomPosScript.RandomPos);
            if (Vector3.Distance(_agent.transform.position, _randomPosScript.RandomPos) <= 0.1f)
            {
                _randomPosScript.IsPosGenerated = false;
                _animator.SetBool("IsArrived", true);
            }
        }
    }


    NavMeshAgent _agent;
    GameObject _enemy;
    Animator _animator;

    ConeDetection _coneDetection;
    EnemyBehaviour _enemyBehaviourScript;
    RandomPosInCircle _randomPosScript;

}
