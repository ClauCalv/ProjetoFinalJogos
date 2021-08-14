using UnityEngine;
using System.Collections;

public class ToughSoldier : Soldier
{

    public float armor = 1;

    public override void TakeDamage(float damage, DamageType damageType)
    {
        if(damageType == DamageType.PHYSICAL)
        {
            damage -= armor;
        }
        base.TakeDamage(damage, damageType);
    }
}
