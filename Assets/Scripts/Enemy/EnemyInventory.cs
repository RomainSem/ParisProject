using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventory : MonoBehaviour
{
    [SerializeField] List<Item> _items = new List<Item>();
    [SerializeField] InventoryManager _inventoryManager;

    private void Start()
    {
        int rand = Random.Range(0, _items.Count);
        _inventoryManager.AddItemToEnemyInventory(_items[rand]);
    }

}
