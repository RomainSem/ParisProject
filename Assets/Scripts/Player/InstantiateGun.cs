using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGun : MonoBehaviour
{
    #region Expose

    [SerializeField] GameObject _gun;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _playerAimScript = GetComponentInParent<PlayerAim>();
    }

    void Start()
    {

    }

    void Update()
    {
        //if (_playerAimScript.IsAiming)
        //{
        //    if (!_isGunInstantiated)
        //    {
        //        _isGunInstantiated = true;
        //        _gun.SetActive(true);
        //        //gun = Instantiate(_gunPrefab, transform.position, Quaternion.);
        //        //gun.transform.parent = transform;
        //    }
        //}
        //else
        //{
        //    //Destroy(gun);
        //    _gun.SetActive(false);
        //    _isGunInstantiated = false;
        //}

    }


    #endregion

    #region Methods


    #endregion

    #region Private & Protected

    PlayerAim _playerAimScript;
    bool _isGunInstantiated;
    GameObject gun;

    #endregion
}
