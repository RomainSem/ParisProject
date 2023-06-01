using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] InventorySlot[] _inventorySlots;
    [SerializeField] InventorySlot[] _enemyInventorySlots;
    [SerializeField] int _maxNbStackedItems = 6;
    [SerializeField] GameObject _itemPrefab;

    private void Start()
    {
        //_enemyInventoryItems = GameObject.Find("Enemy").GetComponent<EnemyInventory>().ItemsPossessed;
        ChangeSelectedSlot(0);
        _playerDetected = GameObject.Find("Player").GetComponent<PlayerDetected>();
        _objectEffects = GameObject.Find("GameManager").GetComponent<ObjectsEffects>();
        _simpleShoot = GameObject.Find("Player").GetComponentInChildren<SimpleShoot>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("TakeAll") && gameObject.GetComponent<EnemyInventory>().IsLootMenuOpen)
        {
            AddAllItems();

        }
        //InputToNavigateThroughSlots();
    }

    public bool AddItem(Item itemToAdd, string whichInventory)
    {
        // Check if item is already in inventory
        // If it is, increase the quantity
        // If it isn't, add it to the inventory
        // If the inventory is full, drop the item on the ground
        if (whichInventory == "Player")
        {
            _isItemToPlayer = true;
            switch (itemToAdd.name)
            {
                case "AmmoBox":
                    _objectEffects.UseAmmoBox();
                    break;
                case "Key":
                    _objectEffects.UseNormalKey();
                    break;
                case "Money":
                    _objectEffects.UseCoin();
                    break;
                default:
                    for (int i = 0; i < _inventorySlots.Length; i++)
                    {
                        InventorySlot slot = _inventorySlots[i];
                        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                        if (itemInSlot != null && itemInSlot.Item == itemToAdd && itemInSlot.Item.IsStackable && itemInSlot.Quantity < _maxNbStackedItems)
                        {
                            itemInSlot.Quantity++;
                            return true;
                        }
                        else if (itemInSlot == null)
                        {
                            SpawnNewItem(itemToAdd, slot);
                            return true;
                        }
                    }
                    return false;

            }
            return false;
        }
        else if (whichInventory == "Enemy")
        {
            _isItemToPlayer = false;
            for (int i = 0; i < _enemyInventorySlots.Length; i++)
            {
                InventorySlot slot = _enemyInventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null)
                {
                    //if (itemToAdd.IsStackable)
                    //{
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
        else
        {
            return false;
        }
    }

    public void AddAllItems()
    {
        GameObject enemy = _playerDetected.ActualEnemyLoot.transform.parent.Find("Enemy").gameObject;
        foreach (InventorySlot slot in _enemyInventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                //if (itemInSlot.Item.name == "AmmoBox" && _simpleShoot.CurrentNbBulletsInMagazine > 31) return;
                AddItem(itemInSlot.Item, "Player");
                //RemoveItemFromEnemy(itemInSlot.Item);
            }
        }
        RemoveAllEnemyItems();
        enemy.GetComponent<EnemyLoot>().PossessedItems.Clear();
    }

    public void RemoveItemFromEnemy(Item itemToRemove)
    {
        // Check if item is in inventory
        // If it is, decrease the quantity
        // If it isn't, do nothing
        //for (int i = 0; i < _enemyInventorySlots.Length; i++)
        //{
        //    InventorySlot slot = _enemyInventorySlots[i];
        //    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        //    if (itemInSlot != null)
        //    {
        //        if (itemInSlot.Item == itemToRemove)
        //        {
        //            Destroy(itemInSlot.gameObject);
        //            break;
        //        }
        //    }
        //}
    }

    public void RemoveItemFromPlayer(Item itemToRemove)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            InventorySlot slot = _inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                if (itemInSlot.Item == itemToRemove)
                {
                    Destroy(itemInSlot.gameObject);
                    //break;
                }
            }
        }
    }

    public void RemoveAllEnemyItems()
    {
        for (int i = 0; i < _enemyInventorySlots.Length; i++)
        {
            InventorySlot slot = _enemyInventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Destroy(itemInSlot.gameObject);
            }
        }
    }

    void SpawnNewItem(Item itemToSpawn, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(_itemPrefab, slot.transform);
        //newItemGo.transform.Find("ItemIcon").GetComponent<Image>().enabled = true;
        InventoryItem newItem = newItemGo.GetComponent<InventoryItem>();
        //Debug.Log("SPAWN ITEM " + itemToSpawn);
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

    public void RefreshContent(InventorySlot[] inventorySlots, List<Item> itemsToRefreshWith)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot currentSlot = inventorySlots[i].GetComponent<InventorySlot>();
            InventoryItem itemSlot = currentSlot.GetComponentInChildren<InventoryItem>();
            itemSlot.Item = null;
            itemSlot.GetComponentInChildren<Image>().sprite = null;
        }
        for (int i = 0; i < itemsToRefreshWith.Count; i++)
        {
            InventorySlot currentSlot = inventorySlots[i].GetComponent<InventorySlot>();
            InventoryItem itemSlot = currentSlot.GetComponentInChildren<InventoryItem>();
            itemSlot.Item = _enemyInventoryItems[i];
            itemSlot.GetComponentInChildren<Image>().sprite = _enemyInventoryItems[i].Icon;
        }
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
    bool _isItemToPlayer;
    List<Item> _enemyInventoryItems;
    PlayerDetected _playerDetected;
    ObjectsEffects _objectEffects;
    SimpleShoot _simpleShoot;

    public InventorySlot[] EnemyInventorySlots { get => _enemyInventorySlots; set => _enemyInventorySlots = value; }
    public InventorySlot[] InventorySlots { get => _inventorySlots; set => _inventorySlots = value; }
    public bool IsItemToPlayer { get => _isItemToPlayer; set => _isItemToPlayer = value; }
}
