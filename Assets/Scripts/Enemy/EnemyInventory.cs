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
        if ( _isEnemyDead && !_playerAim.IsAiming && Input.GetMouseButtonDown(0) && _isMouseOverEnemy && !_isEnemyInventoryGenerated)
        {
            {
                _inventoryManager.RemoveAllEnemyItems();
                for (int i = 0; i < _enemyLoot.PossessedItems.Count; i++)
                {
                    _inventoryManager.AddItem(_enemyLoot.PossessedItems[i], "Enemy");
                }
                _isEnemyInventoryGenerated = true;
            }
            _lootMenu.SetActive(true);
            _lootMenu.transform.position = new Vector2(Input.mousePosition.x + 97, Input.mousePosition.y + 44);
        }
    }

    private void FixedUpdate()
    {
        RaycastFromMouseToEnemy();
    }

    void RaycastFromMouseToEnemy()
    {
        RaycastHit hit;
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
                    _isEnemyInventoryGenerated = false;
                }
            }
            else
            {
                _isMouseOverEnemy = false;
            }
        }
    }

    public void CloseLootMenu()
    {
        _lootMenu.SetActive(false);
    }


    EnemyLoot _enemyLoot;
    PlayerAim _playerAim;
    bool _isMouseOverEnemy;
    bool _isEnemyDead;
    bool _isEnemyInventoryGenerated;
    InventoryManager _inventoryManager;
}
