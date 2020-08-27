using UnityEngine;
using UnityEngine.SceneManagement;

namespace AStarVisualization
{
    public class MenuButton : MonoBehaviour
    {
        public void QuitApplication()
        {
            Application.Quit();
        }

        public void SwitchScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
