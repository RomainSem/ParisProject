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
        _itemEffectText = _itemDescUI.transform.Find("ItemDescriptionGreen").GetComponent<TextMeshProUGUI>();
        _itemName = _itemDescUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        _nbItemsInScene = GameObject.FindGameObjectsWithTag("Item").Length;
        _interactPanel = GameObject.Find("InteractPanel");
        _playerDetected = GameObject.Find("Player").GetComponent<PlayerDetected>();
    }

    private void Start()
    {
        _interactPanel.SetActive(false);
    }

    private void Update()
    {
        if (_playerDetected.IsPlayerInLootZone && _playerDetected.ActualEnemyLoot == null)
        {
            _interactPanel.SetActive(true);
            ShowItemTooltip();

            if (Input.GetButtonDown("Use"))
            {
                PickupItem();
                gameObject.transform.position = new Vector3(0, 9999, 0);
                if (_nbItemsInScene > 1)
                {
                    Destroy(gameObject, 1);
                }
            }
        }
        else
        {
            _itemDescUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerDetected.IsPlayerInLootZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerDetected.IsPlayerInLootZone = false;
        }
    }

    private void PickupItem()
    {
        _inventoryManager.AddItem(_itemToPickup, "Player");
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

    private void ShowItemTooltip()
    {
        _itemDescUI.SetActive(true);
        _itemDescUI.transform.position = new Vector2(Screen.width / 2.5f, Screen.height / 1.5f);
        _itemName.text = _itemToPickup.ItemName;
        _itemDescText.text = _itemToPickup.Description;
        _itemEffectText.text = _itemToPickup.ItemEffect;
    }


    LayerMask _layerMask;
    int _nbItemsInScene;
    InventoryManager _inventoryManager;
    GameObject _itemDescUI;
    GameObject _interactPanel;
    TextMeshProUGUI _itemDescText;
    TextMeshProUGUI _itemName;
    TextMeshProUGUI _itemEffectText;
    PlayerDetected _playerDetected;

}
