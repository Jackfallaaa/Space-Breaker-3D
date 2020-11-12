using UnityEngine;

namespace Game {
    public class GameManager : MonoBehaviour {
        public static float GameWidth = 7.5f;

        //Variables for game spawning
        public static int MinimumSpawnCount = 60;
        public static float SpawnOffset = 10;
        public static float VerticalDistanceBetweenSpawns = 3.5f;
        public static float SpaceshipOffset = 2.5f;

        //Variables for game speed
        public static float DefaultGameSpeed = 500;
        public static float GameSpeed = DefaultGameSpeed;

        //Variables for player movement controls
        public static float PlayerSensitivity = 10;
        public static float PlayerSensitivityMultiplier = 0.75f;
        public static float PlayerTiltSpeed = 1;
        public static float PlayerMaxTilt = 45;

        //Variables for player shoot controls
        public static int PlayerLaserPower = 1;
        public static float PlayerLaserSpeed = 3000;
        public static float PlayerShootDelay = 0.35f;
        public static float PlayerLaserDestroyDelay = 2;

        //Variables for alien obstacle characteristics
        public static int AlienChance = 100;
        public static float AlienLaserSpeed = 3000;
        public static float AlienShootDelay = 3;

        //Variables for satellite obstacle characteristics
        public static int SatelliteChance = 150;

        //Variables for for gem collectable characteristics
        public static int GemChance = 5;

        //Variables for life collectable characteristics
        public static int LifeChance = 250;
        public static int MaximumLives = 3;

        //Variables for power-up collectable characteristics
        public static int PowerUpChance = 75;
        public static float DefaultPowerUpDuration = 10;

        //Variables for score progression
        public static int ScorePowerUpThreshold = 10;
        public static int ScoreAlienThreshold = 20;
        public static int ScoreSatelliteThreshold = 30;

        //Variables for revive characteristics
        public static int DefaultReviveGemCost = 10;
        public static int ReviveGemCost = DefaultReviveGemCost;
        public static int ReviveGemCostMultiplier = 2;

        //Delays for destroying redundant particles
        public static float ParticleLifespan = 3;

        //Variables for "Play Seed" scoreboard functionality
        public static bool PlaySeed = false;
        public static int Seed;

        private void Start() {
            GameSpeed = DefaultGameSpeed;
            ReviveGemCost = DefaultReviveGemCost;

            PlayerSensitivityMultiplier = PlayerPrefs.GetFloat("sensitivityMultiplier", PlayerSensitivityMultiplier);
        }

        public static void UpdateSpeed(int score) {
            //Allows the game speed to increase rapidly at the start, then slowly as the game progresses
            if (score <= 100 && score % 20 == 0 || score <= 1000 && score % 100 == 0) {
                GameSpeed *= 1.1f;

                //Increment the minimum spawn count so the SpawnManager can keep up with the new speed
                MinimumSpawnCount += 1;

                //Increase the speed of the game background to match the new speed
                FindObjectOfType<BackgroundScript>().IncreaseSpeed(1.1f);
            }
        }
    }
}