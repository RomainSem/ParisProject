using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    #region Exposed

    //[SerializeField] float _rotationSpeed = 500f;
    //[SerializeField] float _turnSmoothTime = 0.1f;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _playerMovScript = GetComponent<PlayerMovement>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {
        //if (!_playerMovScript.IsRunning)
        //{
        //    Aim();
        //}
    }

    private void FixedUpdate()
    {
        if (!_playerMovScript.IsRunning)
        {
            Aim();
        }
    }

    #endregion

    #region Methods

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(cameraRay.origin, cameraRay.direction * 50);

    }

    void Aim()
    {
        if (Input.GetMouseButton(1))
        {
            IsAiming = true;
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(cameraRay, out hit, Mathf.Infinity, 1 << 8))
            {
                PointToLookAt = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                //float targetAngle = Mathf.Atan2(hit.point.z, hit.point.x) * Mathf.Rad2Deg;
                //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, 1);

                //Quaternion rotation = Quaternion.LookRotation(PointToLookAt - transform.position);
                //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime);
                //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 4f * Time.deltaTime);
                //_rigidbody.MoveRotation(rotation);
                //transform.rotation = Quaternion.Euler(0f, angle, 0f);
                //transform.eulerAngles = new Vector3 (0f, targetAngle, 0f);
                //transform.Rotate( transform.position ,targetAngle);
            }
                transform.LookAt(PointToLookAt);
                Debug.Log(PointToLookAt);
        }
        else
        {
            IsAiming = false;
        }
    }

    #endregion

    #region Private & Protected

    PlayerMovement _playerMovScript;
    bool _isAiming;
    Rigidbody _rigidbody;
    float _turnSmoothVelocity;
    Vector3 PointToLookAt;

    public bool IsAiming { get => _isAiming; set => _isAiming = value; }

    #endregion
}
