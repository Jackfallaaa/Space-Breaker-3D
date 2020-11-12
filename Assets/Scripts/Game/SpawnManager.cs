using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;

namespace Game {
    public class SpawnManager : MonoBehaviour {
        private float _previousZ;

        private ScoreManager _scoreManager;

        public GameObject alien;
        public GameObject asteroid;
        public GameObject gem;
        public GameObject life;
        public GameObject powerUp;
        public GameObject satellite;

        public List<GameObject> spawnedObstacles;

        private void Start() {
            _scoreManager = FindObjectOfType<ScoreManager>();

            spawnedObstacles = new List<GameObject>();

            //Sets the seed if the player selected "Play Seed" from the scoreboard
            if (GameManager.PlaySeed)
                Random.seed = GameManager.Seed;
            else
                GameManager.Seed = Random.seed;
        }

        public void Update() {
            //If the number of spawned obstacles is less than the minimum spawn count, spawn a new obstacle
            if (spawnedObstacles.Count < GameManager.MinimumSpawnCount)
                Spawn();
        }

        private void Spawn() {
            //Start spawning from the right by selecting a subsection between the right bound and the offset from the right bound
            var rightBound = GameManager.GameWidth;
            var leftBound = rightBound - GameManager.SpaceshipOffset;

            //Randomise an x position within the subsection
            var x = Random.Range(leftBound, rightBound);

            _previousZ = GetCurrentZ();

            //Randomise z position to decide horizontal position of the next spawns
            var z = Random.Range(_previousZ, _previousZ + GameManager.VerticalDistanceBetweenSpawns);

            //While the x position is within the left bound
            while (x > -GameManager.GameWidth) {
                var position = new Vector3(x, transform.position.y, z);

                //Depending on the player's lives/score, random chance to spawn collectables/obstacles
                if (_scoreManager.lives < GameManager.MaximumLives && Random.Range(0, GameManager.LifeChance) == 0)
                    SpawnLife(position);
                else if (_scoreManager.score >= GameManager.ScorePowerUpThreshold && Random.Range(0, GameManager.PowerUpChance) == 0)
                    SpawnPowerUp(position);
                else if (_scoreManager.score >= GameManager.ScoreAlienThreshold && Random.Range(0, GameManager.AlienChance) == 0)
                    SpawnAlien(position);
                else if (_scoreManager.score >= GameManager.ScoreSatelliteThreshold && Random.Range(0, GameManager.SatelliteChance) == 0)
                    SpawnSatellite(position);
                else
                    SpawnAsteroid(position);

                //Select next subsection bounds where the right bound starts from the previously randomised x position
                rightBound = x - GameManager.SpaceshipOffset;
                leftBound = rightBound - GameManager.SpaceshipOffset;

                //Generate x position for next spawns
                x = Random.Range(leftBound, rightBound);
            }
        }

        private void SpawnAsteroid(Vector3 position) {
            Instantiate(asteroid, position, Quaternion.identity);
        }

        private void SpawnAlien(Vector3 position) {
            Instantiate(alien, position, Quaternion.identity);
        }

        public void SpawnPowerUp(Vector3 position) {
            Instantiate(powerUp, position, Quaternion.identity);
        }

        private void SpawnLife(Vector3 position) {
            Instantiate(life, position, Quaternion.identity);
        }

        public void SpawnGem(Vector3 position) {
            Instantiate(gem, position, Quaternion.identity);
        }

        private void SpawnSatellite(Vector3 position) {
            Instantiate(satellite, position, Quaternion.identity);
        }

        private float GetCurrentZ() {
            //If there are zero spawned obstacles, the current z position is the starting spawn offset + the vertical spaceship offset
            if (spawnedObstacles.Count == 0)
                return GameManager.SpawnOffset + GameManager.SpaceshipOffset;

            //Otherwise, return the z position of last spawned object + the vertical spaceship offset
            return spawnedObstacles.ElementAt(spawnedObstacles.Count - 1).transform.position.z + GameManager.SpaceshipOffset;
        }
    }
}