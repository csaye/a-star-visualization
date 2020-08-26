using System.Collections;
using UnityEngine;

namespace AStarVisualization
{
    public class DraggableNode : MonoBehaviour
    {
        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Drag());
            }
        }

        private IEnumerator Drag()
        {
            // lastMousePosition = MousePosition();

            // While left click held down
            while (Input.GetMouseButton(0))
            {
                // mousePosition = MousePosition();

                // if ()
                yield return null;
            }
        }
    }
}
