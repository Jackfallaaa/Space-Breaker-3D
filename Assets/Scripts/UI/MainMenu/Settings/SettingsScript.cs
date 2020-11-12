using UnityEngine;

namespace UI.MainMenu.Settings {
    public class SettingsScript : MonoBehaviour {
        private MainMenuScript _mainMenuScript;

        private void Start() {
            _mainMenuScript = FindObjectOfType<MainMenuScript>();
        }

        public void Back() {
            _mainMenuScript.ShowMainMenu();
        }
    }
}