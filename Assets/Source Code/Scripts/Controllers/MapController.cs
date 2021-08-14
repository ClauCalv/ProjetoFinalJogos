using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

public class MapController : MonoBehaviour
{
    GameController gameController;

    MapLayout mapLayout;
    GridSystem gridSystem;
    float mapScale = 3;

    GridGenerator grid;
    CameraMovement camera;

    Path spawnpoint;
    IEnumerator<SoldierTypeEnum> soldiers;
    float spawnInterval = 2;
    private bool doneSpawning = false;

    void Awake()
    {
        gameController = GameController.Instance;

        this.grid = GameObject.FindWithTag("Grid").GetComponent<GridGenerator>();
        this.camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
    }

    public void GenerateMap(MapLayout mapLayout)
    {
        this.mapLayout = mapLayout;
        this.gridSystem = new GridSystem(mapScale, mapLayout.size, grid.transform.position);

        grid.Init(gridSystem);
        camera.Init(gridSystem);

        foreach (KeyValuePair<Vector2Int, Tile> keyValue in mapLayout.tiles)
        {
            Vector2Int coord = keyValue.Key;
            Tile tile = keyValue.Value;
            TileType tileType = tile.type.Type();

            switch (tileType.TTEnum)
            {
                case TileTypeEnum.ROAD:

                    RoadVarietyEnum varietyEnum = (RoadVarietyEnum) tile.variety;
                    RoadVariety variety = RoadVariety.fromEnum(varietyEnum);

                    GameObject prefab = findResource<GameObject>(tileType.ResourceFolder + tileType.BaseResource, false);
                    GameObject road = putPrefabOnGrid(prefab, coord);

                    //TODO
                    //Texture texture = findResource<Texture>(tileType.ResourceFolder + variety.ResourceName, false);
                    Texture texture = findResource<Texture>(tileType.ResourceFolder + "BASE_ROAD_TODO", false);
                    Renderer renderer = road.GetComponent<Renderer>();
                    renderer.material.mainTexture = texture;

                    Path path = road.GetComponent<Path>();
                    path.Init(coord, (Direction) varietyEnum);
                    break;

                case TileTypeEnum.SPAWNPOINT:
                case TileTypeEnum.BARRACKS:

                    GameObject prefab1 = findResource<GameObject>(tileType.ResourceFolder + tileType.BaseResource, false);
                    GameObject road1 = putPrefabOnGrid(prefab1, coord);

                    //TODO
                    Texture texture1 = findResource<Texture>(tileType.ResourceFolder + tileType.BaseResource + "_TODO", false);
                    Renderer renderer1 = road1.GetComponent<Renderer>();
                    renderer1.material.mainTexture = texture1;

                    Path path1 = road1.GetComponent<Path>();
                    path1.Init(coord, (Direction) 0b1111);

                    if (tileType.TTEnum == TileTypeEnum.SPAWNPOINT)
                        this.spawnpoint = path1;
                    else if(tileType.TTEnum == TileTypeEnum.BARRACKS)
                    {
                        Barracks barracks = path1.GetComponentInChildren<Barracks>();
                        barracks.Init(this);
                    }
                    break;


                case TileTypeEnum.TOWER:

                    TowerVarietyEnum varietyEnum1 = (TowerVarietyEnum)tile.variety;
                    TowerVariety variety1 = TowerVariety.fromEnum(varietyEnum1);

                    GameObject prefab2 = findResource<GameObject>(tileType.ResourceFolder + variety1.ResourceName, false);
                    GameObject tower = putPrefabOnGrid(prefab2, coord);

                    break;

            }
            
        }

        Path.Dijkstra();

    }

    public void SummonSoldiers(IEnumerator<SoldierTypeEnum> soldiers)
    {
        this.soldiers = soldiers;
        StartCoroutine(SummonSoldiers());
    }

    private IEnumerator SummonSoldiers()
    {
        for(int i = 0; i < mapLayout.maxPlayerUnits; i++)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (!soldiers.MoveNext()) break;

            SoldierType soldierType = soldiers.Current.Type();

            GameObject prefab = findResource<GameObject>(soldierType.ResourceFolder + soldierType.BaseResource, false);
            GameObject soldier = putPrefabOnGrid(prefab, spawnpoint.coord);

            PathFollower pf = soldier.GetComponent<PathFollower>();
            pf.Init(spawnpoint);
            Soldier s = soldier.GetComponent<Soldier>();
            s.Init(this);
        }

        doneSpawning = true;
        yield break;
    }

    public void NoMoreSoldiers()
    {
        if (doneSpawning)
        {
            //TODO LOSE GAME
            Debug.Log("Game lost");
        }
    }

    public void BarracksDestroyed()
    {
        //TODO WIN GAME
        Debug.Log("Game won");
    }

    private T findResource<T>(string address, bool takeRandom) where T : Object
    {
        if (takeRandom)
        {
            T[] objects = Resources.LoadAll<T>(address);
            int index = Random.Range(0, objects.Length);
            return objects[index];
        }
        else
        {
            return Resources.Load<T>(address);
        }
    }

    private GameObject putPrefabOnGrid(GameObject prefab, Vector2Int coord)
    {
        Vector3 pos = gridSystem.GetPosFromCoords(coord);

        GameObject tileObject = GameObject.Instantiate(prefab);
        tileObject.transform.position += pos;
        tileObject.transform.parent = grid.gameObject.transform;

        return tileObject;
    }



}
