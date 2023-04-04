using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _losePanel;
    [SerializeField] GameObject _winPanel;
    [SerializeField] Image[] _nbBullets;
    [SerializeField] GameObject _player;

    private void Awake()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Start()
    {
        _enemyBehaviourScript = _enemy.GetComponent<EnemyBehaviour>();
        _playerHealthScript = _player.GetComponent<PlayerHealth>();
        _simpleShootScript = _player.GetComponentInChildren<SimpleShoot>();

    #if DEVELOPMENT_BUILD
        Debug.LogError("Force the build console open...");

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
        UpdateNbBullets();

        if (_playerHealthScript.IsPlayerDead)
        {
            _losePanel.SetActive(true);
        }

        if (_enemyBehaviourScript.NbEnemies <= 0)
        {
            _winPanel.SetActive(true);
        }
    }

    private void UpdateNbBullets() 
    {
        for (int i = 0; i < _nbBullets.Length; i++)
        {
            if (i < _simpleShootScript.CurrentNbBullets)
            {
                _nbBullets[i].enabled = true;
            }
            else
            {
                _nbBullets[i].enabled = false;
            }
        }
    }

    PlayerHealth _playerHealthScript;
    EnemyBehaviour _enemyBehaviourScript;
    GameObject _enemy;
    SimpleShoot _simpleShootScript;
}
