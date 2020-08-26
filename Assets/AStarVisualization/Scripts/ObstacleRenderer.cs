using System.Collections.Generic;
using UnityEngine;

namespace AStarVisualization
{
    [RequireComponent(typeof(MeshFilter))]
    public class ObstacleRenderer : MonoBehaviour
    {
        public List<Vector2Int> obstacles {get; private set;} = new List<Vector2Int>();

        private List<Vector3> vertices = new List<Vector3>();
        private List<int> triangles = new List<int>();

        private Mesh mesh;

        private void Start()
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            mesh = meshFilter.mesh;
        }

        public void TriggerObstacle(Vector2Int position)
        {
            if (obstacles.Contains(position))
            {
                RemoveObstacle(position);
            }
            else
            {
                CreateObstacle(position);
            }
        }

        private void RemoveObstacle(Vector2Int position)
        {
            RemoveVertices(position);
            UpdateMesh();

            obstacles.Remove(position);
        }

        private void RemoveVertices(Vector2Int position)
        {
            int obstacleIndex = obstacles.IndexOf(position);
            int positionIndex = obstacleIndex * 4;

            for (int i = 0; i < 4; i++)
            {
                vertices.RemoveAt(positionIndex);
            }

            RemoveTriangles();
        }

        private void RemoveTriangles()
        {
            for (int i = 0; i < 6; i++)
            {
                triangles.RemoveAt(triangles.Count - 1);
            }
        }

        private void CreateObstacle(Vector2Int position)
        {
            obstacles.Add(position);

            CreateVertices(position);
            UpdateMesh();
        }

        private void CreateVertices(Vector2Int position)
        {
            Vector3 pos = (Vector3Int)position;

            vertices.Add(pos);
            vertices.Add(pos + Vector3.up);
            vertices.Add(pos + Vector3.one);
            vertices.Add(pos + Vector3.right);

            CreateTriangles();
        }

        private void CreateTriangles()
        {
            int vertIndex = vertices.Count - 4;

            triangles.Add(vertIndex);
            triangles.Add(vertIndex + 1);
            triangles.Add(vertIndex + 2);

            triangles.Add(vertIndex);
            triangles.Add(vertIndex + 2);
            triangles.Add(vertIndex + 3);
        }

        private void UpdateMesh()
        {
            mesh.Clear();

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
        }
    }
}
