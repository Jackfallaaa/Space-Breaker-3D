using UnityEngine;

namespace UI.MainMenu {
    public class InstructionsScript : MonoBehaviour {
        private MainMenuScript _mainMenuScript;

        private void Start() {
            _mainMenuScript = FindObjectOfType<MainMenuScript>();
        }

        public void Back() {
            _mainMenuScript.ShowMainMenu();
        }
    }
}