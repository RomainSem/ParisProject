using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventory : MonoBehaviour
{

    [SerializeField] GameObject _lootMenu;

    private void Start()
    {
        _inventoryManager = GetComponent<InventoryManager>();
        _playerAim = GameObject.Find("Player").GetComponent<PlayerAim>();
        _lootMenu.SetActive(false);
    }

    private void Update()
    {
        if (_isEnemyDead && !_playerAim.IsAiming && Input.GetMouseButtonDown(1) && _isMouseOverEnemy)
        {
            if (hit.collider.gameObject != null)
            {
                _enemyClickedOn = hit.collider.gameObject;
            }
            _inventoryManager.RemoveAllEnemyItems();
            for (int i = 0; i < _enemyLoot.PossessedItems.Count; i++)
            {
                _inventoryManager.AddItem(_enemyLoot.PossessedItems[i], "Enemy");
            }
            OpenLootMenu();
        }

    }

    private void FixedUpdate()
    {
        RaycastFromMouseToEnemy();
    }

    void RaycastFromMouseToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                _isMouseOverEnemy = true;
                EnemyLoot currentEnemyLoot = hit.collider.gameObject.GetComponent<EnemyLoot>();
                _isEnemyDead = hit.collider.gameObject.GetComponent<EnemyBehaviour>().IsEnemyDead;
                if (currentEnemyLoot != _enemyLoot)
                {
                    _enemyLoot = currentEnemyLoot;
                }
            }
            else
            {
                _isMouseOverEnemy = false;
            }
        }
    }

    public void OpenLootMenu()
    {
        _lootMenu.SetActive(true);
        _lootMenu.transform.position = new Vector2(Input.mousePosition.x + 97, Input.mousePosition.y + 44);
        _isLootMenuOpen = true;
    }

    public void CloseLootMenu()
    {
        _lootMenu.SetActive(false);
        _isLootMenuOpen = false;
    }


    EnemyLoot _enemyLoot;
    PlayerAim _playerAim;
    RaycastHit hit;
    bool _isMouseOverEnemy;
    bool _isEnemyDead;
    bool _isLootMenuOpen;
    InventoryManager _inventoryManager;
    GameObject _enemyClickedOn;

    public GameObject EnemyClickedOn { get => _enemyClickedOn; set => _enemyClickedOn = value; }
    public bool IsLootMenuOpen { get => _isLootMenuOpen; set => _isLootMenuOpen = value; }
    public bool IsMouseOverEnemy { get => _isMouseOverEnemy; set => _isMouseOverEnemy = value; }
}
