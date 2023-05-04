using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventory : MonoBehaviour
{

    [SerializeField] GameObject _lootMenu;
    [SerializeField] GameObject _interactPanel;
    [SerializeField] PlayerMovement _playerMovement;

    private void Start()
    {
        _inventoryManager = GetComponent<InventoryManager>();
        _playerAim = GameObject.Find("Player").GetComponent<PlayerAim>();
        _playerDetected = GameObject.Find("Player").GetComponent<PlayerDetected>();
        _lootMenu.SetActive(false);
    }

    private void Update()
    {
        if (_isLootMenuOpen)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
        if (_isLootMenuOpen && _playerAim.IsAiming)
        {
            CloseLootMenu();
        }

        if (_playerDetected.IsPlayerInLootZone && !_playerAim.IsAiming)
        {
            if (_playerDetected.ActualEnemyLoot != null)
            {
                if (_playerDetected.ActualEnemyLoot.transform.parent.Find("Enemy").GetComponent<EnemyLoot>().PossessedItems.Count == 0)
                {
                    CloseLootMenu();
                    _interactPanel.SetActive(false);
                    return;
                }
                _interactPanel.SetActive(true);
                if (Input.GetButtonDown("Use"))
                {
                    if (_isLootMenuOpen)
                    {
                        CloseLootMenu();
                        return;
                    }
                    else
                    {
                        _inventoryManager.RemoveAllEnemyItems();
                        for (int i = 0; i < _playerDetected.ActualEnemyLoot.transform.parent.Find("Enemy").GetComponent<EnemyLoot>().PossessedItems.Count; i++)
                        {
                            _inventoryManager.AddItem(_playerDetected.ActualEnemyLoot.transform.parent.Find("Enemy").GetComponent<EnemyLoot>().PossessedItems[i], "Enemy");
                        }
                        OpenLootMenu();
                    }
                }
            }
        }
        else
        {
            _interactPanel.SetActive(false);
            CloseLootMenu();
        }
        if (_playerMovement.IsMoving)
        {
            CloseLootMenu();
        }
    }

    public void OpenLootMenu()
    {
        CursorControl.SetPosition(Screen.width / 2, Screen.height / 2);
        _lootMenu.SetActive(true);
        _lootMenu.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        _isLootMenuOpen = true;
    }

    public void CloseLootMenu()
    {
        _lootMenu.SetActive(false);
        _isLootMenuOpen = false;
    }


    PlayerAim _playerAim;
    PlayerDetected _playerDetected;
    bool _isLootMenuOpen;
    InventoryManager _inventoryManager;

    public bool IsLootMenuOpen { get => _isLootMenuOpen; set => _isLootMenuOpen = value; }
}
