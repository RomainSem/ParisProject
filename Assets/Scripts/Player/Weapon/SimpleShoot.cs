using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] Animator gunAnimator;
    [SerializeField] Transform barrelLocation;
    [SerializeField] Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destroy the casing object")][SerializeField] float destroyTimer = 2f;
    [Tooltip("Bullet Speed")][SerializeField] float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")][SerializeField] float ejectPower = 150f;

    [Tooltip("Max Number of Bullets")][SerializeField] float maxNbBullets = 7f;
    [Tooltip("Current Number of Bullets")][SerializeField] float currentNbBullets;
    //[Tooltip("Time To Reload")][SerializeField] float timeToReload = 1.5f;

    [Tooltip("Reload Text Blink")][SerializeField] TextMeshProUGUI reloadTxt;
    [Tooltip("UI Bullets Panel")][SerializeField] GameObject _nbBulletPanel;

    [SerializeField] int _bulletDamage;
    [SerializeField] float _critMultiplier = 1.5f;

    [SerializeField] PlayerAim _playerAimScript;

    void Start()
    {
#if DEVELOPMENT_BUILD
if (_playerAimScript == null)
	{
        Debug.Log("PLAYER AIM IS NULL");
	}
#endif
        reloadTxt.color = Color.clear;
        currentNbBullets = maxNbBullets;
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        isShooting = false;
        if (_playerAimScript.IsAiming)
        {
            Cursor.visible = false;
            //If you want a different input, change it here
            if (Input.GetButtonDown("Fire1"))
            {
                if (currentNbBullets > 0 || IsReloading == false)
                {
                    //Calls animation on the gun that has the relevant animation events that will fire
                    gunAnimator.SetTrigger("Fire");
                }
            }
        }
        else
        {
            Cursor.visible = true;
        }
        if (currentNbBullets <= 0 && IsReloading == false || currentNbBullets < maxNbBullets && Input.GetButtonDown("Reload") && IsReloading == false)
        {
            currentNbBullets = 0;
            StartCoroutine("Reload");
        }
    }


    //This function creates the bullet behavior
    void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        tempBullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        int critChance = Random.Range(0, 100);
        _bulletDamage = Random.Range(10, 20);
        if (critChance < 10)
        {
            _bulletDamage = Mathf.CeilToInt(_bulletDamage * _critMultiplier);
        }
        Debug.Log("Bullet damage : " + _bulletDamage);
        currentNbBullets--;
        isShooting = true;
        tempBullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        Destroy(tempBullet, 5f);

    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

    IEnumerator Reload()
    {
        float blinkSpeed = 0.2f; // La vitesse à laquelle le texte clignote
        float blinkTime = 0; // Le temps écoulé depuis le dernier clignotement
        bool blink = false; // Indique si le texte doit être visible ou non
        isReloading = true;
        _nbBulletPanel.SetActive(false);


        for (int i = 0; i < 400; i++)
        {
            blinkTime += Time.deltaTime;

            // Clignoter le texte
            if (blinkTime >= blinkSpeed)
            {
                blink = !blink;
                reloadTxt.color = blink ? Color.white : Color.clear;
                blinkTime = 0;
            }

            yield return null;
        }
        currentNbBullets = maxNbBullets;
        isReloading = false;

        // Remettre la couleur normale du texte
        _nbBulletPanel.SetActive(true);
        reloadTxt.color = Color.clear;
    }

    GameObject tempBullet;
    bool isReloading;
    bool isShooting;

    public float CurrentNbBullets { get => currentNbBullets; set => currentNbBullets = value; }
    public bool IsReloading { get => isReloading; set => isReloading = value; }
    public bool IsShooting { get => isShooting; set => isShooting = value; }
    public int BulletDamage { get => _bulletDamage; set => _bulletDamage = value; }
}
