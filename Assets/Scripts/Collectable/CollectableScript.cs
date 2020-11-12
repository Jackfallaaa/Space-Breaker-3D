using Game;
using UnityEngine;

namespace Collectable {
    public class CollectableScript : MonoBehaviour {
        protected virtual void Start() {
            //Move the collectable towards the player at a constant speed
            GetComponent<Rigidbody>().AddForce(Vector3.back * GameManager.GameSpeed);
        }

        public void OnTriggerEnter(Collider other) {
            //If the player collides with the collectable, destroy the collectable
            if (other.CompareTag("Player"))
                Destroy(gameObject);
        }
    }
}