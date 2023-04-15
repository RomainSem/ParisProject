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
        _rgb = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
    }

    #endregion

    #region Methods

    public void EnableConstraints()
    {
        _rgb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void DisableConstraints()
    {
        _rgb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }


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
    Rigidbody _rgb;

    #endregion
}
