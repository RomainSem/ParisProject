using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsEffects : MonoBehaviour
{
    private void Start()
    {
        _player = GameObject.Find("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
    }


    public void UseCupOfCoffee()
    {
        StartCoroutine(CupOfCoffeeHeal());
        //Debug.Log("You drank a cup of coffee");
    }

    IEnumerator CupOfCoffeeHeal()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            _playerHealth.Heal(1);
            //Debug.Log("You healed 1 HP");
        }
    }

    GameObject _player;
    PlayerHealth _playerHealth;
}
