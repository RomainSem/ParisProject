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
        if (Input.GetKeyDown(KeyCode.T) && _isInCoverHitbox && !_isTakingCover)
        {
            Debug.Log("Taking cover");
            _savedConstraints = _rgb.constraints;
            _savedPosition = transform.position;
            _savedCollider = _playerCollider;
            _savedCollider.size = _playerCollider.size;
            Quaternion coverRotation = Quaternion.LookRotation(-hit.transform.parent.right);
            Vector3 coverPosition = new Vector3( hit.point.x-0.25f, transform.position.y, hit.point.z-0.25f);
            transform.position = coverPosition;
            _rgb.MoveRotation(coverRotation);
            _playerCollider.size = new Vector3(0.01f, 0.01f, 0.01f);
            _rgb.constraints = RigidbodyConstraints.FreezeAll;
            _isTakingCover = true;
        }
        else if (Input.GetKeyDown(KeyCode.T) && _isTakingCover)
        {
            Debug.Log("Leaving cover");
            _rgb.constraints = _savedConstraints;
            transform.position = _savedPosition;
            _playerCollider.size = _savedCollider.size;
            _isTakingCover = false;
        }
    }

    private void FixedUpdate()
    {
        RaycastFromPlayerToForward();
    }

    private void RaycastFromPlayerToForward()
    {
        Debug.DrawRay(transform.position, transform.forward * 2f, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            Debug.Log(hit.transform.name);
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.Log("In cover hitbox");
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
    BoxCollider _savedCollider;

    public bool IsTakingCover { get => _isTakingCover; set => _isTakingCover = value; }
}
