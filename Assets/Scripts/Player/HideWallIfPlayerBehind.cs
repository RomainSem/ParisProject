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
                // Hide wall
                _wall = hit.transform.gameObject;
                _wallMat = _wall.GetComponent<MeshRenderer>().material;
                Color _matColor = _wallMat.color;
                _transparentMat.color = new Color(_matColor.r, _matColor.g, _matColor.b, 0.6f);
                hit.transform.gameObject.GetComponent<MeshRenderer>().material = _transparentMat;
                _isPlayerBehindWall = true;
            }
            else if (hit.collider.gameObject.CompareTag("Player"))
            {
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