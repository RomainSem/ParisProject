using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    //[HideInInspector]
    [SerializeField] Item _item;
    [HideInInspector]
    [SerializeField] int _quantity = 1;

    public void InitialiseItem(Item newItem)
    {
        _itemIcon = transform.Find("ItemIcon").GetComponent<Image>();
        if (_item == null)
        {
            _item = newItem;
            _itemIcon.sprite = newItem.Icon;
        }
    }

    Image _itemIcon;

    public Item Item { get => _item; set => _item = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }
}
