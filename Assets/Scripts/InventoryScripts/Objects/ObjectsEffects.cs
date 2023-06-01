using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectsEffects : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _nbOfKeysText;
    [SerializeField] TextMeshProUGUI _nbOfCoinsText;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        _nbOfKeysText.text = _nbOfKeys.ToString();
        _nbOfCoinsText.text = _nbOfCoins.ToString();
    }

    public void UseCupOfCoffee()
    {
        StartCoroutine(CupOfCoffeeHeal());
        Debug.Log("You drank a cup of coffee");
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

    public void UseAmmoBox()
    {
        int rand = Random.Range(4, 8);
        _player.GetComponentInChildren<SimpleShoot>().CurrentNbBulletsInMagazine += rand;
        Debug.Log("You added : " + rand);
    }

    public void UseNormalKey()
    {
        Debug.Log("You used a normal key");
        _nbOfKeys++;
    }

    public void UseCoin()
    {
        Debug.Log("You used a coin");
        int rand = Random.Range(5, 10);
        _nbOfCoins += rand;
    }

    GameObject _player;
    PlayerHealth _playerHealth;
    int _nbOfKeys;
    int _nbOfCoins;

    public int NbOfKeys { get => _nbOfKeys; set => _nbOfKeys = value; }
}
