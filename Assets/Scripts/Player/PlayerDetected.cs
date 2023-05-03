using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetected : MonoBehaviour
{
    #region Exposed

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        if (_enemyBehaviourScript == null) return;
        _enemyBehaviourScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>();
    }

    private void Update()
    {
       
    }

    #endregion

    #region Methods

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("EnemyCone"))
    //    {
    //
    //        .Log("PLAYER IN CONE");
    //        //_isPlayerInCone = true;
    //        if (_enemyBehaviourScript.IsEnemyRayHittingPlayer)
    //        {
    //            IsDetectedByEnemy = true;
    //        }
    //        else
    //        {
    //            IsDetectedByEnemy = false;
    //        }
    //        Debug.Log(IsDetectedByEnemy);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy" ))
    //    {
    //        IsPlayerCloseToEnemy = true;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyLoot"))
        {
            _actualEnemyLoot = other.gameObject;
            _isPlayerInLootZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyLoot"))
        {
            _actualEnemyLoot = null;
            _isPlayerInLootZone = false;
        }
    }

    #endregion

    #region Private & Protected

    GameObject _actualEnemyLoot;
    bool _isPlayerInLootZone;
    bool _isDetectedByEnemy;
    bool _isPlayerCloseToEnemy;
    EnemyBehaviour _enemyBehaviourScript;

    public bool IsDetectedByEnemy { get => _isDetectedByEnemy; set => _isDetectedByEnemy = value; }
    public bool IsPlayerCloseToEnemy { get => _isPlayerCloseToEnemy; set => _isPlayerCloseToEnemy = value; }
    public bool IsPlayerInLootZone { get => _isPlayerInLootZone; set => _isPlayerInLootZone = value; }
    public GameObject ActualEnemyLoot { get => _actualEnemyLoot; set => _actualEnemyLoot = value; }

    #endregion
}
