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
        _enemyBehaviourScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>();
    }

    #endregion

    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyCone")
        {
            _isPlayerInCone = true;
            Debug.Log("PLAYERDETECT : " + _enemyBehaviourScript.IsEnemyRayHittingPlayer);
            if (_enemyBehaviourScript.IsEnemyRayHittingPlayer)
            {
                IsDetectedByEnemy = true;
            }
            else
            {
                IsDetectedByEnemy = false;
            }
        }
    }

    #endregion

    #region Private & Protected

    bool _isPlayerInCone;
    bool _isDetectedByEnemy;
    bool _isPlayerCloseToEnemy;
    EnemyBehaviour _enemyBehaviourScript;

    public bool IsDetectedByEnemy { get => _isDetectedByEnemy; set => _isDetectedByEnemy = value; }
    public bool IsPlayerCloseToEnemy { get => _isPlayerCloseToEnemy; set => _isPlayerCloseToEnemy = value; }

    #endregion
}
