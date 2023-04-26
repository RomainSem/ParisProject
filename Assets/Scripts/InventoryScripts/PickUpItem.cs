using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] Item _itemToPickup;

    private void Start()
    {
        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        _itemDescUI = GameObject.Find("ItemDescPanel");
        _itemDescText = _itemDescUI.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
        _itemDescTextGreen = _itemDescUI.transform.Find("ItemDescriptionGreen").GetComponent<TextMeshProUGUI>();
        _itemName = _itemDescUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        _itemDescUI.SetActive(false);
    }

    private void Update()
    {
        if (_isMouseClicked)
        {
            _itemDescUI.SetActive(false);
        }
    }

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
        _isMouseClicked = true;
        PickupItem();
        Destroy(gameObject);
    }

    

    private void OnMouseOver()
    {
        // CHANGER CURSEUR
        // AFFICHER DESCRIPTION ITEM
        _itemDescUI.SetActive(true);
        _itemDescUI.transform.position = new Vector2(Input.mousePosition.x - 95, Input.mousePosition.y + 45);
        _itemDescText.text = _itemToPickup.Description;
        _itemDescTextGreen.text = _itemToPickup.DescriptionGreen;
        _itemName.text = _itemToPickup.ItemName;
    }

    private void OnMouseExit()
    {
        // CHANGER CURSEUR
        // CACHER DESCRIPTION ITEM
        _itemDescUI.SetActive(false);
    }

    GameObject _itemDescUI;
    bool _isMouseClicked;
    TextMeshProUGUI _itemDescText;
    TextMeshProUGUI _itemName;
    TextMeshProUGUI _itemDescTextGreen;
    InventoryManager _inventoryManager;

}
