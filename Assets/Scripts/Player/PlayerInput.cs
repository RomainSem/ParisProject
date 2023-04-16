using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Exposed

    public static Vector3 _leftStickDirection;
    [SerializeField] float _kickCooldownTime = 10;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        _rgbd = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _leftStickDirection = new Vector3(horizontal, 0, vertical).normalized;
        if (Input.GetButtonDown("Kick"))
        {
            if (_canKick)
            {
                _rgbd.velocity = Vector3.zero;
                _isKicking = true;
                StartCoroutine(TimerKick());
            }
        }
        else
        {
            _isKicking = false;
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
    Rigidbody _rgbd;
    public bool IsKicking { get => _isKicking; set => _isKicking = value; }
    public bool CanKick { get => _canKick; set => _canKick = value; }
    public float KickCooldownTime { get => _kickCooldownTime; set => _kickCooldownTime = value; }

    #endregion
}
