using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewPosToGoGenerator : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemy = animator.gameObject;
        _playerDetectedScript = _player.GetComponent<PlayerDetected>();
        _randomPosScript = _enemy.transform.parent.GetComponentInChildren<RandomPosInCircle>();
        _agent = _enemy.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GoToRandomPosInCircle();
        RaycastHit hit;
        if (Physics.Raycast(_enemy.transform.position, _player.transform.position - _enemy.transform.position, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                IsEnemyRayHittingPlayer = true;
            }
            else
            {
                IsEnemyRayHittingPlayer = false;
            }
            if (_playerDetectedScript != null)
            {
                animator.SetBool("IsPlayerDetected", _playerDetectedScript.IsDetectedByEnemy);
            }
        }
    }

    private void GoToRandomPosInCircle()
    {
        if (_randomPosScript.IsPosGenerated)
        {
            _agent.SetDestination(_randomPosScript.RandomPos);
            if (Vector3.Distance(_enemy.transform.position, _randomPosScript.RandomPos) <= 1f)
            {
                _timer += Time.deltaTime;
                if (_timer >= 3)
                {
                    _randomPosScript.IsPosGenerated = false;
                    _timer = 0;
                }
            }
        }
    }



    NavMeshAgent _agent;

    GameObject _enemy;
    GameObject _player;

    float _timer = 0f;
    bool _isEnemyRayHittingPlayer;

    PlayerDetected _playerDetectedScript;
    RandomPosInCircle _randomPosScript;

    public bool IsEnemyRayHittingPlayer { get => _isEnemyRayHittingPlayer; set => _isEnemyRayHittingPlayer = value; }
}
