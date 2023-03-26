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
        _player = GameObject.FindGameObjectWithTag("Player");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyCone")
        {
            _isPlayerInCone = true;
            if (_newPosToGoGeneratorScript.IsEnemyRayHittingPlayer)
            {
                IsDetectedByEnemy = true;
            }
        }
    }

    #endregion

    #region Private & Protected

    GameObject _player;
    bool _isPlayerInCone;
    bool _isDetectedByEnemy;
    NewPosToGoGenerator _newPosToGoGeneratorScript;

    public bool IsDetectedByEnemy { get => _isDetectedByEnemy; set => _isDetectedByEnemy = value; }

    #endregion
}
