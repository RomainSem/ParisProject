using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _losePanel;
    [SerializeField] GameObject _winPanel;
    [SerializeField] GameObject _player;

    private void Awake()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
        //_player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        _enemyBehaviourScript = _enemy.GetComponent<EnemyBehaviour>();
        _playerHealthScript = _player.GetComponent<PlayerHealth>();
        //Debug.Break();
        //Debug.LogError("Force the build console open...");

    #if DEVELOPMENT_BUILD

        if (_playerHealthScript == null)
	    {
            Debug.Log("PLAYER HEALTH IS NULL");
	    }
    #endif

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (_playerHealthScript.IsPlayerDead)
        {
            _losePanel.SetActive(true);
        }

        if (_enemyBehaviourScript.NbEnemies <= 0)
        {
            _winPanel.SetActive(true);
        }
    }


    PlayerHealth _playerHealthScript;
    EnemyBehaviour _enemyBehaviourScript;
    GameObject _enemy;
}
