using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    [SerializeField] Image _image;
    [SerializeField] Color _selectedColor;
    [SerializeField] Color _notSelectedColor;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        _image.color = _selectedColor;
    }

    public void Deselect()
    {
        _image.color = _notSelectedColor;
    }

}
