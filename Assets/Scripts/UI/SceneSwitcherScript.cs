using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class SceneSwitcherScript : MonoBehaviour {
        private string _targetScene;

        public Animator animator;

        public void LoadScene(string sceneName) {
            _targetScene = sceneName;

            //Start the fade out animation upon starting
            animator.SetTrigger("FadeOut");
        }

        private void OnFadeComplete() {
            //Once the fade out is complete, load the target scene
            SceneManager.LoadScene(_targetScene);
        }
    }
}