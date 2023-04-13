using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunch : MonoBehaviour
{
    #region Exposed

    [SerializeField] BoxCollider _punchCollider;
    

    #endregion

    #region Unity Lifecycle

    void Start()
    {
        _punchCollider.enabled = false;
    }

    #endregion

    #region Methods

    public void Punch()
    {
        _punchCollider.enabled = true;
    }

    public void StopPunch()
    {
        _punchCollider.enabled = false;
    }

    #endregion

    #region Private & Protected

    

    #endregion
}
