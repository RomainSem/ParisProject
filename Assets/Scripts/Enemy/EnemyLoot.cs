using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [SerializeField] GameObject _lootMenu;

    private void Start()
    {
        _enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    private void OnMouseDown()
    {
        if (_enemyBehaviour.IsEnemyDead)
        {
            _lootMenu.SetActive(true);
        }
    }

    EnemyBehaviour _enemyBehaviour;
}
