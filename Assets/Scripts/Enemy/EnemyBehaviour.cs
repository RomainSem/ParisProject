using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    #region Exposed

    [SerializeField] GameObject _player;
    [SerializeField] byte _health = 5;
    [SerializeField] byte _damage = 1;
    [SerializeField] PlayerDetected _playerDetectedScript;



    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
    }

    void Start()
    {
        NbEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _agent = GetComponent<NavMeshAgent>();
        _coneDetectionScript = GetComponentInChildren<ConeDetection>();
        _animator = GetComponent<Animator>();
        //_agent.updatePosition = false;
        //_agent.updateRotation = false;
    }

    void Update()
    {
        //Debug.Log(NbEnemies);
        RaycastToPlayer();
        if (_isAttacked)
        {
            _coneDetectionScript.IsDetectedByEnemy = true;
        }
    }

    private void FixedUpdate()
    {

    }

    #endregion

    #region Methods

    private void RaycastToPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, _player.transform.position - gameObject.transform.position, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("RAYCAST TRUE");
                IsEnemyRayHittingPlayer = true;
            }
            else
            {
                Debug.Log("RAYCAST FALSE");
                IsEnemyRayHittingPlayer = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _isAttacked = true;
            _health--;
            _animator.SetTrigger("IsHit");
            Debug.Log("Enemy Health: " + _health);
            if (_health <= 0)
            {
                NbEnemies--;
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 directionToPlayer = _player.transform.position - transform.position;
        Gizmos.DrawLine(transform.position, transform.position + directionToPlayer);
    }

    #endregion

    #region Private & Protected

    bool _isEnemyRayHittingPlayer;
    bool _isAttacked;
    int _nbEnemies;
    NavMeshAgent _agent;
    ConeDetection _coneDetectionScript;
    Animator _animator;

    public bool IsEnemyRayHittingPlayer { get => _isEnemyRayHittingPlayer; set => _isEnemyRayHittingPlayer = value; }
    public byte Health { get => _health; set => _health = value; }
    public int NbEnemies { get => _nbEnemies; set => _nbEnemies = value; }
    public byte Damage { get => _damage; set => _damage = value; }
    public PlayerDetected PlayerDetectedScript { get => _playerDetectedScript; set => _playerDetectedScript = value; }

    #endregion
}
