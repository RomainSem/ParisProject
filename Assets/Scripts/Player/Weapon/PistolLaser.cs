using UnityEngine;

public class PistolLaser : MonoBehaviour
{
    #region Exposed

    [SerializeField] GameObject _pistol;
    [SerializeField] PlayerAim _playerAimScript;
    [SerializeField] GameObject _bulletHole;

    #endregion

    #region Unity Lifecycle
    void Start()
    {
        _pistolLaser = GetComponent<LineRenderer>();
        _pistolLaser.widthMultiplier = 0.1f;
        _simpleShootScript = _pistol.GetComponentInChildren<SimpleShoot>();
        _playerInputScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInput>();
        _kickCooldownScript = _pistol.GetComponentInParent<Animator>().GetBehaviour<KickCooldown>();
    }

    void Update()
    {
        if (_playerAimScript.IsAiming)
        {

            // Lancer un Raycast dans la direction du pistolet
            if (!_simpleShootScript.IsReloading && !_kickCooldownScript.IsKickingAnim /* && _playerInputScript.CanKick*/)
            {
                _pistolLaser.enabled = true;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward * 100, out hit))
                {
                    //Vector3 pistolDirection = new Vector3(0, 0, 100);
                    Debug.DrawLine(transform.position, hit.point);
                    _pistolLaser.SetPosition(1, transform.InverseTransformPoint(hit.point));
                    // Vérifier si le Raycast a touché un objet
                    GameObject hitObject = hit.transform.gameObject;
                    //Debug.Log("Le Raycast a touché l'objet : " + hitObject.name);

                    if (/*hit.collider.gameObject.CompareTag("Obstacle") && */_simpleShootScript.IsShooting)
                    {
                        // Créer un trou sur l'objet touché
                        GameObject decal = Instantiate(_bulletHole, hit.point, Quaternion.FromToRotation(-Vector3.forward, hit.normal));
                        decal.transform.position = hit.point;
                        decal.transform.parent = hitObject.transform;
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
    PlayerInput _playerInputScript;
    SimpleShoot _simpleShootScript;
    KickCooldown _kickCooldownScript;
    #endregion
}
