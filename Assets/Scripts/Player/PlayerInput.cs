using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Exposed

    public static Vector3 _leftStickDirection;

    #endregion

    #region Unity Lifecycle

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _leftStickDirection = new Vector3(horizontal, 0, vertical).normalized;
        if (Input.GetButtonDown("Kick"))
        {
            _isKicking = true;
        }
        else
        {
            _isKicking = false;
        }
    }

    #endregion

    #region Methods



    #endregion

    #region Private & Protected

    bool _isKicking;

    public bool IsKicking { get => _isKicking; set => _isKicking = value; }

    #endregion
}
