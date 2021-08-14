using UnityEngine;
using System.Collections;

public abstract class ProjectileOnHitEffect : MonoBehaviour
{
    public LayerMask targetLayer;

    void OnTriggerEnter(Collider other)
    {
        if (targetLayer.ContainsLayer(other.gameObject.layer))
        {
            PerformHitEffect(other);
        }
    }

    protected abstract void PerformHitEffect(Collider other);
}
