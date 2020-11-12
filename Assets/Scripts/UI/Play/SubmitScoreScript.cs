using Game;
using Player;
using Scoreboard;
using TMPro;
using UnityEngine;

namespace UI.Play {
    public class SubmitScoreScript : MonoBehaviour {
        private int _score;

        private GameOverScript _gameOverScript;
        private ScoreboardManager _scoreboardManager;
        private ScoreManager _scoreManager;

        public GameObject canvas;

        public TMP_InputField nameInput;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI warningField;

        private void Start() {
            _scoreManager = FindObjectOfType<ScoreManager>();
            _scoreboardManager = FindObjectOfType<ScoreboardManager>();
            _gameOverScript = FindObjectOfType<GameOverScript>();
        }

        public void Submit() {
            var input = nameInput.text;
            if (string.IsNullOrEmpty(input)) {
                warningField.SetText("Please enter your name.");
            }
            else if (input.Contains(" ")) {
                warningField.SetText("Your name cannot contain spaces.");
            }
            else if (input.Contains("*")) {
                warningField.SetText("Your name cannot contain asterisks.");
            }
            else {
                //If the player input meets all requirements, submit the name, score, and seed to the ScoreboardManager
                _scoreboardManager.AddScore(input.ToUpper(), _score, GameManager.Seed);

                FindObjectOfType<SceneSwitcherScript>().LoadScene("Scoreboard");
            }
        }

        public void Back() {
            Hide();
        }

        private void Refresh() {
            _score = _scoreManager.score;

            //Refresh the UI components to show accurate player information
            scoreText.text = _score.ToString();
            nameInput.Select();
        }

        public void Show() {
            _gameOverScript.Hide();
            canvas.SetActive(true);
            Refresh();
        }

        private void Hide() {
            _gameOverScript.Show();
            canvas.SetActive(false);
        }
    }
}