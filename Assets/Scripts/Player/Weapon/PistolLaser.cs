using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolLaser : MonoBehaviour
{
    #region Exposed

    [SerializeField] GameObject _pistol;
    [SerializeField] PlayerAim _playerAimScript;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        _pistolLaser = GetComponent<LineRenderer>();
        _pistolLaser.widthMultiplier = 0.1f;
        _pistolLaser.positionCount = 20;
    }

    void Update()
    {
        if (_playerAimScript.IsAiming)
        {
            _pistolLaser.enabled = true;
            Vector3 pistolDirection = new Vector3(0, 0 , 100);
            //Vector3 pistolEndPosition = pistolDirection * 100;
            _pistolLaser.SetPosition(1 , pistolDirection);
            Debug.Log("pistolposition " + _pistol.transform.position);
            Debug.Log("pistolforward " + pistolDirection);
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
    GameObject _player;

    #endregion
}
