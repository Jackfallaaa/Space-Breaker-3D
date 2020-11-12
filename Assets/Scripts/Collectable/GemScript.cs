using Audio;
using Player;
using UnityEngine;

namespace Collectable {
    public class GemScript : CollectableScript {
        private new void OnTriggerEnter(Collider other) {
            //If a player collides with a gem, play the collect gem sound and increase the number of gems
            if (other.CompareTag("Player")) {
                AudioManager.Play("CollectGem");
                FindObjectOfType<ScoreManager>().AddGems(1);
            }

            base.OnTriggerEnter(other);
        }
    }
}