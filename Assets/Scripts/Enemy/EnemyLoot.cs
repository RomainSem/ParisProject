using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [SerializeField] GameObject _lootMenu;

    private void Start()
    {
        _enemyBehaviour = GetComponent<EnemyBehaviour>();
        _lootMenu.SetActive(false);
    }

    private void Update()
    {
        if (_enemyBehaviour.IsEnemyDead && _isMouseOver && Input.GetMouseButtonDown(0) && !_isLootMenuOpen)
        {
            _lootMenu.SetActive(true);
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
                _isMouseOver = true;
            }
            else
            {
                _isMouseOver = false;
            }
        }
    }

    public void CloseLootMenu()
    {
        _lootMenu.SetActive(false);
        _isLootMenuOpen = false;
    }

    EnemyBehaviour _enemyBehaviour;
    bool _isMouseOver;
    bool _isLootMenuOpen;
}
