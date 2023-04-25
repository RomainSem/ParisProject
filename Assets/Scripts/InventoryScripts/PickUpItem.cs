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

    private void GetSelectedItem() 
    {
        Item receivedItem = _inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("RECEIVED ITEM " + receivedItem);
        }
        else
        {
            Debug.Log("NO ITEM RECEIVED");
        }
    }
    
    private void UseSelectedItem() 
    {
        Item usedItem = _inventoryManager.GetSelectedItem(true);
        if (usedItem != null)
        {
            Debug.Log("USED ITEM " + usedItem);
        }
        else
        {
            Debug.Log("NO ITEM USED");
        }
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("PICK UP ITEM");
        PickupItem();
        Destroy(gameObject);
        //Destroy(gameObject);
        Debug.Log("ITEM DESTROY");
    }

    private void OnMouseOver()
    {
        // CHANGER CURSEUR
    }

}
