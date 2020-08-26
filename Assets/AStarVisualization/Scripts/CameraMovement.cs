using UnityEngine;

namespace AStarVisualization
{
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private float moveSpeed = 0;
        [SerializeField] private float zoomSpeed = 0;
        [SerializeField] private float minZoom = 0;
        [SerializeField] private float maxZoom = 0;

        [Header("References")]
        [SerializeField] private GridRenderer gridRenderer = null;

        private Camera cam;

        private Vector3 moveDirection;

        private float zoom = 5;

        private void Start()
        {
            cam = GetComponent<Camera>();
        }

        private void Update()
        {
            Move();
            Zoom();
        }

        private void Move()
        {
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.y = Input.GetAxis("Vertical");

            transform.position = transform.position + (moveDirection * moveSpeed * Time.deltaTime);
        }

        private void Zoom()
        {
            // Zoom out
            if (Input.GetKey("z"))
            {
                zoom += zoomSpeed * Time.deltaTime;
            }
            // Zoom in
            else if (Input.GetKey("x"))
            {
                zoom -= zoomSpeed * Time.deltaTime;
            }
            else
            {
                return;
            }

            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = zoom;
            gridRenderer.UpdateGrid();
        }
    }
}
