using System.Collections;
using UnityEngine;

namespace AStarVisualization
{
    public class DraggableNode : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ObstacleRenderer obstacleRenderer = null;

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Drag());
            }
        }

        private IEnumerator Drag()
        {
            Vector2Int mousePosition;
            Vector2Int lastMousePosition = Mouse.Position();

            // While button pressed, continue to drag node
            while (Input.GetMouseButton(0))
            {
                mousePosition = Mouse.Position();

                if (mousePosition != lastMousePosition && obstacleRenderer.IsEmpty(mousePosition))
                {
                    MoveTo(mousePosition);
                }

                lastMousePosition = mousePosition;

                yield return null;
            }
        }

        private void MoveTo(Vector2Int position)
        {
            transform.position = (Vector3Int)position;
        }
    }
}
