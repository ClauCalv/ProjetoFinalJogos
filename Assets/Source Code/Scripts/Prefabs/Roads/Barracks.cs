using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour 
{
    public LayerMask targetLayer;
    public int maxSoldiers = 2;
    public int soldiers = 0;

    MapController mapController;

    public void Init (MapController mapController)
    {
        this.mapController = mapController;
    }
    void OnTriggerEnter(Collider other)
    {
        if (targetLayer.ContainsLayer(other.gameObject.layer))
        {
            Soldier soldier = other.gameObject.GetComponent<Soldier>();
            soldier.BarracksReached();

            soldiers++;
            if(soldiers >= maxSoldiers)
            {
                mapController.BarracksDestroyed();
            }
        }
    }
}
