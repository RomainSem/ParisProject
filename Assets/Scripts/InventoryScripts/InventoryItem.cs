using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    [SerializeField] Item _item;
    [HideInInspector]
    [SerializeField] int _quantity = 1;


    private void Start()
    {
        _itemUILayer = LayerMask.GetMask("UI");
        _itemDescUI = GameObject.Find("ItemDescPanel");
        _itemDescText = _itemDescUI.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
        _itemDescTextGreen = _itemDescUI.transform.Find("ItemDescriptionGreen").GetComponent<TextMeshProUGUI>();
        _itemName = _itemDescUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_isMouseOver)
        {
            _itemDescUI.SetActive(true);
            _itemDescUI.transform.position = new Vector2(Input.mousePosition.x + 97, Input.mousePosition.y + 44);
            _itemDescText.text = _item.Description;
            _itemDescTextGreen.text = _item.DescriptionGreen;
            _itemName.text = _item.ItemName;
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
                //case "Helmet":
                //    Debug.Log("You equipped a helmet");
                //    break;
                //case "Chestplate":
                //    Debug.Log("You equipped a chestplate");
                //    break;
                //case "Leggings":
                //    Debug.Log("You equipped leggings");
                //    break;
                //case "Boots":
                //    Debug.Log("You equipped boots");
                //    break;
                //case "Gloves":
                //    Debug.Log("You equipped gloves");
                //    break;
                //case "Ring":
                //    Debug.Log("You equipped a ring");
                //    break;
                //case "Amulet":
                //    Debug.Log("You equipped an amulet");
                //    break;
                //case "Potion":
                //    Debug.Log("You drank a potion");
                //    break;
                //case "Scroll":
                //    Debug.Log("You read a scroll");
                //    break;
                //case "Key":
                //    Debug.Log("You used a key");
                //    break;
                //case "Quest Item":
                //    Debug.Log("You used a quest item");
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
    LayerMask _itemUILayer;
    ObjectsEffects _objectEffects;
    GameObject _itemDescUI;
    TextMeshProUGUI _itemDescText;
    TextMeshProUGUI _itemDescTextGreen;
    TextMeshProUGUI _itemName;

    public Item Item { get => _item; set => _item = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }
}
