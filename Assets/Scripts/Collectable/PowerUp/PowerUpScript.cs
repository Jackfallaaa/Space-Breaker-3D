using Audio;
using Player;
using TMPro;
using UnityEngine;

namespace Collectable.PowerUp {
    public class PowerUpScript : CollectableScript {
        private PowerUp _powerUp;

        public TextMeshPro powerUpText;

        protected override void Start() {
            base.Start();

            //Assign a random power-up type to the script
            _powerUp = PowerUp.GetRandomPowerUp();

            //Set the power-up text
            powerUpText.text = _powerUp.GetName();
        }

        public new void OnTriggerEnter(Collider other) {
            //If a player collides with a power-up, play the collect power-up sound and activate the power-up
            if (other.CompareTag("Player")) {
                AudioManager.Play("CollectPowerUp");
                FindObjectOfType<PowerUpManager>().Activate(_powerUp);
            }

            base.OnTriggerEnter(other);
        }
    }
}