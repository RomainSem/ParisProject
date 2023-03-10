using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    [SerializeField] string itemName;
    public int value;
    [SerializeField] Sprite icon;

    public Sprite Icon { get => icon; set => icon = value; }
    public string ItemName { get => itemName; set => itemName = value; }
}
