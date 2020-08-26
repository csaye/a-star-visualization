using UnityEngine;

namespace AStarVisualization
{
    public class PathfindingNode : MonoBehaviour
    {
        public Vector2 position {get;}
        public int steps {get;}
        public int endDistance {get;}

        public PathfindingNode(Vector2 position, int steps, int endDistance)
        {
            this.position = position;
            this.steps = steps;
            this.endDistance = endDistance;
        }

        public int totalDistance()
        {
            return steps + endDistance;
        }
    }
}
