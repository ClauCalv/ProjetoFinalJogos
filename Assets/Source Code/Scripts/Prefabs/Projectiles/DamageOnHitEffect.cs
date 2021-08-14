using UnityEngine;
using System.Collections;

public class DamageOnHitEffect : ProjectileOnHitEffect
{
    public float damage = 1;
    public DamageType damageType;
    public bool destroyOnHit = true;
    protected override void PerformHitEffect(Collider other)
    {
        //Damageable damageable = other.gameObject.GetComponent<Damageable>(); interfaces requerem não usar generics
        Damageable damageable = other.gameObject.GetComponent(typeof(Damageable)) as Damageable;
        damageable.TakeDamage(damage, damageType);

        if (destroyOnHit)
            DestroyOnHit();
    }

    protected virtual void DestroyOnHit()
    {
        Destroy(gameObject);
    }

}

// Tirar essas duas classes daqui e por em algum lugar
public interface Damageable
{
    void TakeDamage(float damage, DamageType damageType);
}

public enum DamageType
{
    PHYSICAL,
    MAGICAL
}
