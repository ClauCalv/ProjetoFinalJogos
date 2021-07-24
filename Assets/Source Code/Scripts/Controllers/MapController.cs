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

    void Awake()
    {
        gameController = GameController.Instance;

        this.grid = GameObject.FindWithTag("Grid").GetComponent<GridGenerator>();
        this.camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
    }

    //modo genérico só tava dando bosta
    //    public void GenerateMap(MapLayout mapLayout)
    //   {
    //        this.mapLayout = mapLayout;
    //        this.gridSystem = new GridSystem(mapScale, mapLayout.size, grid.transform.position);

    //        grid.Init(gridSystem);
    //        camera.Init(gridSystem);

    //        foreach (KeyValuePair<Vector2Int, Tile> keyValue in mapLayout.tiles) {
    //            Vector2Int coord = keyValue.Key;
    //            Tile tile = keyValue.Value;

    //            TileType tileType = tile.type.Type();
    ////            System.Type enumType = tileType.VarietyEnumAssociated;

    //            GameObject prefab;

    ////            if (System.Enum.IsDefined(enumType, tile.variation)) {
    ////                // ENUMS NÃO HERDAM DE ENUM!!!:
    ////                // Enum varietyEnum = Enum.ToObject(enumType, value);
    ////                dynamic varietyEnum = System.Enum.ToObject(enumType, tile.variation);
    ////                // JÁ NÃO BASTA O ENUM SER UMA BOSTA, O GENERICS DO C# TEM QUE SER UMA BOSTA TBM
    ////                // Não dá pra simplesmente fazer:
    ////                // TileVariety<?> variety = varietyEnum.Type();
    ////                dynamic variety = varietyEnum.Type();
    ////                string resourceName = variety.ResourceName;
    ////
    ////                prefab = Resources.Load<GameObject>(tileType.ResourceFolder + resourceName);
    ////            }
    ////            else
    ////            {
    //                GameObject[] objects = Resources.LoadAll<GameObject>(tileType.ResourceFolder);
    //                int index = Random.Range(0, objects.Length);
    //                prefab = objects[index];
    ////            }

    //            Vector3 pos = gridSystem.GetPosFromCoords(coord);

    //            GameObject tileObject = GameObject.Instantiate(prefab);
    //            tileObject.transform.position = pos;
    //            tileObject.transform.parent = grid.gameObject.transform;
    //        }
    //    }

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
            }
            
        }

        Path.Dijkstra();

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
        tileObject.transform.position = pos;
        tileObject.transform.parent = grid.gameObject.transform;

        return tileObject;
    }



}
