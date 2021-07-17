using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{

    public float gridScale = 2;

    Vector2 size;
    Vector3 center;
    void Awake()
    {
        Vector3 scale = transform.localScale;
        this.size = new Vector2(scale.x, scale.z) * 10 / gridScale;
        this.center = transform.position;
    }

    public Vector3 GetPosFromCoords (Vector2 coords)
    {
        Vector2 actual = coords - (size / 2) + (Vector2.one / 2);
        Vector3 pos = center + new Vector3(actual.x * gridScale, 0, actual.y * gridScale);

        return pos;
    }

    public Vector2 GetNearestCoordFromPos (Vector3 pos)
    {
        Vector3 local = pos - center;
        Vector2 coords = new Vector2(Mathf.Floor(local.x), Mathf.Floor(local.y)) + (size / 2);

        return coords;
    }

}
