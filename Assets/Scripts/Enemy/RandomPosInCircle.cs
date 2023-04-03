using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosInCircle : MonoBehaviour
{
    #region Exposed

    [SerializeField] float _radius = 4f;


    #endregion

    #region Unity Lifecycle

    private void Update()
    {
        GetRandomPos();
    }

    #endregion

    #region Methods

    private void GetRandomPos()
    {
        if (!IsPosGenerated)
        {
            Vector3 pos  = Random.insideUnitCircle * _radius;
            RandomPos = new Vector3(pos.x, 0, pos.y);
            IsPosGenerated = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    #endregion

    #region Private & Protected

    Vector3 randomPos;
    bool _isPosGenerated;

    public Vector3 RandomPos { get => randomPos; set => randomPos = value; }
    public bool IsPosGenerated { get => _isPosGenerated; set => _isPosGenerated = value; }

    #endregion
}
