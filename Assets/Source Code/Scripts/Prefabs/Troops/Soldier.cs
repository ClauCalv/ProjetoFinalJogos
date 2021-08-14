using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Soldier : MonoBehaviour, Damageable
{
    public static List<Soldier> soldiers = new List<Soldier>();

    MapController mapController;

    public float maxLife = 20;
    public float currLife;

    public void Init(MapController mc)
    {
        this.mapController = mc;
        soldiers.Add(this);
    }

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

    public virtual void BarracksReached()
    {
        OutOfLife();
    }

    protected virtual void OutOfLife()
    {
        Destroy(gameObject);

        soldiers.Remove(this);
        if(soldiers.Count == 0)
        {
            mapController.NoMoreSoldiers();
        }
    }

}
