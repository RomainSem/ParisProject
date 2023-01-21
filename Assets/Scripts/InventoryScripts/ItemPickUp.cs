using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    #region Exposed

    [SerializeField] Item _item;
    

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    #endregion

    #region Methods

    void PickUp()
    {
        InventoryManager.Instance.AddItem( _item );
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        PickUp();
    }

    #endregion

    #region Private & Protected



    #endregion
}
