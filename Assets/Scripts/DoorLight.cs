using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLight : MonoBehaviour
{
    #region Expose

    
    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _anim = GetComponentInParent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }


    #endregion

    #region Methods

    private void OnTriggerEnter(Collider other)
    {
    }

    #endregion

    #region Private & Protected

    Animator _anim;


    #endregion
}
