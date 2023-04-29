using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyInventory", menuName = "Enemy/Create New Inventory")]
public class EnemyInventorySO : ScriptableObject
{
    [SerializeField] List<Item> _possibleItems = new List<Item>();
    //[SerializeField] List<Item> _itemsPossessed = new List<Item>();

    public List<Item> PossibleItems { get => _possibleItems; set => _possibleItems = value; }
}
