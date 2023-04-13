using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    #region Exposed

    [SerializeField] BoxCollider _rightLegCollider;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _rightLegCollider.enabled = false;
    }

    void Start()
    {
        _playerInputScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInput>();
    }

    void Update()
    {
        //Kick();
    }

    #endregion

    #region Methods

    //public void Kick()
    //{
    //    if (_playerInputScript.IsKicking)
    //    {
    //        _rightLegCollider.enabled = true;
    //    }
    //    else
    //    {
    //        _rightLegCollider.enabled = false;
    //    }
    //}

    public void EnableKick()
    {
        _rightLegCollider.enabled = true;
    }

    public void DisableKick()
    {
        _rightLegCollider.enabled = false;
    }

    #endregion

    #region Private & Protected

    PlayerInput _playerInputScript;

    #endregion
}
