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
        _playerRGBD = _player.GetComponent<Rigidbody>();
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
            StartCoroutine(GoToBed(5f));
            _isPlayerInTrigger = false;
        }
        else
        {
            Debug.Log("Tu n'es pas assez proche");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _isPlayerInTrigger = true;
        }
    }

    private IEnumerator GoToBed(float time)
    {
        IsSleeping = true;
        _playerTransform.position = new Vector3(-0.56f, 0.62f, 3.54f);
        _playerRGBD.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        yield return new WaitForSeconds(time);
        IsSleeping = false;
        _playerTransform.position = new Vector3(-0.56f, 0f, 3.0f);
        _playerRGBD.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    #endregion

    #region Private & Protected

    Transform _playerTransform;
    GameObject _player;
    Rigidbody _playerRGBD;
    bool _isPlayerInTrigger;
    bool _isSleeping;

    public bool IsSleeping { get => _isSleeping; set => _isSleeping = value; }

    #endregion
}
