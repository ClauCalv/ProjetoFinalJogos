using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Soldier : MonoBehaviour, Damageable
{
    public static List<Soldier> soldiers = new List<Soldier>();

    MapController mapController;
    Animator animator;

    public float maxLife = 20;
    public float currLife;

    public void Init(MapController mc)
    {
        this.mapController = mc;
        this.animator = GetComponentInChildren<Animator>();
        soldiers.Add(this);
    }

    void Start()
    {
        currLife = maxLife;
    }
    public virtual void TakeDamage(float damage, DamageType damageType)
    {
        currLife -= damage;
        if (currLife <= 0)
            OutOfLife();
    }

    public virtual void BarracksReached()
    {
        animator.SetBool("Walk", false);
        StartCoroutine(Death());
    }

    protected virtual void OutOfLife()
    {
        animator.SetTrigger("Death");
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        soldiers.Remove(this);
        if (soldiers.Count == 0)
        {
            mapController.NoMoreSoldiers();
        }

        Collider c = GetComponent<Collider>();
        c.enabled = false;

        PathFollower pf = GetComponent<PathFollower>();
        pf.stopMoving = true;

        float animTime = animator.GetCurrentAnimatorStateInfo(0).length +
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

        yield return new WaitForSeconds(animTime);

        Destroy(gameObject);
    }

}
