using Game;
using UI.Play;
using UnityEngine;

namespace Player {
    public class ScoreManager : MonoBehaviour {
        private OverlayScript _overlayScript;

        public int gems;
        public int lives;
        public int score;

        private void Start() {
            _overlayScript = FindObjectOfType<OverlayScript>();

            score = 0;
            _overlayScript.SetScoreText(score.ToString());

            gems = PlayerPrefs.GetInt("gems", 0);
            _overlayScript.SetGemText(gems.ToString());

            lives = 0;
            _overlayScript.SetLivesText(lives.ToString());
        }

        public void AddScore(int i) {
            score += i;
            PlayerPrefs.SetInt("score", score);
            _overlayScript.SetScoreText(score.ToString());

            //Update the game speed, which scales with the player's score
            GameManager.UpdateSpeed(score);
        }

        public void AddGems(int i) {
            gems += i;
            PlayerPrefs.SetInt("gems", gems);
            _overlayScript.SetGemText(gems.ToString());
        }

        public void RemoveGems(int i) {
            gems -= i;
            PlayerPrefs.SetInt("gems", gems);
            _overlayScript.SetGemText(gems.ToString());
        }

        public void AddLives(int i) {
            lives += i;
            _overlayScript.SetLivesText(lives.ToString());
        }

        public void RemoveLives(int i) {
            lives -= i;
            _overlayScript.SetLivesText(lives.ToString());
        }
    }
}