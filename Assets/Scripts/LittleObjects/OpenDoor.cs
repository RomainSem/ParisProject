using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _messageToPlayer;
    [SerializeField] Item[] _itemsNeededToOpenDoor;
    [SerializeField] bool _isSpecialDoor;

    private void Start()
    {
        _objectsEffects = GameObject.Find("GameManager").GetComponent<ObjectsEffects>();
        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    private void Update()
    {
        posToGo = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 10, gameObject.transform.position.z);
        if (_isSpecialDoorOpen)
        {
            //Destroy(gameObject);
            StartCoroutine(MoveDoor(posToGo, 3f));
            if (Vector3.Distance(gameObject.transform.position, posToGo) < 2)
            {
                _isSpecialDoorOpen = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_isSpecialDoor)
            {
                List<Item> tempList = new List<Item>();
                foreach (Item item in _inventoryManager.PlayerPossessedItems)
                {
                    if (_itemsNeededToOpenDoor.Contains(item))
                    {
                        tempList.Add(item);
                        if (_itemsNeededToOpenDoor.Length == tempList.Count)
                        {
                            _isSpecialDoorOpen = true;
                        }
                    }
                }
                if (!_isSpecialDoorOpen)
                {
                    _messageToPlayer.gameObject.SetActive(true);
                    _messageToPlayer.text = "It seems like I need a few things to open this door";
                    StartCoroutine(ResetMessage());
                }
                
            }
            else if (!_isSpecialDoor && _objectsEffects.NbOfKeys > 0)
            {
                _objectsEffects.NbOfKeys--;
                if (!_isMoving) // Vérifier si l'objet est déjà en mouvement
                {
                    //Destroy(gameObject);
                    StartCoroutine(MoveDoor(posToGo, 3f));
                }
            }
            else if (collision.gameObject.CompareTag("Player") && _objectsEffects.NbOfKeys == 0)
            {
                _messageToPlayer.gameObject.SetActive(true);
                _messageToPlayer.text = "I need a key to open this door";
                StartCoroutine(ResetMessage());
            }
        }
    }

    IEnumerator MoveDoor(Vector3 targetPosition, float duration)
    {
        _isMoving = true; // Définir l'état de déplacement sur true pour éviter les mouvements simultanés

        Vector3 startPosition = gameObject.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assurez-vous que l'objet atteigne la position finale exacte
        gameObject.transform.position = targetPosition;

        _isMoving = false; // Définir l'état de déplacement sur false une fois le mouvement terminé
    }

    IEnumerator ResetMessage()
    {
        yield return new WaitForSeconds(3);
        _messageToPlayer.text = "";
        _messageToPlayer.gameObject.SetActive(false);
    }

    ObjectsEffects _objectsEffects;
    InventoryManager _inventoryManager;
    bool _isSpecialDoorOpen;
    private bool _isMoving = false; // Ajout d'une variable pour suivre l'état de déplacement
    Vector3 posToGo;


    public bool IsSpecialDoorOpen { get => _isSpecialDoorOpen; set => _isSpecialDoorOpen = value; }
}
