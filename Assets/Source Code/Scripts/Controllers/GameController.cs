using Map;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static GameController instance = null;
    public bool spawned = false;

    MapController mapController;

    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameController").AddComponent<GameController>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        MapLayout teste = new MapLayout(new Vector2Int(10, 10));
        Dictionary<Vector2Int, Tile> tiles = teste.tiles;
        tiles.Add(new Vector2Int(0, 0), new Tile(TileTypeEnum.SPAWNPOINT, 0));
        tiles.Add(new Vector2Int(1, 0), new Tile(TileTypeEnum.ROAD, (int)RoadVarietyEnum.STRAIGHT_HORIZONTAL));
        tiles.Add(new Vector2Int(2, 0), new Tile(TileTypeEnum.ROAD, (int)RoadVarietyEnum.CORNER_TOP_LEFT));
        tiles.Add(new Vector2Int(2, 1), new Tile(TileTypeEnum.ROAD, (int)RoadVarietyEnum.STRAIGHT_VERTICAL));
        tiles.Add(new Vector2Int(2, 2), new Tile(TileTypeEnum.ROAD, (int)RoadVarietyEnum.CORNER_BOTTOM_RIGHT));
        tiles.Add(new Vector2Int(3, 2), new Tile(TileTypeEnum.ROAD, (int)RoadVarietyEnum.STRAIGHT_HORIZONTAL));
        tiles.Add(new Vector2Int(4, 2), new Tile(TileTypeEnum.ROAD, (int)RoadVarietyEnum.CORNER_BOTTOM_LEFT));
        tiles.Add(new Vector2Int(4, 1), new Tile(TileTypeEnum.ROAD, (int)RoadVarietyEnum.STRAIGHT_VERTICAL));
        tiles.Add(new Vector2Int(4, 0), new Tile(TileTypeEnum.ROAD, (int)RoadVarietyEnum.CORNER_TOP_RIGHT));
        tiles.Add(new Vector2Int(5, 0), new Tile(TileTypeEnum.ROAD, (int)RoadVarietyEnum.STRAIGHT_HORIZONTAL));
        tiles.Add(new Vector2Int(6, 0), new Tile(TileTypeEnum.BARRACKS, 0));
        tiles.Add(new Vector2Int(3, 1), new Tile(TileTypeEnum.TOWER, (int)TowerVarietyEnum.ARROW_TOWER));

        MapController mc = gameObject.AddComponent<MapController>();
        this.mapController = mc;
        mc.GenerateMap(teste);
    }

    void Update()
    {
        if (!spawned)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                mapController.SummonSoldiers(testeSoldiers1());
                spawned = true;
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                mapController.SummonSoldiers(testeSoldiers2());
                spawned = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Comma))
            Time.timeScale -= 0.5f;

        if (Input.GetKeyDown(KeyCode.Period))
            Time.timeScale += 0.5f;
    }

    static IEnumerator<SoldierTypeEnum> testeSoldiers1()
    {
        while (true)
            yield return SoldierTypeEnum.BASIC;
    }

    static IEnumerator<SoldierTypeEnum> testeSoldiers2()
    {
        yield return SoldierTypeEnum.TOUGH;
        while (true)
            yield return SoldierTypeEnum.BASIC;
    }
}

