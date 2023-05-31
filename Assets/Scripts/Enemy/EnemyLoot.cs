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
        int randNbItems = Random.Range(0, _enemyInventorySO.PossibleItems.Count);
        for (int i = 0; i < randNbItems; i++)
        {
            int randItem = Random.Range(0, _enemyInventorySO.PossibleItems.Count);
            possessedItems.Add(_enemyInventorySO.PossibleItems[randItem]);
        }
    }

    private void Update()
    {
        if (possessedItems.Count == 0)
        {
            gameObject.transform.Find("CanBeLootParticles").gameObject.SetActive(false);
        }
    }



    bool _isMouseOverEnemy;
    bool _isLootMenuOpen;

    public bool IsLootMenuOpen { get => _isLootMenuOpen; set => _isLootMenuOpen = value; }
    public List<Item> PossessedItems { get => possessedItems; }
    public bool IsMouseOverEnemy { get => _isMouseOverEnemy; set => _isMouseOverEnemy = value; }
}
