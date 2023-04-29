using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventory : MonoBehaviour
{
    [SerializeField] EnemyInventorySO _enemyInventorySO;
    [SerializeField] List<Item> _itemsPossessed = new List<Item>();
    [SerializeField] InventoryManager _inventoryManager;

    private void Start()
    {
        //_enemyLoot = GetComponent<EnemyLoot>();

        //for (int i = 0; i < 6; i++)
        //{
        //    int rand = Random.Range(0, _possibleItems.Count);
        //    _itemsPossessed.Add(_possibleItems[rand]);
        //    _inventoryManager.AddItem(_itemsPossessed[i], "Enemy");
        //}
    }

    private void Update()
    {
       //RefreshEnemyInventory();
    }

    //void RefreshEnemyInventory()
    //{
    //    if (_enemyLoot.IsLootMenuOpen)
    //    {
    //        if (_enemyLoot != null)
    //        {
    //            _inventoryManager.RemoveAllEnemyItems();

    //        }
    //        Debug.Log(_enemyLoot.IsLootMenuOpen);
    //        //if (!_isEnemyItemsCleared)
    //        //{
    //        //    _isEnemyItemsCleared = true;
    //        //}
    //        for (int i = 0; i < _itemsPossessed.Count; i++)
    //        {
    //            _inventoryManager.AddItem(_itemsPossessed[i], "Enemy");
    //            //_inventoryManager.RefreshContent(_inventoryManager.EnemyInventorySlots, _itemsPossessed);
    //        }
    //    }
    //}

    EnemyLoot _enemyLoot;
    [SerializeField]
    bool _isEnemyItemsCleared;

    public List<Item> ItemsPossessed { get => _itemsPossessed; }
}
