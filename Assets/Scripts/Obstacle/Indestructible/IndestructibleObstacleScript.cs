using Collectable.PowerUp;
using Game;
using Player;
using UnityEngine;

namespace Obstacle.Indestructible {
    public class IndestructibleObstacleScript : MonoBehaviour {
        private float _speed;

        protected PlayerScript playerScript;

        public GameObject destroyParticle;

        protected virtual void Start() {
            _speed = GameManager.GameSpeed * 2;

            playerScript = FindObjectOfType<PlayerScript>();

            //Move the obstacle towards the player at a constant speed
            GetComponent<Rigidbody>().AddForce(Vector3.back * _speed);
        }

        private void OnTriggerEnter(Collider other) {
            //If the player collides with an obstacle and has invincibility or an extra life, destroy the obstacle
            if (other.CompareTag("Player")) {
                if (PowerUpManager.currentPowerUp == PowerUp.Invincibility)
                    DestroyObstacle(true, true);
                else if (!playerScript.Destroy())
                    DestroyObstacle(false, true);
            }

            //If the obstacle collides with a player laser of any type, destroy the laser
            else if (other.CompareTag("PlayerLaser") || other.CompareTag("PlayerDoubleLaser") || other.CompareTag("PlayerTripleLaser")) {
                Destroy(other.gameObject);
            }

            //If the obstacle collides with the destroyer, destroy the obstacle
            else if (other.CompareTag("Destroyer")) {
                DestroyObstacle(false, false);
            }
        }

        protected virtual void DestroyObstacle(bool withParticle, bool withSound) {
            //If true, play the assigned destroy particle
            if (withParticle) {
                var particle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
                Destroy(particle, GameManager.ParticleLifespan);
            }

            Destroy(gameObject);
        }
    }
}