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
    }

    #endregion

    #region Methods



    #endregion

    #region Private & Protected

    

    #endregion
}
