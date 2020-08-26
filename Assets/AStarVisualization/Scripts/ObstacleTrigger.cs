using System.Collections;
using UnityEngine;

namespace AStarVisualization
{
    

    public class ObstacleTrigger : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ObstacleRenderer obstacleRenderer = null;

        private void Update()
        {
            TriggerObstacle();
        }

        private void TriggerObstacle()
        {
            if (Input.GetMouseButtonDown(0))
            {
                obstacleRenderer.TriggerObstacle(Mouse.Position());

                StartCoroutine(ContinueTriggerObstacle());
            }
        }

        private IEnumerator ContinueTriggerObstacle()
        {
            Vector2Int lastMousePosition = Mouse.Position();

            // While button pressed, continue to trigger obstacles
            while (Input.GetMouseButton(0))
            {
                Vector2Int mousePosition = Mouse.Position();

                if (mousePosition != lastMousePosition)
                {
                    obstacleRenderer.TriggerObstacle(mousePosition);
                }

                lastMousePosition = mousePosition;

                yield return null;
            }
        }
    }
}
