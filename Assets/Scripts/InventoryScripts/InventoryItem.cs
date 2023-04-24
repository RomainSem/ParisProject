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

    private void Start()
    {
        _itemIcon = transform.Find("ItemIcon").GetComponent<Image>();
        InitialiseItem(_item);
    }

    //private void Update()
    //{
    //    InitialiseItem(_item);
    //}

    public void InitialiseItem(Item newItem)
    {
        //if (_item != null)
        //{
        _item = newItem;
        Debug.Log("_ITEM " + _item);
        Debug.Log("NEW ITEM " + newItem);

        _itemIcon.sprite = newItem.Icon;
        //}
    }

    Image _itemIcon;
}
