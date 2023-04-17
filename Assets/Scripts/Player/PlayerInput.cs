using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Exposed

    public static Vector3 _leftStickDirection;
    [SerializeField] float _kickCooldownTime = 10;
    [SerializeField] Rigidbody _playerRgbd;

    #endregion

    #region Unity Lifecycle

    void Update()
    {
        float horizontal;
        float vertical;
        if (!_isKicking)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            _leftStickDirection = new Vector3(horizontal, 0, vertical).normalized;
        }
        if (Input.GetButtonDown("Kick"))
        {
            if (_canKick)
            {
                _isKicking = true;
                StartCoroutine(TimerKick());
            }
        }
    }

    #endregion

    #region Methods


    IEnumerator TimerKick()
    {
        _canKick = false;
        yield return new WaitForSeconds(_kickCooldownTime);
        _canKick = true;
    }

    #endregion

    #region Private & Protected

    bool _isKicking;
    bool _canKick = true;
    public bool IsKicking { get => _isKicking; set => _isKicking = value; }
    public bool CanKick { get => _canKick; set => _canKick = value; }
    public float KickCooldownTime { get => _kickCooldownTime; set => _kickCooldownTime = value; }

    #endregion
}
