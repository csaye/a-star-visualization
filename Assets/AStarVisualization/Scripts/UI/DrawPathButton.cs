using UnityEngine;
using UnityEngine.UI;

namespace AStarVisualization
{
    [RequireComponent(typeof(Image),typeof(Button))]
    public class DrawPathButton : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Pathfinding pathfinding = null;
        [SerializeField] private Transform startingNodeTransform = null;
        [SerializeField] private Transform endingNodeTransform = null;
        [SerializeField] private Sprite startButton = null;
        [SerializeField] private Sprite startButtonHighlight = null;
        [SerializeField] private Sprite pauseButton = null;
        [SerializeField] private Sprite pauseButtonHighlight = null;

        private Image image;
        private Button button;

        private bool lastDrawState;

        private void Start()
        {
            image = GetComponent<Image>();
            button = GetComponent<Button>();
        }

        private void Update()
        {
            if (lastDrawState != pathfinding.drawing && !pathfinding.drawing)
            {
                StartButtonSprite();
            }

            lastDrawState = pathfinding.drawing;
        }

        public void DrawPath()
        {
            if (pathfinding.drawing)
            {
                StartButtonSprite();
                pathfinding.CancelDrawPath();
            }
            else
            {
                PauseButtonSprite();

                Vector2Int startingPosition = ToVector2Int(startingNodeTransform.position);
                Vector2Int endingPosition = ToVector2Int(endingNodeTransform.position);
                
                pathfinding.DrawPath(startingPosition, endingPosition);
            }
        }

        private void StartButtonSprite()
        {
            image.sprite = startButton;

            SpriteState spriteState = new SpriteState();

            spriteState.highlightedSprite = startButtonHighlight;
            spriteState.pressedSprite = startButtonHighlight;

            button.spriteState = spriteState;
        }

        private void PauseButtonSprite()
        {
            image.sprite = pauseButton;

            SpriteState spriteState = new SpriteState();

            spriteState.highlightedSprite = pauseButtonHighlight;
            spriteState.pressedSprite = pauseButtonHighlight;

            button.spriteState = spriteState;
        }

        private Vector2Int ToVector2Int(Vector3 v3)
        {
            return new Vector2Int(Mathf.RoundToInt(v3.x), Mathf.RoundToInt(v3.y));
        }
    }
}
