using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [SerializeField] GameObject _lootMenu;
    [SerializeField] InventoryManager _inventoryManager;
    [SerializeField] EnemyInventorySO _enemyInventorySO;
    [SerializeField] List<Item> possessedItems = new List<Item>();


    private void Start()
    {
        _enemyBehaviour = GetComponent<EnemyBehaviour>();
        _playerAim = GameObject.Find("Player").GetComponent<PlayerAim>();
        _lootMenu.SetActive(false);
    }

    private void Update()
    {
        if (_enemyBehaviour.IsEnemyDead && _isMouseOverEnemy && !_playerAim.IsAiming && Input.GetMouseButtonDown(0) && !_isLootMenuOpen)
        {
            _lootMenu.SetActive(true);
            _inventoryManager.RemoveAllEnemyItems();
            foreach (var item in possessedItems)
            {
                _inventoryManager.AddItem(item, "Enemy");
            }
            _lootMenu.transform.position = new Vector2(Input.mousePosition.x + 97, Input.mousePosition.y + 44);
            _isLootMenuOpen = true;
        }
    }

    private void FixedUpdate()
    {
        RaycastToMouse();
    }


    private void RaycastToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                _isMouseOverEnemy = true;
            }
            else
            {
                _isMouseOverEnemy = false;
            }
        }
    }

    public void ReturnEnemyPossessedItems()
    {
        for (int i = 0; i < _enemyInventorySO.PossibleItems.Count; i++)
        {
            int rand = Random.Range(0, _enemyInventorySO.PossibleItems.Count);
            possessedItems.Add(_enemyInventorySO.PossibleItems[rand]);
            //_inventoryManager.AddItem(_enemyInventorySO.PossibleItems[rand], "Enemy");
        }
    }

    public void CloseLootMenu()
    {
        _lootMenu.SetActive(false);
        _isLootMenuOpen = false;
    }

    EnemyBehaviour _enemyBehaviour;
    bool _isMouseOverEnemy;
    bool _isLootMenuOpen;
    PlayerAim _playerAim;

    public bool IsLootMenuOpen { get => _isLootMenuOpen; set => _isLootMenuOpen = value; }
    public List<Item> PossessedItems { get => possessedItems; }
}
