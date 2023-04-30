using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    Light _flashlight;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _inventoryManager = GameObject.Find("InventoryManager");
    }

    void Start()
    {
        _playerMovScript = GetComponent<PlayerMovement>();
        _animator = GetComponentInChildren<Animator>();
        _enemyInventory = _inventoryManager.GetComponent<EnemyInventory>();
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
    }

    private void FixedUpdate()
    {
        if (!_playerMovScript.IsRunning && !_kickCooldownScript.IsKickingAnim)
        {
            _cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Aim();
        }
    }

    #endregion

    #region Methods

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_cameraRay.origin, _cameraRay.direction * 50);

    }

    void Aim()
    {
        if (Input.GetMouseButton(1) && !_enemyInventory.IsMouseOverEnemy)
        {
            IsAiming = true;
            RaycastHit hit;
            if (Physics.Raycast(_cameraRay, out hit, Mathf.Infinity, 1 << 8))
            {
                PointToLookAt = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                #region Other ways to do it, not working
                //float targetAngle = Mathf.Atan2(hit.point.z, hit.point.x) * Mathf.Rad2Deg;
                //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, 1);

                //Quaternion rotation = Quaternion.LookRotation(PointToLookAt - transform.position);
                //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime);
                //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 4f * Time.deltaTime);
                //_rigidbody.MoveRotation(rotation);
                //transform.rotation = Quaternion.Euler(0f, angle, 0f);
                //transform.eulerAngles = new Vector3 (0f, targetAngle, 0f);
                //transform.Rotate( transform.position ,targetAngle);
                #endregion
                transform.LookAt(PointToLookAt);
            }
        }
        else
        {
            IsAiming = false;
        }
    }

    #endregion

    #region Private & Protected

    PlayerMovement _playerMovScript;
    Ray _cameraRay;
    Animator _animator;
    KickCooldown _kickCooldownScript;
    EnemyInventory _enemyInventory;
    GameObject _inventoryManager;
    bool _isAiming;
    Vector3 PointToLookAt;

    public bool IsAiming { get => _isAiming; set => _isAiming = value; }

    #endregion
}
