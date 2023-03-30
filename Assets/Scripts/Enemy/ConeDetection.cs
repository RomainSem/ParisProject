using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeDetection : MonoBehaviour
{
    #region Expose

    
    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        
    }

    void Start()
    {
        _enemyBehaviourScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>();
    }

    void Update()
    {
        
    }


    #endregion

    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            //Debug.LogError("COLLIDE PLAYER");
            //_isPlayerInCone = true;
            if (_enemyBehaviourScript.IsEnemyRayHittingPlayer)
            {
                IsDetectedByEnemy = true;
            }
            else
            {
                IsDetectedByEnemy = false;
            }
            //Debug.Log(IsDetectedByEnemy);
        }
    }

    #endregion

    #region Private & Protected

    bool _isDetectedByEnemy;
    EnemyBehaviour _enemyBehaviourScript;

    public bool IsDetectedByEnemy { get => _isDetectedByEnemy; set => _isDetectedByEnemy = value; }


    #endregion
}
