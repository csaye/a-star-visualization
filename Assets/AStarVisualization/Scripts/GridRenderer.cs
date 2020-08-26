using System.Collections.Generic;
using UnityEngine;

namespace AStarVisualization
{
    [RequireComponent(typeof(MeshFilter))]
    public class GridRenderer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera mainCamera = null;

        private const float lineThickness = 0.01f;
        private const float offscreenExtent = 1;

        private List<Vector3> vertices = new List<Vector3>();
        private List<int> triangles = new List<int>();

        private Vector2 screenMin;
        private Vector2 screenMax;

        private Vector3 lastCameraPosition = Vector3.zero;

        private Transform cameraTransform = null;

        private Mesh mesh;
        
        private void Start()
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            mesh = meshFilter.mesh;

            screenMin = Vector2.zero;
            screenMax = new Vector2(Screen.width, Screen.height);

            cameraTransform = mainCamera.transform;
        }

        private void Update()
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            if (cameraTransform.position == lastCameraPosition) return;

            UpdateVertices();
            UpdateTriangles();

            UpdateMesh();
        }

        private void UpdateVertices()
        {
            vertices.Clear();

            Vector2 gridMin = mainCamera.ScreenToWorldPoint(screenMin);
            Vector2 gridMax = mainCamera.ScreenToWorldPoint(screenMax);

            CreateVerticalLines(gridMin, gridMax);
            CreateHorizontalLines(gridMin, gridMax);
        }

        private void CreateVerticalLines(Vector2 gridMin, Vector2 gridMax)
        {
            int xMin = Mathf.CeilToInt(gridMin.x);
            int xMax = Mathf.FloorToInt(gridMax.x);

            for (int x = xMin; x <= xMax; x++)
            {
                Vector3 lowerLeft = new Vector3(x - lineThickness, gridMin.y - offscreenExtent, 0);
                Vector3 upperLeft = new Vector3(x - lineThickness, gridMax.y + offscreenExtent, 0);
                Vector3 upperRight = new Vector3(x + lineThickness, gridMax.y + offscreenExtent, 0);
                Vector3 lowerRight = new Vector3(x + lineThickness, gridMin.y - offscreenExtent, 0);

                vertices.Add(lowerLeft);
                vertices.Add(upperLeft);
                vertices.Add(upperRight);
                vertices.Add(lowerRight);
            }
        }

        private void CreateHorizontalLines(Vector2 gridMin, Vector2 gridMax)
        {
            int yMin = Mathf.CeilToInt(gridMin.y);
            int yMax = Mathf.FloorToInt(gridMax.y);

            for (int y = yMin; y <= yMax; y++)
            {
                Vector3 lowerLeft = new Vector3(gridMin.x - offscreenExtent, y - lineThickness, 0);
                Vector3 upperLeft = new Vector3(gridMin.x - offscreenExtent, y + lineThickness, 0);
                Vector3 upperRight = new Vector3(gridMax.x + offscreenExtent, y + lineThickness, 0);
                Vector3 lowerRight = new Vector3(gridMax.x + offscreenExtent, y - lineThickness, 0);

                vertices.Add(lowerLeft);
                vertices.Add(upperLeft);
                vertices.Add(upperRight);
                vertices.Add(lowerRight);
            }
        }

        private void UpdateTriangles()
        {
            triangles.Clear();

            for (int i = 0; i < vertices.Count; i += 4)
            {
                triangles.Add(i);
                triangles.Add(i + 1);
                triangles.Add(i + 2);

                triangles.Add(i);
                triangles.Add(i + 2);
                triangles.Add(i + 3);
            }
        }

        private void UpdateMesh()
        {
            mesh.Clear();

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
        }
    }
}
