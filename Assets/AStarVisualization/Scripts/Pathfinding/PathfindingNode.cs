using UnityEngine;

namespace AStarVisualization
{
    public class PathfindingNode
    {
        public Vector2Int position {get;}
        public int steps {get; set;}
        public int endDistance {get;}

        public PathfindingNode(Vector2Int position, int steps, int endDistance)
        {
            this.position = position;
            this.steps = steps;
            this.endDistance = endDistance;
        }

        public int TotalDistance()
        {
            return steps + endDistance;
        }
    }
}
