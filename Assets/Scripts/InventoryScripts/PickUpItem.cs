using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] Item _itemToPickup;

    private void Awake()
    {
        _layerMask = LayerMask.GetMask("EnemyCone");
        _layerMask = ~_layerMask;
        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        _itemDescUI = GameObject.Find("ItemDescPanel");
        _itemDescText = _itemDescUI.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
        _itemDescTextGreen = _itemDescUI.transform.Find("ItemDescriptionGreen").GetComponent<TextMeshProUGUI>();
        _itemName = _itemDescUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        _nbItemsInScene = GameObject.FindGameObjectsWithTag("Item").Length;
        _playerAimScript = GameObject.Find("Player").GetComponent<PlayerAim>();
    }
    private void Start()
    {
       
    }

    private void Update()
    {
        if (_isMouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickupItem();
                gameObject.transform.position = new Vector3(0, 999999999, 0);
                if (_nbItemsInScene > 1)
                {
                    Destroy(gameObject, 1);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        RaycastToMouse();
    }

    private void PickupItem()
    {
        _inventoryManager.AddItem(_itemToPickup);
    }

    private void GetSelectedItem()
    {
        Item receivedItem = _inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("RECEIVED ITEM " + receivedItem);
        }
        else
        {
            Debug.Log("NO ITEM RECEIVED");
        }
    }

    private void UseSelectedItem()
    {
        Item usedItem = _inventoryManager.GetSelectedItem(true);
        if (usedItem != null)
        {
            Debug.Log("USED ITEM " + usedItem);
        }
        else
        {
            Debug.Log("NO ITEM USED");
        }
    }

    void RaycastToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, _layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Debug.Log("HIT " + hit.collider.gameObject.name);
            if (hit.collider.CompareTag("Item") && !_playerAimScript.IsAiming)
            {
                _itemDescUI.SetActive(true);
                _itemDescUI.transform.position = new Vector2(Input.mousePosition.x - 97, Input.mousePosition.y + 44);
                _itemDescText.text = _itemToPickup.Description;
                _itemDescTextGreen.text = _itemToPickup.DescriptionGreen;
                _itemName.text = _itemToPickup.ItemName;
                _isMouseOver = true;
            }
            else
            {
                _isMouseOver = false;
                _itemDescUI.SetActive(false);
            }
        }
    }

    LayerMask _layerMask;
    bool _isMouseOver;
    int _nbItemsInScene;
    GameObject _itemDescUI;
    TextMeshProUGUI _itemDescText;
    TextMeshProUGUI _itemName;
    TextMeshProUGUI _itemDescTextGreen;
    InventoryManager _inventoryManager;
    PlayerAim _playerAimScript;

}
