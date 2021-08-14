using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour, Damageable
{

    public float maxLife = 20;
    public float currLife;


    // Use this for initialization
    void Start()
    {
        currLife = maxLife;
    }
    public void TakeDamage(float damage, DamageType damageType)
    {
        currLife -= damage;
        if (currLife <= 0)
            OutOfLife();
    }

    protected virtual void OutOfLife()
    {
        Destroy(gameObject);
    }

}
