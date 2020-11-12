using Collectable.PowerUp;
using Game;
using Player;
using TMPro;
using UnityEngine;

namespace Obstacle.Destructible {
    public class DestructibleObstacleScript : MonoBehaviour {
        private float _speed;

        public TextMeshPro obstacleHealthText;
        public GameObject destroyParticle;
        public AnimationCurve hpCurve;

        protected int obstacleHealth;
        protected PlayerScript playerScript;
        protected ScoreManager scoreManager;
        protected SpawnManager spawnManager;

        protected virtual void Start() {
            _speed = GameManager.GameSpeed;

            scoreManager = FindObjectOfType<ScoreManager>();
            spawnManager = FindObjectOfType<SpawnManager>();
            playerScript = FindObjectOfType<PlayerScript>();

            //Add obstacle to the list of spawned obstacles
            spawnManager.spawnedObstacles.Add(gameObject);

            //Move the obstacle towards the player at a constant speed
            GetComponent<Rigidbody>().AddForce(Vector3.back * _speed);
        }

        protected virtual void OnTriggerEnter(Collider other) {
            //If the player collides with an obstacle and has invincibility or an extra life, destroy the obstacle
            if (other.CompareTag("Player")) {
                if (PowerUpManager.currentPowerUp == PowerUp.Invincibility)
                    DestroyObstacle(true, true, true);
                else if (!playerScript.Destroy())
                    DestroyObstacle(false, true, false);
            }

            //If the obstacle is not an alien and collides with an alien laser, destroy the obstacle
            else if (!CompareTag("Alien") && other.CompareTag("AlienLaser")) {
                DestroyObstacle(true, true, true);
            }

            //If the destructible obstacle collides with an indestructible obstacle, destroy the destructible obstacle
            else if (other.CompareTag("IndestructibleObstacle")) {
                DestroyObstacle(true, true, true);
            }

            //If the obstacle collides with a player laser of any type, destroy the laser
            else if (other.CompareTag("PlayerLaser") || other.CompareTag("PlayerDoubleLaser") || other.CompareTag("PlayerTripleLaser")) {
                Destroy(other.gameObject);

                //Apply the damage corresponding to the laser type
                var damage = GameManager.PlayerLaserPower;
                if (other.CompareTag("PlayerDoubleLaser"))
                    damage *= 2;
                else if (other.CompareTag("PlayerTripleLaser"))
                    damage *= 3;
                obstacleHealth -= damage;

                //Increment the player's score depending on the active power-ups
                var score = 1;
                if (PowerUpManager.currentPowerUp == PowerUp.DoubleScore)
                    score = 2;
                else if (PowerUpManager.currentPowerUp == PowerUp.TripleScore)
                    score = 3;
                scoreManager.AddScore(score);

                //Set the remaining obstacle health, or destroy if it has no remaining health
                if (obstacleHealth > 0)
                    obstacleHealthText.text = obstacleHealth.ToString();
                else
                    DestroyObstacle(true, true, true);
            }

            //If the obstacle collides with the destroyer, destroy the obstacle
            else if (other.CompareTag("Destroyer")) {
                DestroyObstacle(false, false, false);
            }
        }

        protected virtual void DestroyObstacle(bool withParticle, bool replace, bool withSound) {
            //If true, play the destroy particle
            if (withParticle) {
                var particle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
                Destroy(particle, GameManager.ParticleLifespan);
            }

            //Remove the obstacle from the list of spawned obstacle
            spawnManager.spawnedObstacles.Remove(gameObject);

            Destroy(gameObject);
        }
    }
}