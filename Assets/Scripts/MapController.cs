using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    GameController gameController;
    MapLayout map;
    GridSystem grid;

    public GameObject floor;

    public GameObject spawner;

    void Awake()
    {
        gameController = GameController.GetInstance();
        grid = floor.GetComponent<GridSystem>();
    }

    public void GenerateMap(MapLayout map)
    {
        this.map = map;

        spawner = new GameObject("Spawner");
        spawner.transform.position = grid.GetPosFromCoords(map.spawn);
        //TODO instantiate spawner

    }

}
