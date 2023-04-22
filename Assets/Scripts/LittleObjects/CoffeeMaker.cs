using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMaker : MonoBehaviour
{
    #region Expose

    [SerializeField] GameObject _cupOfCoffee;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        ActivateOutline(0);

    }

    void Update()
    {

    }


    #endregion

    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            ActivateOutline(1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ActivateOutline(0);
    }

    private void OnMouseOver()
    {
        ActivateOutline(3f);
    }

    private void OnMouseExit()
    {
        ActivateOutline(0);
    }

    private void OnMouseDown()
    {
        if (!_isCoffeeMade)
        {
            ActivateOutline(0f);
            Instantiate(_cupOfCoffee, _instantiatePos);
            _cupOfCoffee.transform.position = new Vector3(12.601f, 1.001f, -0.901f);
            _isCoffeeMade = true;
        }
    }

    void ActivateOutline(float width)
    {
        var _outline = gameObject.GetComponent<Outline>();
        _outline.OutlineMode = Outline.Mode.OutlineAll;
        _outline.OutlineColor = Color.yellow;
        _outline.OutlineWidth = width;
    }



    #endregion

    #region Private & Protected

    GameObject _player;
    bool _isCoffeeMade;
    Transform _instantiatePos;

    #endregion
}
