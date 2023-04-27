using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [HideInInspector]
    [SerializeField] Item _item;
    [HideInInspector]
    [SerializeField] int _quantity = 1;

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

    Image _itemIcon;
    ObjectsEffects _objectEffects;

    public Item Item { get => _item; set => _item = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }
}
