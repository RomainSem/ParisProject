using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoToBedScript : MonoBehaviour
{
    #region Exposed

    [SerializeField] Animator _animPlayer;
    //[SerializeField] int _timeLayingDown = 2;


    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _player.GetComponent<Transform>();
        _rgbdPlayer = _player.GetComponent<Rigidbody>();
    }

    void Start()
    {
        _isPlayerInTrigger = false;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    #endregion

    #region Methods

    private void OnMouseDown()
    {
        if (_isPlayerInTrigger)
        {
            _isPlayerInTrigger = false;
            StartCoroutine(GoToBed(5f));
        }
        else
        {
            Debug.Log("Tu n'es pas assez proche");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other = _player.GetComponent<Collider>();
        _isPlayerInTrigger = true;
    }

    private IEnumerator GoToBed(float time)
    {
        _animPlayer.SetBool("IsSleeping", true);
        _playerTransform.position = new Vector3(-0.56f, 0.62f, 3.54f);
        _player.gameObject.GetComponent<Player>().MoveSpeed = 0;
        _player.gameObject.GetComponent<Player>().RunSpeed = 0;
        Debug.Log(_player.gameObject.GetComponent<Player>().MoveSpeed);
        _rgbdPlayer.constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(time);
        _animPlayer.SetBool("IsSleeping", false);
        _playerTransform.position = new Vector3(-0.56f, 0f, 3.0f);
        _player.gameObject.GetComponent<Player>().MoveSpeed = 1;
        _player.gameObject.GetComponent<Player>().RunSpeed = 1.3f;
        _rgbdPlayer.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    #endregion

    #region Private & Protected

    Transform _playerTransform;
    GameObject _player;
    Rigidbody _rgbdPlayer;
    bool _isPlayerInTrigger;
    //bool _isTransformLocked;

    #endregion
}
