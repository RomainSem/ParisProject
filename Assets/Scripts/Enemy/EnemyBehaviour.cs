using System.Collections;
using System.Collections.Generic;
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
        
    }

    private void FixedUpdate()
    {
        
    }

    #endregion

    #region Methods

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 directionToPlayer = _player.transform.position - transform.position;
        Gizmos.DrawLine(transform.position, transform.position + directionToPlayer);
    }

    #endregion

    #region Private & Protected


    #endregion
}
