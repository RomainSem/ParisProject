using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDetection : MonoBehaviour
{
    #region Exposed

    

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _animator = transform.parent.Find("Enemy").GetComponent<Animator>();
    }

    #endregion

    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
            _animator.SetBool("IsPlayerDetected", true);
        }
    }

    #endregion

    #region Private & Protected

    Animator _animator;
    #endregion
}
