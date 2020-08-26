using UnityEngine;

namespace AStarVisualization
{
    public class ObstacleTrigger : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ObstacleRenderer obstacleRenderer = null;
        [SerializeField] private Camera mainCamera = null;

        private void Update()
        {
            TriggerObstacle();
        }

        private void TriggerObstacle()
        {
            if (Input.GetMouseButtonDown(0))
            {
                obstacleRenderer.TriggerObstacle(MousePosition());
            }
        }

        // Returns the bottom left corner of the tile currently at the mouse position
        private Vector2Int MousePosition()
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            
            int x = Mathf.FloorToInt(mousePos.x);
            int y = Mathf.FloorToInt(mousePos.y);
            
            return new Vector2Int(x, y);
        }
    }
}
