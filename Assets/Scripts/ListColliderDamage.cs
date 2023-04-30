using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListColliderDamage : MonoBehaviour
{

    private void Start()
    {
        _armCollider = GetComponent<BoxCollider>();
        _enemyBehaviour = GetComponentInParent<EnemyBehaviour>();
    }

    private void Update()
    {
        if (_armCollider.enabled)
        {
            alreadyCollidedWith.Clear();
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!alreadyCollidedWith.Contains(collision))
            {
                alreadyCollidedWith.Add(collision);
                collision.gameObject.GetComponent<PlayerHealth>().LoseHP(_enemyBehaviour.Damage);
            }
        }
    }



    BoxCollider _armCollider;
    EnemyBehaviour _enemyBehaviour;
    [SerializeField]
    List<Collider> alreadyCollidedWith = new List<Collider>();

    public List<Collider> AlreadyCollidedWith { get => alreadyCollidedWith; set => alreadyCollidedWith = value; }
}
