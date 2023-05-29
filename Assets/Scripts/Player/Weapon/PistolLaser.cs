using UnityEngine;

public class PistolLaser : MonoBehaviour
{
    #region Exposed

    [SerializeField] GameObject _pistol;
    [SerializeField] PlayerAim _playerAimScript;
    [SerializeField] GameObject _bulletHole;
    [SerializeField] LayerMask layerMask;

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
        if (_simpleShootScript.IsShooting && _hit.collider != null)
        {
            // Créer un trou sur l'objet touché
            GameObject decal = Instantiate(_bulletHole, _hit.point, Quaternion.FromToRotation(-Vector3.forward, _hit.normal));
            decal.transform.position = _hit.point;
            decal.transform.parent = _hit.transform.parent;
            //Destroy(decal, 5);
        }
    }

    private void FixedUpdate()
    {
        if (_playerAimScript.IsAiming)
        {
            // Lancer un Raycast dans la direction du pistolet
            if (!_simpleShootScript.IsReloading && !_kickCooldownScript.IsKickingAnim /* && _playerInputScript.CanKick*/)
            {
                _pistolLaser.enabled = true;
                if (Physics.Raycast(transform.position, transform.forward , out _hit, Mathf.Infinity, layerMask))
                {
                    //Debug.DrawLine(transform.position, hit.point);
                    _pistolLaser.SetPosition(1, transform.InverseTransformPoint(_hit.point));


                    // Vérifier si le Raycast a touché un objet
                    //GameObject hitObject = _hit.transform.gameObject;
                    //Debug.Log("Le Raycast a touché l'objet : " + hitObject.name);
                }
                else
                {
                    _pistolLaser.SetPosition(1, new Vector3(0, 0, 100));
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
    RaycastHit _hit;
    PlayerInput _playerInputScript;
    SimpleShoot _simpleShootScript;
    KickCooldown _kickCooldownScript;
    bool _canInstantiateBulletHole;
    #endregion
}
