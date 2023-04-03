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

        _coneDetection = transform.parent.GetComponentInChildren<ConeDetection>();
    }

    #endregion

    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _coneDetection.IsDetectedByEnemy = true;
        }
    }

    #endregion

    #region Private & Protected

    ConeDetection _coneDetection;
    #endregion
}
