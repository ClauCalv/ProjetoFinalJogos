using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float maxLifetime = -1;

    protected virtual void Start()
    {
        StartCoroutine(EndOfLifetime());
    }

    IEnumerator EndOfLifetime()
    {
        if(maxLifetime > 0)
        {
            yield return new WaitForSeconds(maxLifetime);
            DestroyOnLifetime();
        }
        yield break;
    }

    protected virtual void DestroyOnLifetime()
    {
        Destroy(gameObject);
    }
}
