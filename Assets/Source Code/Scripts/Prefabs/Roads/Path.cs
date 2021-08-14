using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public static Dictionary<Vector2Int, Path> paths = new Dictionary<Vector2Int, Path>();
    private static Dictionary<Path, int> DijkstraNotVisited = null;
    private bool DijkstraVisited = false;

    public Vector2Int coord;
    public Direction connections;

    public bool isDestination = false;
    public int distance = int.MaxValue;
    public List<Neighbour> neighbours = null;

    public void Init(Vector2Int coord, Direction connections)
    {
        this.coord = coord;
        paths.Add(coord, this);

        this.connections = connections;
        neighbours = new List<Neighbour>(4);

        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            if (connections.HasFlag(dir))
            {
                Path neighbour = null;
                paths.TryGetValue(coord + dir.Vector(), out neighbour);
                if (neighbour != null){
                    neighbours.Add(new Neighbour(neighbour, dir));
                    neighbour.neighbours.Add(new Neighbour(this, dir.Opposite()));
                }
            }
        }
    }

    void OnDestroy()
    {
        paths.Remove(coord);

        //TODO neighbour-removing considerando o caso que todos são destruídos de uma vez e é desnecessário.
    }

    public Path smallerNeighbour()
    {
        Path minNeighbour = null;
        int minDistance = int.MaxValue;
        foreach(Neighbour n in neighbours)
        {
            if(n.path.distance < minDistance)
            {
                minDistance = n.path.distance;
                minNeighbour = n.path;
            }
        }

        return minNeighbour;
    }

    private bool DijkstraInit()
    {
        distance = isDestination ? 0 : int.MaxValue;
        DijkstraVisited = false;

        return isDestination;
    }

    private void DjikstraCalc()
    {
        foreach (Neighbour n in neighbours)
        {
            if (n.path.distance > distance + 1)
                n.path.distance = distance + 1;

            if (!n.path.DijkstraVisited)
                DijkstraNotVisited.Add(n.path, n.path.distance);
        }

        DijkstraNotVisited.Remove(this);
        DijkstraVisited = true;
    }

    public static void Dijkstra()
    {
        Path destination = null;

        foreach(Path p in paths.Values)
            if(p.DijkstraInit())
                destination = p;

        if (destination is null) return;

        DijkstraNotVisited = new Dictionary<Path, int>();
        DijkstraNotVisited.Add(destination, 0);
        
        while(DijkstraNotVisited.Count > 0)
        {
            Path min = null;
            int minVal = int.MaxValue;

            foreach(KeyValuePair<Path, int> entry in DijkstraNotVisited)
                if(entry.Value < minVal)
                    (min, minVal) = (entry.Key, entry.Value);

            min.DjikstraCalc();
        }

        DijkstraNotVisited = null;
    }

    public struct Neighbour
    {
        public Path path;
        public Direction direction;

        public Neighbour(Path p, Direction d) => (path, direction) = (p, d);
    }


}

[Flags]
public enum Direction
{
    TOP = 0b0001,
    BOTTOM = 0b0010,
    LEFT = 0b0100,
    RIGHT = 0b1000
}

// EU JÁ XINGUEI A TOTAL IMBECIBILIDADE DOS ENUMS DO C# HOJE?
public static class DirectionVectors
{
    public static Vector2Int Vector(this Direction d)
    {
        switch (d)
        {
            case Direction.TOP:
                return Vector2Int.up;
            case Direction.BOTTOM:
                return Vector2Int.down;
            case Direction.LEFT:
                return Vector2Int.left;
            case Direction.RIGHT:
                return Vector2Int.right;
        }
        return Vector2Int.zero;
    }

    public static Direction Opposite(this Direction d)
    {
        switch (d)
        {
            case Direction.TOP:
                return Direction.BOTTOM;
            case Direction.BOTTOM:
                return Direction.TOP;
            case Direction.LEFT:
                return Direction.RIGHT;
            case Direction.RIGHT:
                return Direction.LEFT;
        }
        return 0;
    }
}
