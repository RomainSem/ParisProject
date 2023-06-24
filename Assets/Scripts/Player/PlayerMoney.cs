using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] PlayerResources _money;
    [SerializeField] PlayerResources _nbOfKeys;

    private void Start()
    {
        NbOfKeys.NbOfResource = 0;
    }

    public PlayerResources Money { get => _money; set => _money = value; }
    public PlayerResources NbOfKeys { get => _nbOfKeys; set => _nbOfKeys = value; }
}
