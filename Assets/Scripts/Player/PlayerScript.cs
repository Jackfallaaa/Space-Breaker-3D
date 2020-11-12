using Audio;
using Collectable.PowerUp;
using Game;
using UI.Play;
using UnityEngine;

namespace Player {
    public class PlayerScript : MonoBehaviour {
        private bool _alive;
        private bool _shooting;

        private Rigidbody _playerRigidBody;

        private OverlayScript _overlayScript;
        private GameOverScript _gameOverScript;
        private PowerUpManager _powerUpManager;
        private ScoreManager _scoreManager;

        public GameObject damageParticle;
        public GameObject destroyParticle;
        public GameObject doubleDamageLaser;

        public GameObject normalLaser;
        public GameObject powerUpParticle;
        public GameObject tripleDamageLaser;

        private void Start() {
            _alive = true;
            _shooting = false;

            _playerRigidBody = GetComponent<Rigidbody>();
            _scoreManager = FindObjectOfType<ScoreManager>();
            _powerUpManager = FindObjectOfType<PowerUpManager>();
            _gameOverScript = FindObjectOfType<GameOverScript>();
            _overlayScript = FindObjectOfType<OverlayScript>();
        }

        private void Update() {
            //As long as the player is alive, allow the spaceship to move and shoot
            if (_alive) {
                Move();
                Shoot();
            }
        }

        private void Move() {
            //Depending on horizontal player movement, calculate the left and right spaceship tilt, clamped at the maximum tilt
            var leftTilt = Quaternion.Euler(0, 0, Input.GetAxis("Horizontal") * -GameManager.PlayerMaxTilt);
            var rightTilt = Quaternion.Euler(0, 0, -Input.GetAxis("Horizontal") * GameManager.PlayerMaxTilt);

            //Apply the left and right tilt to the current transform rotation
            var rotation = transform.rotation;
            rotation = Quaternion.Slerp(rotation, leftTilt, GameManager.PlayerTiltSpeed * GameManager.PlayerSensitivityMultiplier * Time.deltaTime);
            rotation = Quaternion.Slerp(rotation, rightTilt, GameManager.PlayerTiltSpeed * GameManager.PlayerSensitivityMultiplier * Time.deltaTime);
            transform.rotation = rotation;

            //Allow horizontal spaceship movement, clamped at the maximum/minimum game width
            var horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * (GameManager.PlayerSensitivity * GameManager.PlayerSensitivityMultiplier);
            var horizontalMovement = _playerRigidBody.transform.localPosition + Vector3.right * horizontalInput;
            horizontalMovement.x = Mathf.Clamp(horizontalMovement.x, -GameManager.GameWidth, GameManager.GameWidth);
            transform.localPosition = horizontalMovement;
        }

        private void Shoot() {
            //If the player is not playing, ignore
            if (!_overlayScript.canvas.active)
                return;

            //If the player is holding down the SPACE key or LEFT MOUSE button, and is not already shooting, shoot a laser
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
                if (!_shooting) {
                    UpdateShootingState();

                    //Initialise the laser depending on the current power-up 
                    var laser = normalLaser;
                    if (PowerUpManager.currentPowerUp == PowerUp.TripleDamage)
                        laser = tripleDamageLaser;
                    else if (PowerUpManager.currentPowerUp == PowerUp.DoubleDamage)
                        laser = doubleDamageLaser;
                    Instantiate(laser, transform.position, Quaternion.identity);

                    //Play the Player Shoot sound effect
                    AudioManager.Play("PlayerShoot");

                    //Set the bullet delay depending on the current power-up 
                    var delay = GameManager.PlayerShootDelay;
                    if (PowerUpManager.currentPowerUp == PowerUp.FastLaser)
                        delay *= 0.75f;
                    Invoke(nameof(UpdateShootingState), delay);
                }
        }

        private void UpdateShootingState() {
            _shooting = !_shooting;
        }

        public bool Destroy() {
            //Play the player destroy sound effect
            AudioManager.Play("PlayerDestroy");

            //If the player has lives remaining, play the damage particle and decrease the number of remaining lives
            if (_scoreManager.lives > 0) {
                var tempDamageParticle = Instantiate(damageParticle, transform.position, Quaternion.identity);
                Destroy(tempDamageParticle, GameManager.ParticleLifespan);

                _scoreManager.RemoveLives(1);

                //Return false since the player has not been destroyed
                return false;
            }

            _alive = false;

            //Deactivate any active power-ups
            _powerUpManager.Deactivate();

            //Deactivate child components, including the spaceship mesh and engine
            for (var i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(false);

            //Play the destroy particle
            var tempDestroyParticle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(tempDestroyParticle, GameManager.ParticleLifespan);

            //Show the game over screen after a delay 
            Invoke(nameof(GameOver), 1);

            //Return true since the player has been destroyed
            return true;
        }

        private void GameOver() {
            _gameOverScript.Show();
        }

        public void Revive() {
            //Decrease the player's gem count by the revive cost
            _scoreManager.RemoveGems(GameManager.ReviveGemCost);

            //Apply the cost multiplier to the player's next revive
            GameManager.ReviveGemCost *= GameManager.ReviveGemCostMultiplier;

            _alive = true;

            //Activate child components, including the spaceship mesh and engine
            for (var i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(true);

            //Activate the invincibility power-up
            _powerUpManager.Activate(PowerUp.Invincibility);
        }

        public void ActivatePowerUp(PowerUp powerUp) {
            //Play the power-up particle
            var tempPowerUpParticle = Instantiate(powerUpParticle, transform.position, Quaternion.identity);
            Destroy(tempPowerUpParticle, GameManager.ParticleLifespan);
        }
    }
}