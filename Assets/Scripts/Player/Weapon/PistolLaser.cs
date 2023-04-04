using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    }

    void Update()
    {
        if (_playerAimScript.IsAiming)
        {
            RaycastHit hit;
            Vector3 pistolDirection = new Vector3(0, 0, 100);
            if (Physics.Raycast(transform.position, pistolDirection, out hit, Mathf.Infinity))
            {

            _pistolLaser.enabled = true;
            //pistolDirection = new Vector3(0, 0 , 100);
            _pistolLaser.SetPosition(1 , pistolDirection);
            }
        }
        else
        {
            _pistolLaser.enabled = false;
        }
    }

    //void Update()
    //{
    //    if (_playerAimScript.IsAiming)
    //    {
    //        _pistolLaser.enabled = true;

    //        Vector3 pistolDirection = new Vector3(0, 0, 100);
    //        RaycastHit hit;

    //        //Définir les couches qui doivent être touchées par le Raycast
    //        //int layerMask = LayerMask.GetMask("NomDeLaCouche");

    //        if (Physics.Raycast(transform.position, pistolDirection, out hit, Mathf.Infinity))
    //        {
    //            //Définir la position de la fin du laser sur le point d'impact
    //            Debug.Log(hit.collider.gameObject.name);
    //            _pistolLaser.SetPosition(0, hit.point);
    //        }
    //        else
    //        {
    //            //Si le Raycast ne touche aucun objet, définir la position de la fin du laser à 100 unités
    //            _pistolLaser.SetPosition(0, pistolDirection);
    //        }
    //    }
    //    else
    //    {
    //        _pistolLaser.enabled = false;
    //    }
    //}


    #endregion

    #region Methods

    #endregion

    #region Private & Protected

    LineRenderer _pistolLaser;
    GameObject _player;

    #endregion
}
