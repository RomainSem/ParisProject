using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerResources", menuName = "PlayerResources/Create New PlayerResources")]
public class PlayerResources : ScriptableObject
{
    [SerializeField] int nbOfResource = 0;

    public int NbOfResource { get => nbOfResource; set => nbOfResource = value; }
}
