using Map;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static GameController instance = null;

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
        tiles.Add(Vector2Int.zero, new Tile(TileTypeEnum.ROAD, (int)RoadVarietyEnum.STRAIGHT_HORIZONTAL));
        tiles.Add(Vector2Int.right, new Tile(TileTypeEnum.ROAD, (int)RoadVarietyEnum.CORNER_TOP_LEFT));
        tiles.Add(Vector2Int.one, new Tile(TileTypeEnum.ROAD, (int)RoadVarietyEnum.STRAIGHT_VERTICAL));


        MapController mc = gameObject.AddComponent<MapController>();
        mc.GenerateMap(teste);
    }
}