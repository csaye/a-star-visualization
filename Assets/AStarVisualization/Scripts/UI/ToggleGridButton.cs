using UnityEngine;

namespace AStarVisualization
{
    public class ToggleGridButton : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GridRenderer gridRenderer = null;

        public void ToggleGrid()
        {
            gridRenderer.ToggleGridActive();
        }
    }
}
