using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    #region Exposed

    [SerializeField] int _maxHealth = 100;
    [SerializeField] int _currentHealth;
    [SerializeField] Image _healthBarGreen;
    [SerializeField] Image _healthBarRed;
    [SerializeField] TextMeshProUGUI _healthNbUI;


    #endregion

    #region Unity Lifecycle

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
        _healthNbUI.text = _currentHealth.ToString("0") + " / " + _maxHealth.ToString("0");

        _lastDamageTime += Time.deltaTime;

        if (_currentHealth <= 0)
        {
            IsPlayerDead = true;
        }
    }

    #endregion

    #region Methods

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("EnemyPunchArm"))
        {
            if (_lastDamageTime >= 1f)
            {
                _lastDamageTime = 0f;
                LoseHP(_enemyBehaviour.Damage);
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

    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    private void LoseHP(int enemyDamage)
    {
        {
            _currentHealth -= enemyDamage;
        }
    }

    IEnumerator LoseHPSlow(int enemyDamage)
    {
        _currentHealth -= enemyDamage;
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
