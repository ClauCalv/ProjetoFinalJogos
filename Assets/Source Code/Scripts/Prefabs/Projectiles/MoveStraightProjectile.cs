using UnityEngine;
using System.Collections;
using System;

public class MoveStraightProjectile : Projectile
{
    public float maxDistance = -1;
    public float distance;

    public Vector3 destination;
    public Vector3 direction;

    protected override void Start()
    {
        direction = destination - transform.position;
        direction.Normalize();
        Quaternion originalRotation = transform.rotation;
        transform.LookAt(destination);
        transform.rotation *= originalRotation;
        distance = 0;

        base.Start();
        StartCoroutine(EndOfMaxDistance());
        StartCoroutine(MoveStraight());
    }

    IEnumerator EndOfMaxDistance()
    {
        if (maxDistance > 0)
        {
            while (distance < maxDistance)
                yield return null;
            DestroyOnMaxDistance();
        }
        yield break;
    }

    IEnumerator MoveStraight()
    {
        while (true)
        {
            transform.position += direction * projectileSpeed * Time.deltaTime;
            distance += projectileSpeed * Time.deltaTime;
            yield return null;
        }
    }

    protected virtual void DestroyOnMaxDistance()
    {
        Destroy(gameObject);
    }

    


}
