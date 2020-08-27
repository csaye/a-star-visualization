using UnityEngine;

namespace AStarVisualization
{
    public class DrawPathButton : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Pathfinding pathfinding = null;
        [SerializeField] private Transform startingNodeTransform = null;
        [SerializeField] private Transform endingNodeTransform = null;

        public void DrawPath()
        {
            Vector2Int startingPosition = ToVector2Int(startingNodeTransform.position);
            Vector2Int endingPosition = ToVector2Int(endingNodeTransform.position);
            
            pathfinding.DrawPath(startingPosition, endingPosition);
        }

        private Vector2Int ToVector2Int(Vector3 v3)
        {
            return new Vector2Int(Mathf.RoundToInt(v3.x), Mathf.RoundToInt(v3.y));
        }
    }
}
