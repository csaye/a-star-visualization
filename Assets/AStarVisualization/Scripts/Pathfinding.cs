using System.Collections.Generic;
using UnityEngine;

namespace AStarVisualization
{
    public class Pathfinding : MonoBehaviour
    {
        // Steps which a path can take
        private List<Vector2Int> moveDirections = new List<Vector2Int>
        {
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.right,
            Vector2Int.down
        };

        // The maximum number of checks before failing to find a path
        private const int maxIterations = 1000;
        private int iterations = 0;

        // Returns shortest path from start position to end position
        private List<Vector2Int> Path(Vector2Int start, Vector2Int end)
        {
            List<Vector2Int> path = new List<Vector2Int>();

            return path;
        }
    }
}
