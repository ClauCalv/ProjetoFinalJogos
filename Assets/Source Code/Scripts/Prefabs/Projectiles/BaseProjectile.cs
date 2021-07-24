using UnityEngine;
using System.Collections;
using System;

public class BaseProjectile : MonoBehaviour
{
    public float projectileSpeed;
    public float maxDistance;

    public Vector3 destination;
    public Vector3 direction;
    public float distance;
    // Use this for initialization
    void Start()
    {
        Debug.Log("Projectile spawned towards " + destination + "!");
        direction = destination - transform.position;
        direction.Normalize();
        Quaternion originalRotation = transform.rotation;
        transform.LookAt(destination);
        transform.rotation *= originalRotation;
        distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(distance > maxDistance)
        {
            Debug.Log("Projectile faded away!");
            DestroyOnFadeAnim();
        }

        transform.position += direction * projectileSpeed * Time.deltaTime;
        distance += projectileSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile just hit " + other.gameObject.name + "!");
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerTroops"))
        {
            DestroyOnHitAnim();
        }
    }

    //TODO
    void DestroyOnHitAnim()
    {
        Destroy(gameObject);
    }

    void DestroyOnFadeAnim()
    {
        Destroy(gameObject);
    }

}
