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
            }
            else
            {
                currTime -= firingInterval;
                DoShoot();
            }
        }
        timeSincelastShot = currTime;

    }

    private void DoShoot()
    {
        Transform targetTransform = null;
        float maxDistance = -1;

        foreach(GameObject target in targets)
        {
            PathFollower pf = target.GetComponent<PathFollower>();
            if(pf.distanceTraveled > maxDistance)
            {
                targetTransform = target.transform;
                maxDistance = pf.distanceTraveled;
            }
        }

        Vector3 destination = targetTransform.position;

        GameObject projectile = GameObject.Instantiate(arrowProjectile);
        MoveStraightProjectile projectileScript = projectile.GetComponent<MoveStraightProjectile>();
        projectile.transform.position = transform.position;
        projectileScript.destination = destination;
    }

    public void setTargets(List<GameObject> targets)
    {
        this.targets = targets;
    }
}
