using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [SerializeField] InventoryManager _inventoryManager;
    [SerializeField] EnemyInventorySO _enemyInventorySO;
    [SerializeField] List<Item> possessedItems = new List<Item>();


    public void ReturnEnemyPossessedItems()
    {
        for (int i = 0; i < _enemyInventorySO.PossibleItems.Count; i++)
        {
            int rand = Random.Range(0, _enemyInventorySO.PossibleItems.Count);
            possessedItems.Add(_enemyInventorySO.PossibleItems[rand]);
        }
    }



    bool _isMouseOverEnemy;
    bool _isLootMenuOpen;

    public bool IsLootMenuOpen { get => _isLootMenuOpen; set => _isLootMenuOpen = value; }
    public List<Item> PossessedItems { get => possessedItems; }
    public bool IsMouseOverEnemy { get => _isMouseOverEnemy; set => _isMouseOverEnemy = value; }
}
