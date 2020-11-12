using Audio;
using Game;
using UnityEngine;

namespace Obstacle.Destructible {
    public class AlienScript : DestructibleObstacleScript {
        public GameObject alienLaser;

        protected override void Start() {
            base.Start();

            //Set the obstacle health, which is scaled to the player's score
            obstacleHealth = Mathf.RoundToInt(hpCurve.Evaluate((float) scoreManager.score / 10000));
            obstacleHealth += Mathf.RoundToInt(Random.Range(0, obstacleHealth / 1.5f));

            //Set the alien health text
            obstacleHealthText.text = obstacleHealth.ToString();

            //Call the shoot method after a constant shoot delay
            Invoke(nameof(Shoot), GameManager.AlienShootDelay);
        }

        private void Shoot() {
            //Play the alien shoot sound effect and create an alien laser
            AudioManager.Play("AlienShoot");
            Instantiate(alienLaser, transform.position, Quaternion.identity);

            //Invoke the shoot method again after the same delay
            Invoke(nameof(Shoot), GameManager.AlienShootDelay);
        }

        protected override void DestroyObstacle(bool withParticle, bool replace, bool withSound) {
            //If true, replace the alien with a power-up
            if (replace)
                spawnManager.SpawnPowerUp(transform.position);

            //If true, play the alien destroy sound effect
            if (withSound)
                AudioManager.Play("AlienDestroy");

            base.DestroyObstacle(withParticle, replace, withSound);
        }
    }
}