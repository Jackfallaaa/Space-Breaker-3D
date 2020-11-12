using UnityEngine;

namespace UI.Play {
    public class PauseScript : MonoBehaviour {
        private OverlayScript _overlayScript;

        private bool _paused;

        public GameObject canvas;

        private void Start() {
            _overlayScript = FindObjectOfType<OverlayScript>();
        }

        public void Update() {
            //If the player presses the ESC key, pause/resume the game
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (_paused)
                    Resume();
                else
                    Pause();
            }
        }

        public void Pause() {
            //If the player is currently playing, pause the game
            if (_overlayScript.canvas.active) {
                Show();
                Time.timeScale = 0f;
            }
        }

        public void Resume() {
            Hide();
            Time.timeScale = 1f;
        }

        public void Quit() {
            Resume();
            FindObjectOfType<SceneSwitcherScript>().LoadScene("MainMenu");
        }

        public void Show() {
            _paused = true;
            canvas.SetActive(true);
            _overlayScript.Hide();
        }

        public void Hide() {
            _paused = false;
            canvas.SetActive(false);
            _overlayScript.Show();
        }
    }
}