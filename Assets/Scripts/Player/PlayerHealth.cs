using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    #region Exposed

    [SerializeField] float _maxHealth = 10;
    [SerializeField] float _currentHealth;
    [SerializeField] Image _healthBarGreen;
    [SerializeField] Image _healthBarRed;


    #endregion

    #region Unity Lifecycle

    private void Awake()
    {

    }

    void Start()
    {
        t = 0;
        _currentHealth = _maxHealth;
        _enemyBehaviour = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>();
    }

    void Update()
    {
        if (_previousHealth != _currentHealth)
        {
            _previousHealth = Mathf.Lerp(_previousHealth, _currentHealth, t);
            t += 0.01f * Time.deltaTime;
        }
        else
        {
            t = 0;
        }

        _healthBarGreen.fillAmount = _currentHealth / _maxHealth;
        _healthBarRed.fillAmount = _previousHealth / _maxHealth;
    }

    #endregion

    #region Methods

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LoseHP(_enemyBehaviour.Damage);
            if (_currentHealth <= 0)
            {
                IsPlayerDead = true;
            }
        }
    }

    private void LoseHP(float enemyDamage)
    {
        _currentHealth -= enemyDamage;
    }

    #endregion

    #region Private & Protected

    bool _isPlayerDead;
    float t = 0;
    EnemyBehaviour _enemyBehaviour;
    float _previousHealth;

    public bool IsPlayerDead { get => _isPlayerDead; set => _isPlayerDead = value; }

    #endregion
}
