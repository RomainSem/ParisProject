using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    #region Exposed

    [SerializeField] float _maxHealth = 100;
    [SerializeField] float _currentHealth;
    [SerializeField] Image _healthBarRed;
    [SerializeField] Image _healthBarBlack;
    [SerializeField] TextMeshProUGUI _healthNbUI;


    #endregion

    #region Unity Lifecycle

    void Start()
    {
        t = 0.008f;
        _currentHealth = _maxHealth;
        _previousHealth = _currentHealth;
    }

    void Update()
    {
        if (_previousHealth != _currentHealth)
        {
            _previousHealth = Mathf.Lerp(_previousHealth, _currentHealth, t);
        }

        _healthBarRed.fillAmount = _currentHealth / _maxHealth;
        _healthBarBlack.fillAmount = _previousHealth / _maxHealth;
        _healthNbUI.text = _currentHealth.ToString("0") + " / " + _maxHealth.ToString("0");

        if (_currentHealth <= 0)
        {
            IsPlayerDead = true;
        }
    }

    #endregion

    #region Methods

    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void LoseHP(int enemyDamage)
    {
        {
            _currentHealth -= enemyDamage;
        }
    }

    #endregion

    #region Private & Protected

    bool _isPlayerDead;
    float t = 0;
    float _previousHealth;

    public bool IsPlayerDead { get => _isPlayerDead; set => _isPlayerDead = value; }

    #endregion
}
