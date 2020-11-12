using Collectable.PowerUp;
using Game;
using Player;
using UnityEngine;

namespace Projectile {
    public class AlienLaserScript : MonoBehaviour {
        private void Start() {
            //Set the laser rotation to target the player
            var rotation = transform.rotation;
            transform.Rotate(270, rotation.y, rotation.z);

            //Move the alien laser towards the player at a constant speed
            GetComponent<Rigidbody>().AddForce(Vector3.back * GameManager.AlienLaserSpeed);
        }

        private void OnTriggerEnter(Collider other) {
            //If the player collides with an alien laser and has invincibility or an extra life, destroy the alien laser
            if (other.CompareTag("Player")) {
                if (PowerUpManager.currentPowerUp == PowerUp.Invincibility)
                    Destroy(gameObject);
                else if (!FindObjectOfType<PlayerScript>().Destroy())
                    Destroy(gameObject);
            }
        }
    }
}