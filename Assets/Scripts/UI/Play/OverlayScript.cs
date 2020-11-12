using Audio;
using Collectable.PowerUp;
using TMPro;
using UnityEngine;

namespace UI.Play {
    public class OverlayScript : MonoBehaviour {
        private float _currentPowerUpDuration;

        public GameObject canvas;

        public TextMeshProUGUI gemText;
        public TextMeshProUGUI livesText;
        public TextMeshProUGUI scoreText;

        public GameObject powerUpContainer;
        public GameObject powerUpNameText;
        public GameObject powerUpDurationText;

        public void SetLivesText(string text) {
            livesText.GetComponent<Animation>().Play();
            livesText.text = text;
        }

        public void SetScoreText(string text) {
            scoreText.GetComponent<Animation>().Play();
            scoreText.text = text;
        }

        public void SetGemText(string text) {
            gemText.GetComponent<Animation>().Play();
            gemText.text = text;
        }

        public void ActivatePowerUp(PowerUp powerUp) {
            ResetPowerUp();

            powerUpContainer.SetActive(true);
            powerUpNameText.GetComponent<Animation>().Play();
            powerUpNameText.GetComponent<TextMeshProUGUI>().text = powerUp.GetName();
            powerUpDurationText.GetComponent<Animation>().Play();
            powerUpDurationText.GetComponent<TextMeshProUGUI>().text = powerUp.GetDuration().ToString();

            _currentPowerUpDuration = powerUp.GetDuration();

            //Play the Power-Up Tick sound effect
            AudioManager.Play("PowerUpTick");

            //Update the power-up state each second
            Invoke(nameof(UpdatePowerUp), 1);
        }

        private void UpdatePowerUp() {
            //If the power-up is still active, continue to update the power-up state
            if (_currentPowerUpDuration >= 0) {
                AudioManager.Play("PowerUpTick");

                _currentPowerUpDuration -= 1;

                powerUpDurationText.GetComponent<Animation>().Play();
                powerUpDurationText.GetComponent<TextMeshProUGUI>().text = _currentPowerUpDuration.ToString();

                Invoke(nameof(UpdatePowerUp), 1);
            }
        }

        public void ResetPowerUp() {
            CancelInvoke(nameof(UpdatePowerUp));
            powerUpContainer.SetActive(false);
        }

        public void Show() {
            canvas.SetActive(true);
        }

        public void Hide() {
            canvas.SetActive(false);
        }
    }
}