using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGun : MonoBehaviour
{
    #region Expose

    [SerializeField] GameObject _gunPrefab;
    
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
        if (_playerAimScript.IsAiming && !_isGunInstantiated)
        {
            Vector3 pos = new Vector3(0.0368000008f, 1.41100001f, 0.414700001f);
            gun = Instantiate(_gunPrefab, pos, Quaternion.identity);
            _isGunInstantiated = true;
        }
        else
        {
            Destroy(gun);
            _isGunInstantiated = false;
        }
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
