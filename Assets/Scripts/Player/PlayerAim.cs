using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    Light _flashlight;
    [SerializeField]
    LayerMask _groundMask;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
    }

    void Start()
    {
        _inputReader = GameObject.Find("GameManager").GetComponent<InputReader>();
        _playerCoverScript = GetComponent<PlayerCover>();
        _animator = GetComponentInChildren<Animator>();
        _kickCooldownScript = _animator.GetBehaviour<KickCooldown>();
    }

    void Update()
    {
        if (_isAiming)
        {
            _flashlight.enabled = true;
        }
        else
        {
            _flashlight.enabled = false;
        }
        characterPosition = transform.position;

    }

    private void FixedUpdate()
    {
        //if (_animator.GetBool("IsAiming") && !_kickCooldownScript.IsKickingAnim && !_playerCoverScript.IsTakingCover)
        //{
        //    Aiming();
        //}
    }

    #endregion

    #region Methods

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_cameraRay.origin, _cameraRay.direction * 50);

    }

    public void Aim()
    {
        _animator.SetBool("IsAiming", true);
        IsAiming = true;

        //else
        //{
        //    IsAiming = false;
        //}
    }

    public void AimEnd()
    {
        _animator.SetBool("IsAiming", false);
        IsAiming = false;
    }

    public void Aiming()
    {
        _cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        #region Other ways to do it, not working
        //RaycastHit hit;
        //if (Physics.Raycast(_cameraRay, out hit, Mathf.Infinity, _groundMask))
        //{
        //    var hitPosition = hit.point;
        //    var mousePos = Input.mousePosition;
        //    mousePos = transform.TransformPoint(mousePos);
        //    mousePos.y -= hitPosition.y;
        //    Debug.Log(mousePos.y);
        //    PointToLookAt = new Vector3(hit.point.x, mousePos.y, hit.point.z);
        //    // PointToLookAt = hitPosition;
        //    #region Other ways to do it, not working
        //    //float targetAngle = Mathf.Atan2(hit.point.z, hit.point.x) * Mathf.Rad2Deg;
        //    //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, 1);

        //    //Quaternion rotation = Quaternion.LookRotation(PointToLookAt - transform.position);
        //    //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime);
        //    //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 4f * Time.deltaTime);
        //    //_rigidbody.MoveRotation(rotation);
        //    //transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //    //transform.eulerAngles = new Vector3 (0f, targetAngle, 0f);
        //    //transform.Rotate( transform.position ,targetAngle);
        //    #endregion
        //    //transform.LookAt(PointToLookAt);
        //    var rotation = Quaternion.FromToRotation(Vector3.right, Vector3.zero - transform.position);
        //    transform.rotation = rotation;
        //}
        #endregion
        Plane projectilePlane = new Plane(Vector3.up, characterPosition + Vector3.up * projectileHeight); // Le plan du projectile est défini par une normale Vector3.up et une hauteur de projectileHeight
        float rayDistance;
        Vector3 pointOnProjectilePlane;
        if (projectilePlane.Raycast(_cameraRay, out rayDistance))
        { // Vérifie si le rayon intersecte le plan du projectile
            pointOnProjectilePlane = _cameraRay.origin + _cameraRay.direction * rayDistance;
        }
        else
        {
            // Le rayon ne touche pas le plan du projectile, nous ne pouvons pas calculer la position du projectile
            return;
        }


        // Étape 4 : trouver la position du point de sol en soustrayant la hauteur du projectile de la coordonnée Y du point d'intersection
        Vector3 groundPoint = new Vector3(pointOnProjectilePlane.x, pointOnProjectilePlane.y - projectileHeight, pointOnProjectilePlane.z);

        // Calculer la rotation du personnage en utilisant Quaternion.FromToRotation
        Quaternion characterRotation = Quaternion.FromToRotation(Vector3.forward, groundPoint - characterPosition);
        transform.rotation = characterRotation; // Appliquer la rotation au personnage

    }

    #endregion

    #region Private & Protected

    InputReader _inputReader;
    PlayerCover _playerCoverScript;
    Ray _cameraRay;
    Animator _animator;
    KickCooldown _kickCooldownScript;
    bool _isAiming;
    float projectileHeight = 1.47f;
    Vector3 characterPosition;

    public bool IsAiming { get => _isAiming; set => _isAiming = value; }

    #endregion
}
