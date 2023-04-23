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

    void Start()
    {
        t = 0;
        _currentHealth = _maxHealth;
        if (_enemyBehaviour == null) return;
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

        _lastDamageTime += Time.deltaTime;
    }

    #endregion

    #region Methods

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("EnemyPunchArm"))
        {
            LoseHP(_enemyBehaviour.Damage);
            if (_currentHealth <= 0)
            {
                IsPlayerDead = true;
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("EnemyPunchArm"))
        {
            if (_lastDamageTime >= 1f)
            {
                _lastDamageTime = 0f;
                StartCoroutine(LoseHPSlow(_enemyBehaviour.Damage));
            }
        }
    }

    private void LoseHP(float enemyDamage)
    {
        {
            _currentHealth -= enemyDamage;
        }
    }

    IEnumerator LoseHPSlow(float enemyDamage)
    {
        _currentHealth -= enemyDamage;
        if (_currentHealth <= 0)
        {
            IsPlayerDead = true;
        }
        yield return null;
    }

    #endregion

    #region Private & Protected

    bool _isPlayerDead;
    float t = 0;
    EnemyBehaviour _enemyBehaviour;
    float _previousHealth;
    float _lastDamageTime;

    public bool IsPlayerDead { get => _isPlayerDead; set => _isPlayerDead = value; }

    #endregion
}
