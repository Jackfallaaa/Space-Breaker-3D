using Game;
using UnityEngine;

namespace UI.MainMenu {
    public class MainMenuScript : MonoBehaviour {
        public GameObject instructionsCanvas;
        public GameObject mainMenuCanvas;
        public GameObject settingsCanvas;

        private void HideAll() {
            mainMenuCanvas.SetActive(false);
            instructionsCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
        }

        public void ShowMainMenu() {
            HideAll();
            mainMenuCanvas.SetActive(true);
        }

        private void ShowInstructions() {
            HideAll();
            instructionsCanvas.SetActive(true);
        }

        private void ShowSettings() {
            HideAll();
            settingsCanvas.SetActive(true);
        }

        public void Play() {
            //Resets the "Play Seed" scoreboard button flag
            GameManager.PlaySeed = false;

            FindObjectOfType<SceneSwitcherScript>().LoadScene("Play");
        }

        public void Instructions() {
            ShowInstructions();
        }

        public void Scoreboard() {
            FindObjectOfType<SceneSwitcherScript>().LoadScene("Scoreboard");
        }

        public void Settings() {
            ShowSettings();
        }

        public void Exit() {
            Application.Quit();
        }
    }
}