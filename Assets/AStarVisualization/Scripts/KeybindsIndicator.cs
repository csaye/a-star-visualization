using UnityEngine;

namespace AStarVisualization
{
    [RequireComponent(typeof(Animator))]
    public class KeybindsIndicator : MonoBehaviour
    {
        private static bool triggered = false;

        private Animator animator;

        private void Start()
        {
            if (triggered)
            {
                Destroy(gameObject);
            }
            else
            {
                triggered = true;
                
                animator = GetComponent<Animator>();
                animator.SetTrigger("Show");
            }
        }
    }
}
