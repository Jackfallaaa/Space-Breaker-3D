using Collectable.PowerUp;
using UI.Play;
using UnityEngine;

namespace Player {
    public class PowerUpManager : MonoBehaviour {
        public static PowerUp currentPowerUp;

        private PlayerScript _playerScript;
        private OverlayScript _overlayScript;

        private void Start() {
            _playerScript = FindObjectOfType<PlayerScript>();
            _overlayScript = FindObjectOfType<OverlayScript>();
        }

        public void Activate(PowerUp powerUp) {
            Deactivate();

            currentPowerUp = powerUp;

            _playerScript.ActivatePowerUp(powerUp);
            _overlayScript.ActivatePowerUp(powerUp);

            //Deactivate after the power-up duration is complete
            Invoke(nameof(Deactivate), powerUp.GetDuration());
        }

        public void Deactivate() {
            CancelInvoke(nameof(Deactivate));

            currentPowerUp = null;
            _overlayScript.ResetPowerUp();
        }
    }
}