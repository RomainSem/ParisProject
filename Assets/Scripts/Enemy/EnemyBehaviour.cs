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
    [SerializeField] byte _kickImpact = 10;
    [SerializeField] byte _bulletImpact = 6;
    [SerializeField] PlayerDetected _playerDetectedScript;


    #endregion

    #region Unity Lifecycle
    void Start()
    {
        NbEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _agent = GetComponent<NavMeshAgent>();
        _coneDetectionScript = GetComponentInChildren<ConeDetection>();
        _animator = GetComponent<Animator>();
        _rgbd = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!_isEnemyDead)
        {
            RaycastToPlayer();
        }
        if (_isAttacked)
        {
            _coneDetectionScript.IsDetectedByEnemy = true;
        }
        _lastDamageTime += Time.deltaTime;
        _animator.SetInteger("Health", _health);
    }

    private void FixedUpdate()
    {
        if (_agent.enabled == false)
        {
            if (_rgbd.velocity.magnitude < 0.1f)
            {
                _agent.enabled = true;
            }
        }
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
            Destroy(collision.gameObject);
            if (_lastDamageTime >= 0.5f)
            {
                _lastDamageTime = 0f;
                StartCoroutine(LoseHPSlow(1));
            }
            _animator.SetTrigger("IsHit");
            _rgbd.AddForce(-transform.forward * _bulletImpact, ForceMode.Impulse);
            Debug.Log("Enemy Health: " + _health);
            if (_health <= 0)
            {
                NbEnemies--;
                GetComponent<CapsuleCollider>().isTrigger = true;
                _coneDetectionScript.gameObject.SetActive(false);
                _isEnemyDead = true;
                //Destroy(gameObject);
            }
        }
    }

    IEnumerator LoseHPSlow(byte enemyDamage)
    {
        _health -= enemyDamage;
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerKickLeg"))
        {
            _agent.enabled = false;
            _rgbd.AddForce(-transform.forward * _kickImpact, ForceMode.Impulse);
            _animator.SetTrigger("IsKicked");
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
    bool _isEnemyDead;
    int _nbEnemies;
    Rigidbody _rgbd;
    NavMeshAgent _agent;
    ConeDetection _coneDetectionScript;
    Animator _animator;
    float _lastDamageTime;

    public bool IsEnemyRayHittingPlayer { get => _isEnemyRayHittingPlayer; set => _isEnemyRayHittingPlayer = value; }
    public byte Health { get => _health; set => _health = value; }
    public int NbEnemies { get => _nbEnemies; set => _nbEnemies = value; }
    public byte Damage { get => _damage; set => _damage = value; }
    public PlayerDetected PlayerDetectedScript { get => _playerDetectedScript; set => _playerDetectedScript = value; }
    public bool IsEnemyDead { get => _isEnemyDead; set => _isEnemyDead = value; }

    #endregion
}
