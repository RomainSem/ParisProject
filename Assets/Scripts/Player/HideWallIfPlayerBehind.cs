using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWallIfPlayerBehind : MonoBehaviour
{

    [SerializeField] Material _transparentMat;

    private void FixedUpdate()
    {
        RaycastFromCameraToPlayer();
    }

    private void RaycastFromCameraToPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, transform.position - Camera.main.transform.position, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Obstacle") && _isPlayerBehindWall == false)
            {
                Debug.Log("Player is behind wall");
                // Hide wall
                _wall = hit.transform.gameObject;
                _wallMat = _wall.GetComponent<MeshRenderer>().material;
                hit.transform.gameObject.GetComponent<MeshRenderer>().material = _transparentMat;
                _isPlayerBehindWall = true;
            }
            else if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player is not behind wall");
                // Show wall
                if (_wall != null)
                {
                    _wall.GetComponent<MeshRenderer>().material = _wallMat;
                    _isPlayerBehindWall = false;
                }
            }
        }
    }

    Material _wallMat;
    GameObject _wall;
    bool _isPlayerBehindWall;
}
