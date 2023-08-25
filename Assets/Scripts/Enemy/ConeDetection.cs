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
        Debug.Log("RAY : " + _enemyBehaviourScript.IsEnemyRayHittingPlayer);
        Debug.Log("COLLIDER : " + IsDetectedByEnemy);
    }


    #endregion

    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_enemyBehaviourScript.IsEnemyRayHittingPlayer)
            {
                IsDetectedByEnemy = true;
            }
            //else
            //{
            //    IsDetectedByEnemy = false;
            //}
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (_enemyBehaviourScript.IsEnemyRayHittingPlayer)
    //        {
    //            IsDetectedByEnemy = true;
    //        }
    //        else
    //        {
    //            IsDetectedByEnemy = false;
    //        }
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                IsDetectedByEnemy = false;
        }
    }

    #endregion

    #region Private & Protected

    bool _isDetectedByEnemy;
    EnemyBehaviour _enemyBehaviourScript;

    public bool IsDetectedByEnemy { get => _isDetectedByEnemy; set => _isDetectedByEnemy = value; }


    #endregion
}
