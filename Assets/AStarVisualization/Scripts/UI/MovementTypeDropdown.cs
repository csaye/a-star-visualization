using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AStarVisualization
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class MovementTypeDropdown : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Pathfinding pathfinding = null;

        private List<Vector2Int> normal = new List<Vector2Int>()
        {
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.right,
            Vector2Int.down
        };

        private List<Vector2Int> diagonals = new List<Vector2Int>()
        {
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.right,
            Vector2Int.down,
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 1),
            new Vector2Int(1, 1),
            new Vector2Int(1, -1)
        };

        private List<Vector2Int> knight = new List<Vector2Int>()
        {
            new Vector2Int(-1, -2),
            new Vector2Int(-2, -1),
            new Vector2Int(-1, 2),
            new Vector2Int(-2, 1),
            new Vector2Int(1, 2),
            new Vector2Int(2, 1),
            new Vector2Int(1, -2),
            new Vector2Int(2, -1)
        };

        private List<Vector2Int> leapfrog = new List<Vector2Int>();

        private const int maxLeapfrogLeap = 8;

        private TMP_Dropdown dropdown;

        private void Start()
        {
            dropdown = GetComponent<TMP_Dropdown>();

            InitializeLeapfrog();
        }

        private void InitializeLeapfrog()
        {
            for (int leap = 1; leap <= maxLeapfrogLeap; leap++)
            {
                leapfrog.Add(new Vector2Int(0, leap));
                leapfrog.Add(new Vector2Int(0, -leap));
                leapfrog.Add(new Vector2Int(leap, 0));
                leapfrog.Add(new Vector2Int(-leap, 0));
            }
        }

        private void Update()
        {
            dropdown.interactable = !pathfinding.drawing;
        }

        public void UpdateMovementType()
        {
            switch (dropdown.value)
            {
                case 0:
                    pathfinding.moveDirections = normal;
                    break;
                case 1:
                    pathfinding.moveDirections = diagonals;
                    break;
                case 2:
                    pathfinding.moveDirections = knight;
                    break;
                case 3:
                    pathfinding.moveDirections = leapfrog;
                    break;
            }
        }
    }
}
