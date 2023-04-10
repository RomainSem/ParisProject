using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PistolLaser : MonoBehaviour
{
    #region Exposed

    [SerializeField] GameObject _pistol;
    [SerializeField] PlayerAim _playerAimScript;
    [SerializeField] GameObject _bulletHole;

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
        _simpleShootScript = _pistol.GetComponentInChildren<SimpleShoot>();
    }

    //WORKING UPDATE
    //void Update()
    //{
    //    if (_playerAimScript.IsAiming)
    //    {
    //        Vector3 pistolDirection = new Vector3(0, 0, 100);
    //            _pistolLaser.enabled = true;
    //            _pistolLaser.SetPosition(1, pistolDirection);
    //    }
    //    else
    //    {
    //        _pistolLaser.enabled = false;
    //    }
    //}


    //TESTS
    //void Update()
    //{
    //    if (_playerAimScript.IsAiming)
    //    {
    //        _pistolLaser.enabled = true;

    //        Vector3 pistolDirection = new Vector3(0, 0, 100);
    //        RaycastHit hit;

    //        //D�finir les couches qui doivent �tre touch�es par le Raycast
    //        //int layerMask = LayerMask.GetMask("NomDeLaCouche");

    //        if (Physics.Raycast(transform.position, pistolDirection, out hit, Mathf.Infinity))
    //        {
    //            //D�finir la position de la fin du laser sur le point d'impact
    //            Debug.Log(hit.collider.gameObject.name);
    //            _pistolLaser.SetPosition(0, hit.point);
    //        }
    //        else
    //        {
    //            //Si le Raycast ne touche aucun objet, d�finir la position de la fin du laser � 100 unit�s
    //            _pistolLaser.SetPosition(0, pistolDirection);
    //        }
    //    }
    //    else
    //    {
    //        _pistolLaser.enabled = false;
    //    }
    //}

    void Update()
    {
        if (_playerAimScript.IsAiming)
        {
            //_pistolLaser.SetPosition(1, new Vector3(0, 0, 100));

            // Lancer un Raycast dans la direction du pistolet
            if (!_simpleShootScript.IsReloading && Input.GetButtonDown("Fire1"))
            {
                _pistolLaser.enabled = true;
                Vector3 pistolDirection = new Vector3(0, 0, 100);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, pistolDirection, out hit))
                {
                    _pistolLaser.SetPosition(1, pistolDirection);
                    // Vérifier si le Raycast a touché un objet
                    GameObject hitObject = hit.transform.gameObject;
                    Debug.Log("Le Raycast a touché l'objet : " + hitObject.layer);

                    if (hit.collider.gameObject.layer == 8)
                    {
                        // Créer un trou de balle sur l'objet touché
                        GameObject decal = Instantiate(_bulletHole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                        Destroy(decal, 5);
                    }
                }
            }
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
    SimpleShoot _simpleShootScript;

    #endregion
}
