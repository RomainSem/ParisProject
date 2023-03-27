using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    #region Exposed

    [SerializeField] GameObject _player;


    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
    }

    void Start()
    {
        
    }

    void Update()
    {
        RaycastToPlayer();
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
                IsEnemyRayHittingPlayer = false;
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

    public bool IsEnemyRayHittingPlayer { get => _isEnemyRayHittingPlayer; set => _isEnemyRayHittingPlayer = value; }

    #endregion
}
