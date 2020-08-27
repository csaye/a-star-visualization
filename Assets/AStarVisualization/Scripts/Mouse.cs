using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AStarVisualization
{
    public class Mouse : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera _mainCamera = null;

        private static Camera mainCamera;

        private void Start()
        {
            mainCamera = _mainCamera;
        }

        // Returns the bottom left corner of the tile currently at the mouse position
        public static Vector2Int Position()
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            
            int x = Mathf.FloorToInt(mousePos.x);
            int y = Mathf.FloorToInt(mousePos.y);
            
            return new Vector2Int(x, y);
        }

        // Returns true if the mouse is over any ui elements, excluding the background
        public static bool IsOverUI()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            return results.Count > 1;
        }
    }
}
