using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    private void Start()
    {
        _objectsEffects = GameObject.Find("GameManager").GetComponent<ObjectsEffects>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _objectsEffects.NbOfKeys > 0)
        {
            _objectsEffects.NbOfKeys--;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 10, gameObject.transform.position.z), 1);
        }
    }

    ObjectsEffects _objectsEffects;
}
