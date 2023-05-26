using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    #region Exposed

    [SerializeField] GameObject _player;
    [SerializeField] int _health = 100;
    [SerializeField] byte _damage = 10;
    [SerializeField] byte _kickImpact = 10;
    [SerializeField] byte _bulletImpact = 6;
    [SerializeField] bool _isBusy;
    [SerializeField] PlayerDetected _playerDetectedScript;
    [SerializeField] GameObject _canBeLootedFX;


    #endregion

    #region Unity Lifecycle
    void Start()
    {
        NbEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _simpleShootScript = _player.GetComponentInChildren<SimpleShoot>();
        _agent = GetComponent<NavMeshAgent>();
        _coneDetectionScript = GetComponentInChildren<ConeDetection>();
        _animator = GetComponent<Animator>();
        _rgbd = GetComponent<Rigidbody>();
        _enemyLootScript = GetComponent<EnemyLoot>();
        _animator.SetBool("IsBusy", _isBusy);
    }

    void Update()
    {
        if (_isEnemyDead)
        {
            _canBeLootedFX.SetActive(true);
            //Destroy(gameObject, 60f);
            //_isEnemyDead = false;
        }
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
        transform.parent.Find("EnemyLoot").transform.position = new Vector3( gameObject.transform.position.x, gameObject.transform.position.y - 1 ,gameObject.transform.position.z);
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
                //Debug.Log("RAYCAST TRUE");
                IsEnemyRayHittingPlayer = true;
            }
            else
            {
                //Debug.Log("RAYCAST FALSE");
                IsEnemyRayHittingPlayer = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _isAttacked = true;
            if (_lastDamageTime >= 0.5f)
            {
                _lastDamageTime = 0f;
                StartCoroutine(LoseHPSlow(1));
            }
            _animator.SetTrigger("IsHit");
            _rgbd.AddForce(-transform.forward * _bulletImpact, ForceMode.Impulse);
            //Debug.Log("Enemy Health: " + _health);
            if (_health <= 0)
            {
                NbEnemies--;
                GetComponent<CapsuleCollider>().enabled = false;
                transform.parent.Find("EnemyLoot").GetComponent<BoxCollider>().enabled = true;
                //gameObject.layer = 11;
                _coneDetectionScript.gameObject.SetActive(false);
                _enemyLootScript.ReturnEnemyPossessedItems();
                _isEnemyDead = true;
            }
        }
    }

    IEnumerator LoseHPSlow(int enemyDamage)
    {
        _health -= _simpleShootScript.BulletDamage;
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
    SimpleShoot _simpleShootScript;
    ConeDetection _coneDetectionScript;
    EnemyLoot _enemyLootScript;
    Animator _animator;
    float _lastDamageTime;

    public bool IsEnemyRayHittingPlayer { get => _isEnemyRayHittingPlayer; set => _isEnemyRayHittingPlayer = value; }
    public int Health { get => _health; set => _health = value; }
    public int NbEnemies { get => _nbEnemies; set => _nbEnemies = value; }
    public byte Damage { get => _damage; set => _damage = value; }
    public PlayerDetected PlayerDetectedScript { get => _playerDetectedScript; set => _playerDetectedScript = value; }
    public bool IsEnemyDead { get => _isEnemyDead; set => _isEnemyDead = value; }

    #endregion
}
