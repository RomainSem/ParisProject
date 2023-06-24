using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCover : MonoBehaviour
{
    private void Start()
    {
        _rgb = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        ToggleCover();
    }

    private void FixedUpdate()
    {
        RaycastFromPlayerToForward();
    }

    private void ToggleCover()
    {
        if (Input.GetKeyDown(KeyCode.C) && _isInCoverHitbox && !_isTakingCover)
        {
            if (hit.collider == null) return;
            _savedConstraints = _rgb.constraints;
            _savedPosition = transform.position;
            _savedColliderSize = _playerCollider.size;
            Quaternion coverRotation = Quaternion.LookRotation(hit.normal);
            Vector3 coverPosition = new Vector3(hit.point.x - 0.25f, transform.position.y, hit.point.z - 0.25f);
            transform.position = coverPosition;
            _rgb.MoveRotation(coverRotation);
            _playerCollider.size = new Vector3(0.3f, 0.3f, 0.3f);
            _rgb.constraints = RigidbodyConstraints.FreezeAll;
            _isTakingCover = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && _isTakingCover)
        {
            _rgb.constraints = _savedConstraints;
            transform.position = _savedPosition;
            _playerCollider.size = _savedColliderSize;
            _isTakingCover = false;
        }
    }

    private void RaycastFromPlayerToForward()
    {
        Vector3 raycastOrigin = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
        Debug.DrawRay(raycastOrigin, transform.forward * 2f, Color.blue);
        if (Physics.Raycast(raycastOrigin, transform.forward, out hit, 1.5f))
        {
            Debug.Log("What is the ray hitting ? : " + hit.transform.tag);
            if (hit.collider.CompareTag("Obstacle"))
            {
                _isInCoverHitbox = true;
            }
            else
            {
                _isInCoverHitbox = false;
            }
        }
    }

    bool _isTakingCover;
    bool _isInCoverHitbox;
    Rigidbody _rgb;
    RigidbodyConstraints _savedConstraints;
    Vector3 _savedPosition;
    RaycastHit hit;
    BoxCollider _playerCollider;
    Vector3 _savedColliderSize;

    public bool IsTakingCover { get => _isTakingCover; }
}
