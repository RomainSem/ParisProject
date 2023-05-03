using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventory : MonoBehaviour
{

    [SerializeField] GameObject _lootMenu;
    //[SerializeField] LayerMask _layerMask;
    //[SerializeField] Texture2D _lootCursor;
    //[SerializeField] Vector3 _lootCursorOffset;
    [SerializeField] GameObject _interactPanel;
    [SerializeField] PlayerMovement _playerMovement;

    private void Start()
    {
        _inventoryManager = GetComponent<InventoryManager>();
        _playerAim = GameObject.Find("Player").GetComponent<PlayerAim>();
        _playerDetected = GameObject.Find("Player").GetComponent<PlayerDetected>();
        //_layerMask = ~_layerMask;
        _lootMenu.SetActive(false);
    }

    private void Update()
    {
        if (_isLootMenuOpen && _playerAim.IsAiming)
        {
            CloseLootMenu();
        }

        if (_playerDetected.IsPlayerInLootZone && !_playerAim.IsAiming)
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
        else
        {
            _interactPanel.SetActive(false);
            CloseLootMenu() ;
        }
        if (_playerMovement.IsMoving)
        {
            CloseLootMenu();
        }
        //if (_enemyClickedOn != null)
        //{
        //    if (_enemyClickedOn.GetComponent<EnemyLoot>().PossessedItems.Count == 0)
        //    {
        //        CloseLootMenu();
        //        Debug.Log("No items");
        //        //return;
        //    }
        //}
        //if (_isMouseOverEnemy && _isEnemyDead && !_playerAim.IsAiming)
        //{
        //    Cursor.SetCursor(_lootCursor, _lootCursorOffset, CursorMode.ForceSoftware);
        //    _interactPanel.SetActive(true);
        //    if (Input.GetButtonDown("Use") /*Input.GetMouseButtonDown(1)*/)
        //    {
        //        if (hit.collider.gameObject != null)
        //        {
        //            _enemyClickedOn = hit.collider.transform.parent.Find("Enemy").gameObject;
        //        }
        //        _inventoryManager.RemoveAllEnemyItems();
        //        for (int i = 0; i < _enemyLoot.PossessedItems.Count; i++)
        //        {
        //            _inventoryManager.AddItem(_enemyLoot.PossessedItems[i], "Enemy");
        //        }
        //        OpenLootMenu();
        //    }
        //}
        //else
        //{
        //    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        //    _interactPanel.SetActive(false);
        //    if (Input.GetButtonDown("Use"))
        //    {
        //        CloseLootMenu();
        //    }
        //}
        //if (_playerMovement.IsMoving)
        //{
        //    CloseLootMenu();
        //}





    }

    //private void FixedUpdate()
    //{
    //    RaycastFromMouseToEnemy();
    //}

    //void RaycastFromMouseToEnemy()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out hit, 1000, _layerMask))
    //    {
    //        if (hit.collider.CompareTag("EnemyLoot"))
    //        {
    //            _isMouseOverEnemy = true;
    //            EnemyLoot currentEnemyLoot = hit.collider.gameObject.transform.parent.Find("Enemy").GetComponent<EnemyLoot>();
    //            _isEnemyDead = hit.collider.gameObject.transform.parent.Find("Enemy").GetComponent<EnemyBehaviour>().IsEnemyDead;
    //            if (currentEnemyLoot != _enemyLoot)
    //            {
    //                _enemyLoot = currentEnemyLoot;
    //            }
    //        }
    //        else
    //        {
    //            _isMouseOverEnemy = false;
    //        }
    //    }
    //}

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


    //EnemyLoot _enemyLoot;
    PlayerAim _playerAim;
    PlayerDetected _playerDetected;
    //RaycastHit hit;
    //bool _isMouseOverEnemy;
    //bool _isEnemyDead;
    bool _isLootMenuOpen;
    InventoryManager _inventoryManager;
    //GameObject _enemyClickedOn;

    //public GameObject EnemyClickedOn { get => _enemyClickedOn; set => _enemyClickedOn = value; }
    public bool IsLootMenuOpen { get => _isLootMenuOpen; set => _isLootMenuOpen = value; }
    //public bool IsMouseOverEnemy { get => _isMouseOverEnemy; set => _isMouseOverEnemy = value; }
    //public bool IsEnemyDead { get => _isEnemyDead; set => _isEnemyDead = value; }
    //public RaycastHit Hit { get => hit; }
}
