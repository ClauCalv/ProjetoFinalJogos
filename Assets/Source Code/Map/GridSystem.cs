using UnityEngine;

namespace Map
{
    public struct GridSystem
    {

        public float scale;
        public Vector2Int size;
        public Vector3 center;
        public GridSystem(float scale, Vector2Int size, Vector3 center)
        {
            this.scale = scale;
            this.size = size;
            this.center = center;
        }

        public Vector3 GetPosFromCoords(Vector2Int coords)
        {
            Vector2 actual = coords + (Vector2.one / 2);
            Vector3 pos = center + new Vector3(actual.x * scale, 0, actual.y * scale);

            return pos;
        }

        public Vector2Int GetNearestCoordFromPos(Vector3 pos)
        {
            Vector3 local = (pos - center) / scale;
            Vector2Int coords = new Vector2Int((int)Mathf.Floor(local.x), (int)Mathf.Floor(local.y));

            return coords;
        }

    }
}