using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    #region Exposed

    [SerializeField] float _rotationSpeed = 1000f;


    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _playerMovScript = GetComponent<PlayerMovement>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (!_playerMovScript.IsRunning)
        {
            Aim();
        }
    }

    private void FixedUpdate()
    {

    }

    #endregion

    #region Methods

    void Aim()
    {
        if (Input.GetMouseButton(1))
        {
            IsAiming = true;
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(cameraRay, out hit))
            {
                Vector3 PointToLookAt = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                Quaternion rotation = Quaternion.LookRotation(PointToLookAt - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            IsAiming = false;
        }
        Debug.Log(IsAiming);
    }

    #endregion

    #region Private & Protected

    PlayerMovement _playerMovScript;
    bool _isAiming;

    public bool IsAiming { get => _isAiming; set => _isAiming = value; }

    #endregion
}
