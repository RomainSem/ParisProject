using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolLaser : MonoBehaviour
{
    #region Exposed

    [SerializeField] GameObject _pistol;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {

    }

    void Start()
    {
        _pistolLaser = GetComponent<LineRenderer>();
        _playerAimScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAim>();
        _pistolLaser.widthMultiplier = 0.1f;
        _pistolLaser.positionCount = 20;
    }

    void Update()
    {
        if (_playerAimScript.IsAiming)
        {
            _pistolLaser.enabled = true;
            Vector3 pistolDirection = /*_pistol.transform.rotation * */_pistol.transform.forward;
            Vector3 pistolEndPosition = _pistol.transform.position + pistolDirection * 50;
            _pistolLaser.SetPosition(1 , pistolEndPosition);
            //_pistolLaser.SetPosition(0, _pistol.transform.position);
        }
        else
        {
            _pistolLaser.enabled = false;
        }
    }


    #endregion

    #region Methods

    #endregion

    #region Private & Protected

    LineRenderer _pistolLaser;
    PlayerAim _playerAimScript;

    #endregion
}
