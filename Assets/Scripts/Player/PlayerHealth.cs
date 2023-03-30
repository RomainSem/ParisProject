using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Exposed

    [SerializeField] byte _health = 1;


    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _health--;
            if (_health <= 0)
            {
                IsPlayerDead = true;
            }
        }
    }

    #endregion

    #region Private & Protected

    bool _isPlayerDead;
    public byte Health { get => _health; set => _health = value; }
    public bool IsPlayerDead { get => _isPlayerDead; set => _isPlayerDead = value; }

    #endregion
}
