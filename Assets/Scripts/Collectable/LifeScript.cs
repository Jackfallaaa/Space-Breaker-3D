using Audio;
using Player;
using UnityEngine;

namespace Collectable {
    public class LifeScript : CollectableScript {
        private new void OnTriggerEnter(Collider other) {
            //If the player collides with a life, play the collect life sound and increase the number of lives
            if (other.CompareTag("Player")) {
                AudioManager.Play("CollectLife");
                FindObjectOfType<ScoreManager>().AddLives(1);
            }

            base.OnTriggerEnter(other);
        }
    }
}