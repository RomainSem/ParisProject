using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //[HideInInspector]
    [SerializeField] Item _item;
    [HideInInspector]
    [SerializeField] int _quantity = 1;

    private void Awake()
    {
        _canvas = GameObject.Find("Canvas");
        _itemDescUI = _canvas.transform.Find("ItemDescPanel").gameObject;
        _enemyInventory = GameObject.FindGameObjectWithTag("EnemyInventory");
        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    private void Start()
    {
        _playerAimScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAim>();
        _itemDescText = _itemDescUI.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
        _itemDescTextGreen = _itemDescUI.transform.Find("ItemDescriptionGreen").GetComponent<TextMeshProUGUI>();
        _itemName = _itemDescUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_isMouseOver && !_playerAimScript.IsAiming)
        {
            _itemDescUI.SetActive(true);
            _itemDescUI.transform.position = new Vector2(Input.mousePosition.x + 97, Input.mousePosition.y + 44);
            _itemDescText.text = _item.Description;
            _itemDescTextGreen.text = _item.DescriptionGreen;
            _itemName.text = _item.ItemName;
            if (gameObject.transform.parent == _enemyInventory && Input.GetMouseButtonDown(0))
            {
                _inventoryManager.AddItem(_item);
            }
        }
    }

    public void InitialiseItem(Item newItem)
    {
        _objectEffects = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObjectsEffects>();
        _itemIcon = transform.Find("ItemIcon").GetComponent<Image>();
        if (_item == null)
        {
            _item = newItem;
            _itemIcon.sprite = newItem.Icon;
            switch (newItem.ItemName)
            {
                case "Cup of coffee":
                    _objectEffects.UseCupOfCoffee();
                    break;
                //case "Sword":
                //    Debug.Log("You equipped a sword");
                //    break;
                //case "Shield":
                //    Debug.Log("You equipped a shield");
                //    break;
                default:
                    Debug.Log("You used an item");
                    break;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isMouseOver = false;
    }

    Image _itemIcon;
    bool _isMouseOver;
    PlayerAim _playerAimScript;
    ObjectsEffects _objectEffects;
    GameObject _itemDescUI;
    GameObject _enemyInventory;
    GameObject _canvas;
    TextMeshProUGUI _itemDescText;
    TextMeshProUGUI _itemDescTextGreen;
    TextMeshProUGUI _itemName;
    InventoryManager _inventoryManager;

    public Item Item { get => _item; set => _item = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }
}
