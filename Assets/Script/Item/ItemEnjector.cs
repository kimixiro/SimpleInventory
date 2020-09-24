using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnjector : MonoBehaviour
{
    [SerializeField] private ItemObjectPool _pool;
    [SerializeField]private float _range;
    [SerializeField]private PolygonCollider2D _ground;
    

    public void EjectFormPool(IItem item, Vector3 position, Vector3 direction)
    {
        _range = Random.Range(1f, 4f);
        var presenter = _pool.Get(item);
        presenter.transform.position = position;
        
        var target = position + (direction.normalized * _range);
        target = _ground.bounds.ClosestPoint(target);
        
        presenter.gameObject.AddComponent<MovingAlongCurve>()
            .StartMoving(position, target, Vector3.Lerp(position, target, 0.5f) + new Vector3(0, 2, 0), 1)
            .RemoveWhenFinished();
    }
}
