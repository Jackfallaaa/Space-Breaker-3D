using Audio;
using Game;
using UnityEngine;

namespace Obstacle.Destructible {
    public class AsteroidScript : DestructibleObstacleScript {
        private float _randomX, _randomY, _randomZ;
        public Transform mesh;

        protected override void Start() {
            base.Start();

            //Assign a random x, y, z target rotation for the asteroid
            _randomX = Random.Range(0, 60);
            _randomY = Random.Range(0, 60);
            _randomZ = Random.Range(0, 60);

            //Set the asteroid health, which is scaled to the player's score
            obstacleHealth = Mathf.RoundToInt(hpCurve.Evaluate((float) scoreManager.score / 10000));
            obstacleHealth += Mathf.RoundToInt(Random.Range(-obstacleHealth / 1.5f, obstacleHealth / 1.5f));

            //Set the asteroid health text
            obstacleHealthText.text = obstacleHealth.ToString();
        }

        private void Update() {
            //Randomly rotate the asteroid toward the random target rotation
            mesh.Rotate(new Vector3(_randomX, _randomY, _randomZ) * Time.deltaTime);
        }

        protected override void DestroyObstacle(bool withParticle, bool replace, bool withSound) {
            //If true, random chance to replace the asteroid with a gem
            if (replace && Random.Range(0, GameManager.GemChance) == 0)
                spawnManager.SpawnGem(transform.position);

            //If true, play the asteroid destroy sound effect
            if (withSound)
                AudioManager.Play("AsteroidDestroy");

            base.DestroyObstacle(withParticle, replace, withSound);
        }
    }
}