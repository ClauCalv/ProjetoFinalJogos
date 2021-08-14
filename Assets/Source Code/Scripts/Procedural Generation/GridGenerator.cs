using UnityEngine;
using System.Collections;
using Map;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GridGenerator : MonoBehaviour
{

    public Vector2Int size;
    public float scale;

    Vector3[] vertices;
    Mesh mesh;

    public void Init(GridSystem gridSystem)
    {
        this.size = gridSystem.size;
        this.scale = gridSystem.scale;

        Generate();
    }

    private void Generate()
    {
        mesh = new Mesh();
        mesh.name = "Grid";
        GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[(size.x + 1) * (size.y + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        for (int i = 0, y = 0; y <= size.y; y++)
            for (int x = 0; x <= size.x; i++, x++)
            {
                vertices[i] = new Vector3(x, 0, y) * scale;
                uv[i] = new Vector2((float) x / size.x, (float) y / size.y);
            }
        mesh.vertices = vertices;
        mesh.uv = uv;

        int[] triangles = new int[size.x * size.y * 6];
        for (int ti = 0, vi = 0, y = 0; y < size.y; y++, vi++)
            for (int x = 0; x < size.x; x++, ti +=6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 4] = vi + size.x + 1;
                triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                triangles[ti + 5] = vi + size.x + 2;
            }

        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshRenderer>().material.mainTextureScale = size;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        Gizmos.color = Color.green;
        for (int i = 0; i < vertices.Length; i++)
            Gizmos.DrawSphere(vertices[i], 0.1f);
    }
}
