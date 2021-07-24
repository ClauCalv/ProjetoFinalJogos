using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : MonoBehaviour
{
    public GameObject arrowProjectile;
    public float firingSpeed;

    List<GameObject> targets = new List<GameObject>();
    float firingInterval;
    float timeSincelastShot;

    // Start is called before the first frame update
    void Start()
    {
        firingInterval = 1 / firingSpeed;
        timeSincelastShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float currTime = timeSincelastShot + Time.deltaTime;
        if(currTime > firingInterval)
        {
            if (targets.Count == 0)
            {
                currTime = firingInterval;
                Debug.Log("Nothing to shoot at!");
            }
            else
            {
                currTime -= firingInterval;
                DoShoot();
                Debug.Log("Tower firing!");
            }
        }
        timeSincelastShot = currTime;

    }

    private void DoShoot()
    {
        GameObject target = targets[0]; //Needs target-choosing logic
        Vector3 destination = target.transform.position;

        GameObject projectile = GameObject.Instantiate(arrowProjectile);
        BaseProjectile projectileScript = projectile.GetComponent<BaseProjectile>();
        projectile.transform.position = transform.position;
        projectileScript.destination = destination;
    }

    public void setTargets(List<GameObject> targets)
    {
        this.targets = targets;
    }
}
