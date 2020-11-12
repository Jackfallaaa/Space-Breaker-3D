using Game;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Play {
    public class GameOverScript : MonoBehaviour {
        private OverlayScript _overlayScript;
        private PlayerScript _playerScript;
        private ScoreManager _scoreManager;
        private SubmitScoreScript _submitScoreScript;

        public GameObject canvas;

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI gemText;
        public TextMeshProUGUI gemCostText;
        public Button reviveButton;
        public Button submitScoreButton;

        private void Start() {
            _scoreManager = FindObjectOfType<ScoreManager>();
            _overlayScript = FindObjectOfType<OverlayScript>();
            _playerScript = FindObjectOfType<PlayerScript>();
            _submitScoreScript = FindObjectOfType<SubmitScoreScript>();

            //If the player is playing a scoreboard seed, disable the submit score button
            if (GameManager.PlaySeed)
                submitScoreButton.interactable = false;
        }

        public void Restart() {
            FindObjectOfType<SceneSwitcherScript>().LoadScene("Play");
        }

        public void Revive() {
            //If the player has enough gems, revive the player and show the game overlay
            if (_scoreManager.gems >= GameManager.ReviveGemCost) {
                _playerScript.Revive();
                _overlayScript.Show();
                Hide();
            }
        }

        public void SubmitScore() {
            _submitScoreScript.Show();
        }

        public void Quit() {
            FindObjectOfType<SceneSwitcherScript>().LoadScene("MainMenu");
        }

        private void Refresh() {
            //Refresh the UI components to show accurate player information
            gemText.text = _scoreManager.gems.ToString();
            scoreText.text = _scoreManager.score.ToString();
            gemCostText.text = GameManager.ReviveGemCost.ToString();

            //If the player does not have enough gems to revive, disable the revive button
            if (_scoreManager.gems < GameManager.ReviveGemCost)
                reviveButton.interactable = false;
        }

        public void Show() {
            _overlayScript.Hide();
            Refresh();
            canvas.SetActive(true);
        }

        public void Hide() {
            canvas.SetActive(false);
        }
    }
}