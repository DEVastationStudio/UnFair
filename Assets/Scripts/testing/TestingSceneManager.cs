using UnityEngine;

namespace testing
{
    public class TestingSceneManager : MonoBehaviour
    {
        private static TestingSceneManager instance;
    
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void ChangeScene(string scene)
        {
            FadeController.Fade(scene);
        }
    }
}
