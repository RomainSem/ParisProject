using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] InventorySlot[] _inventorySlots;
    [SerializeField] InventorySlot[] _enemyInventorySlots;
    [SerializeField] GameObject _itemPrefab;

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        InputToNavigateThroughSlots();
    }

    public bool AddItem(Item itemToAdd)
    {
        // Check if item is already in inventory
        // If it is, increase the quantity
        // If it isn't, add it to the inventory
        // If the inventory is full, drop the item on the ground
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            InventorySlot slot = _inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                if (itemToAdd.IsStackable)
                {
                    // slot._quantity++;

                }
                else
                {
                    SpawnNewItem(itemToAdd, slot);
                    return true;
                }
            }
            //else if (itemInSlot == itemToAdd)
            //{
            //    break;
            //}
        }
        return false;
    }

    public bool AddItemToEnemyInventory(Item itemToAdd)
    {
        for (int i = 0; i < _enemyInventorySlots.Length; i++)
        {
            InventorySlot slot = _enemyInventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                //if (itemToAdd.IsStackable)
                //{
                //    // slot._quantity++;
                //}
                    SpawnNewItem(itemToAdd, slot);
                    return true;
            }
            //else if (itemInSlot == itemToAdd)
            //{
            //    break;
            //}
        }
        return false;
    }

    public void RemoveItem(Item itemToRemove)
    {
        // Check if item is in inventory
        // If it is, decrease the quantity
        // If it isn't, do nothing
    }

    public void UseItem(Item itemToUse)
    {
        // Check if item is in inventory
        // If it is, use the item
        // If it isn't, do nothing
    }

    void SpawnNewItem(Item itemToSpawn, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(_itemPrefab, slot.transform);
        //newItemGo.transform.Find("ItemIcon").GetComponent<Image>().enabled = true;
        InventoryItem newItem = newItemGo.GetComponent<InventoryItem>();
        Debug.Log("SPAWN ITEM " + itemToSpawn);
        newItem.InitialiseItem(itemToSpawn);
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (_selectedSlot >= 0)
        {
            _inventorySlots[_selectedSlot].Deselect();
        }
        _inventorySlots[newValue].Select();
        _selectedSlot = newValue;
    }

    public Item GetSelectedItem(bool use) 
    { 
        InventorySlot slot = _inventorySlots[_selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.Item;
            if (use)
            {
                itemInSlot.Quantity--;
                if (itemInSlot.Quantity <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                //else
                //{
                    //itemInSlot.RefreshCount();
                //}
            }
            return item;
        }
        return null;
    }

    void InputToNavigateThroughSlots()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (_selectedSlot < _inventorySlots.Length - 1)
            {
                ChangeSelectedSlot(_selectedSlot + 1);
            }
            else
            {
                ChangeSelectedSlot(0);
            }
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (_selectedSlot > 0)
            {
                ChangeSelectedSlot(_selectedSlot - 1);
            }
            else
            {
                ChangeSelectedSlot(_inventorySlots.Length - 1);
            }
        }
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 7)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    

    int _selectedSlot = -1;
}
