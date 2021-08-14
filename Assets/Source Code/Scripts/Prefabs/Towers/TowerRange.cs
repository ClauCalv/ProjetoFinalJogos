using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    ArrowTower tower;
    List<GameObject> targets;

    LayerMask mask;
    void Awake()
    {
        tower = transform.parent.GetComponent<ArrowTower>();
        mask = LayerMask.GetMask("PlayerTroops");

        targets = new List<GameObject>();
    }

    void FixedUpdate()
    {
        tower.setTargets(targets);
        targets = new List<GameObject>();
    }

    void OnTriggerStay(Collider other)
    {
        int layerCollision = 1 << other.gameObject.layer;
        if ((mask.value & layerCollision) > 0)
        {
            targets.Add(other.gameObject);
        }
    }
}
