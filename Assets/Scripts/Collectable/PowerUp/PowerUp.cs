using System;
using Game;

namespace Collectable.PowerUp {
    public class PowerUp {
        private static readonly Random Random = new Random();

        public static readonly PowerUp FastLaser = new PowerUp("Fast Laser", GameManager.DefaultPowerUpDuration);
        public static readonly PowerUp Invincibility = new PowerUp("Invincibility", GameManager.DefaultPowerUpDuration);
        public static readonly PowerUp DoubleDamage = new PowerUp("Double Damage", GameManager.DefaultPowerUpDuration);
        public static readonly PowerUp TripleDamage = new PowerUp("Triple Damage", GameManager.DefaultPowerUpDuration);
        public static readonly PowerUp DoubleScore = new PowerUp("Double Score", GameManager.DefaultPowerUpDuration);
        public static readonly PowerUp TripleScore = new PowerUp("Triple Score", GameManager.DefaultPowerUpDuration);

        private static readonly PowerUp[] PowerUps = {FastLaser, Invincibility, DoubleDamage, TripleDamage, DoubleScore, TripleScore};

        public static PowerUp GetRandomPowerUp() {
            return PowerUps[Random.Next(0, PowerUps.Length)];
        }

        private readonly string _name;
        private readonly float _duration;

        private PowerUp(string name, float duration) {
            _name = name;
            _duration = duration;
        }

        public string GetName() {
            return _name;
        }

        public float GetDuration() {
            return _duration;
        }
    }
}