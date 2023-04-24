using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] Item _itemToPickup;
    [SerializeField] InventoryManager _inventoryManager;

    private void PickupItem()
    {
        _inventoryManager.AddItem(_itemToPickup);
    }

    private void OnMouseDown()
    {
        PickupItem();
    }

}
