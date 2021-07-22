using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [Serializable]
    public struct MapLayout
    {
        public Vector2Int size;
        public Dictionary<Vector2Int, Tile> tiles;
        public int maxPlayerUnits;

        public MapLayout(Vector2Int size)
        {
            this.size = size;
            this.tiles = new Dictionary<Vector2Int, Tile>();
            this.maxPlayerUnits = 3;
        }
    }

    [Serializable]
    public struct Tile
    {
        public TileTypeEnum type;
        public int variation;
    }

}

