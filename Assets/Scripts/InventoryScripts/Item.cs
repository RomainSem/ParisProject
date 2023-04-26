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
    [TextArea(15, 20)]
    [SerializeField] string description;
    [TextArea(10, 10)]
    [SerializeField] string descriptionGreen;
    [SerializeField] bool isActivable;
    [SerializeField] bool isStackable;

    public Sprite Icon { get => icon; set => icon = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public string Description { get => description; set => description = value; }
    public string DescriptionGreen { get => descriptionGreen; set => descriptionGreen = value; }
    public bool IsActivable { get => isActivable; set => isActivable = value; }
    public bool IsStackable { get => isStackable; set => isStackable = value; }
}
