using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarVisualization
{
    public class Pathfinding : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject openNodePrefab = null;
        [SerializeField] private GameObject closedNodePrefab = null;
        [SerializeField] private GameObject pathNodePrefab = null;
        [SerializeField] private ObstacleRenderer obstacleRenderer = null;

        // Steps which a path can take
        private List<Vector2Int> moveDirections = new List<Vector2Int>
        {
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.right,
            Vector2Int.down
        };

        private Dictionary<Vector2Int, GameObject> visualNodes = new Dictionary<Vector2Int, GameObject>();

        // The maximum number of checks before failing to find a path
        private const int maxIterations = 1000;
        private int iterations = 0;

        private const float stepDelay = 0.25f;

        public void DrawPath(Vector2Int start, Vector2Int end)
        {
            // // Create all new children from given path
            // foreach (Vector2Int position in Path(start, end))
            // {
            //     // Skip start and end positions
            //     if (position == start || position == end) continue;

            //     Instantiate(pathNodePrefab, (Vector3Int)position, Quaternion.identity, transform);
            // }
            StartCoroutine(DrawPathSteps(start, end));
        }

        private IEnumerator DrawPathSteps(Vector2Int start, Vector2Int end)
        {
            ClearAllChildren();

            List<Vector2Int> path = new List<Vector2Int>();

            List<PathfindingNode> openNodes = new List<PathfindingNode>();
            List<PathfindingNode> closedNodes = new List<PathfindingNode>();

            // Initialize current node to start position
            PathfindingNode current = new PathfindingNode(start, 0, Distance(start, end));
            openNodes.Add(current);

            CreateVisualNode(openNodePrefab, current.position);

            iterations = 0;

            while (current.position != end)
            {
                yield return new WaitForSeconds(stepDelay);

                // If maximum iterations surpassed and no path found, return empty list
                if (iterations >= maxIterations)
                {
                    ClearAllChildren();
                    yield break;
                }

                iterations++;

                int minTotalDistance = int.MaxValue;

                // Find open node with lowest total distance
                foreach (PathfindingNode node in openNodes)
                {
                    int totalDistance = node.TotalDistance();

                    if (totalDistance < minTotalDistance)
                    {
                        minTotalDistance = totalDistance;
                        current = node;
                    }
                    // If lowest total distance matched, take node with smallest distance to end
                    else if (totalDistance == minTotalDistance && Distance(node.position, end) < Distance(current.position, end))
                    {
                        current = node;
                    }
                }

                int lastSteps = current.steps;

                // Move current node from open to closed
                openNodes.Remove(current);
                closedNodes.Add(current);

                CreateVisualNode(closedNodePrefab, current.position);

                // Check each possible move direction to either update or create node
                foreach (Vector2Int moveDirection in moveDirections)
                {
                    Vector2Int move = current.position + moveDirection;

                    PathfindingNode openMove = openNodes.Find(node => node.position == move);
                    PathfindingNode closedMove = closedNodes.Find(node => node.position == move);

                    // If node closed or obstructed, skip node
                    if (closedMove != null || !obstacleRenderer.IsEmptyObstacles(move))
                    {
                        continue;
                    }
                    // If move open but steps could be smaller, update steps
                    else if (openMove != null && openMove.steps > lastSteps + 1)
                    {
                        openMove.steps = lastSteps + 1;
                    }
                    // If valid area, create new node and increment steps by one
                    else if (openMove == null && closedMove == null)
                    {
                        openNodes.Add(new PathfindingNode(move, lastSteps + 1, Distance(move, end)));

                        CreateVisualNode(openNodePrefab, move);
                    }
                }
            }

            // Start with end node and work towards start
            current = closedNodes[closedNodes.Count - 1];
            path.Add(current.position);

            // Create path back to start by choosing nodes with smallest distance to start
            while (current.position != start)
            {
                int minSteps = int.MaxValue;
                PathfindingNode bestMove = null;

                // Check each direction
                foreach (Vector2Int moveDirection in moveDirections)
                {
                    Vector2Int move = current.position + moveDirection;

                    PathfindingNode closedMove = closedNodes.Find(node => node.position == move);

                    // If move contains closed node, distance to start less than min, and position not already in path
                    if (closedMove != null && closedMove.steps < minSteps && !path.Contains(move))
                    {
                        // Set new minimum distance and move index
                        minSteps = closedMove.steps;
                        bestMove = closedMove;
                    }
                }

                // Set current node and add to path
                current = bestMove;
                path.Add(current.position);
            }

            foreach (Vector2Int position in path)
            {
                yield return new WaitForSeconds(stepDelay / 2);
                CreateVisualNode(pathNodePrefab, position);
            }

            path.Reverse();
        }

        private void ClearAllChildren()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void CreateVisualNode(GameObject prefab, Vector2Int position)
        {
            // If already visual node at position, remove it
            if (visualNodes.ContainsKey(position))
            {
                GameObject oldVisualNode;
                visualNodes.TryGetValue(position, out oldVisualNode);
                Destroy(oldVisualNode);
                visualNodes.Remove(position);
            }

            // Create new visual node of type given
            GameObject visualNode = Instantiate(prefab, (Vector3Int)position, Quaternion.identity, transform);
            visualNodes.Add(position, visualNode);
        }

        // // Returns shortest path from start position to end position
        // private List<Vector2Int> Path(Vector2Int start, Vector2Int end)
        // {
        //     List<Vector2Int> path = new List<Vector2Int>();

        //     List<PathfindingNode> openNodes = new List<PathfindingNode>();
        //     List<PathfindingNode> closedNodes = new List<PathfindingNode>();

        //     // Initialize current node to start position
        //     PathfindingNode current = new PathfindingNode(start, 0, Distance(start, end));
        //     openNodes.Add(current);

        //     iterations = 0;

        //     while (current.position != end)
        //     {
        //         // If maximum iterations surpassed and no path found, return empty list
        //         if (iterations >= maxIterations)
        //         {
        //             return new List<Vector2Int>();
        //         }

        //         iterations++;

        //         int minTotalDistance = int.MaxValue;

        //         // Find open node with lowest total distance
        //         foreach (PathfindingNode node in openNodes)
        //         {
        //             int totalDistance = node.TotalDistance();

        //             if (totalDistance < minTotalDistance)
        //             {
        //                 minTotalDistance = totalDistance;
        //                 current = node;
        //             }
        //             // If lowest total distance matched, take node with smallest distance to end
        //             else if (totalDistance == minTotalDistance && Distance(node.position, end) < Distance(current.position, end))
        //             {
        //                 current = node;
        //             }
        //         }

        //         int lastSteps = current.steps;

        //         // Move current node from open to closed
        //         openNodes.Remove(current);
        //         closedNodes.Add(current);

        //         // Check each possible move direction to either update or create node
        //         foreach (Vector2Int moveDirection in moveDirections)
        //         {
        //             Vector2Int move = current.position + moveDirection;

        //             PathfindingNode openMove = openNodes.Find(node => node.position == move);
        //             PathfindingNode closedMove = closedNodes.Find(node => node.position == move);

        //             // If node closed or obstructed, skip node
        //             if (closedMove != null || !obstacleRenderer.IsEmptyObstacles(move))
        //             {
        //                 continue;
        //             }
        //             // If move open but steps could be smaller, update steps
        //             else if (openMove != null && openMove.steps > lastSteps + 1)
        //             {
        //                 openMove.steps = lastSteps + 1;
        //             }
        //             // If valid area, create new node and increment steps by one
        //             else if (openMove == null && closedMove == null)
        //             {
        //                 openNodes.Add(new PathfindingNode(move, lastSteps + 1, Distance(move, end)));
        //             }
        //         }
        //     }

        //     // Start with end node and work towards start
        //     current = closedNodes[closedNodes.Count - 1];
        //     path.Add(current.position);

        //     // Create path back to start by choosing nodes with smallest distance to start
        //     while (current.position != start)
        //     {
        //         int minSteps = int.MaxValue;
        //         PathfindingNode bestMove = null;

        //         // Check each direction
        //         foreach (Vector2Int moveDirection in moveDirections)
        //         {
        //             Vector2Int move = current.position + moveDirection;

        //             PathfindingNode closedMove = closedNodes.Find(node => node.position == move);

        //             // If move contains closed node, distance to start less than min, and position not already in path
        //             if (closedMove != null && closedMove.steps < minSteps && !path.Contains(move))
        //             {
        //                 // Set new minimum distance and move index
        //                 minSteps = closedMove.steps;
        //                 bestMove = closedMove;
        //             }
        //         }

        //         // Set current node and add to path
        //         current = bestMove;
        //         path.Add(current.position);
        //     }

        //     path.Reverse();
        //     return path;
        // }

        private int Distance(Vector2Int current, Vector2Int target)
        {
            int xDistance = Mathf.Abs(current.x - target.x);
            int yDistance = Mathf.Abs(current.y - target.y);

            return xDistance + yDistance;
        }
    }
}
