using System.Collections;
using UnityEngine;

namespace AStarVisualization
{
    public class ObstacleTrigger : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ObstacleRenderer obstacleRenderer = null;
        [SerializeField] private Pathfinding pathfinding = null;

        private void Update()
        {
            TriggerObstacle();
        }

        private void TriggerObstacle()
        {
            // If mouse button pressed, not currently drawing, area is empty, and mouse is not over UI
            if (Input.GetMouseButtonDown(0) && !pathfinding.drawing && obstacleRenderer.IsEmptyNodes(Mouse.Position()) && !Mouse.IsOverUI())
            {
                bool obstacleCreated = obstacleRenderer.TriggerObstacle(Mouse.Position());

                StartCoroutine(ContinueTriggerObstacle(obstacleCreated));
            }
        }

        private IEnumerator ContinueTriggerObstacle(bool createObstacle)
        {
            Vector2Int lastMousePosition = Mouse.Position();

            // While button pressed, continue to trigger obstacles
            while (Input.GetMouseButton(0))
            {
                Vector2Int mousePosition = Mouse.Position();

                if (mousePosition != lastMousePosition)
                {
                    TriggerObstacleSpecified(mousePosition, createObstacle);
                }

                lastMousePosition = mousePosition;

                yield return null;
            }
        }

        private void TriggerObstacleSpecified(Vector2Int mousePosition, bool createObstacle)
        {
            if (createObstacle)
            {
                if (obstacleRenderer.IsEmpty(mousePosition))
                {
                    obstacleRenderer.CreateObstacle(mousePosition);
                }
            }
            else
            {
                obstacleRenderer.RemoveObstacle(mousePosition);
            }
        }
    }
}
