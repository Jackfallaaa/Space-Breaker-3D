using UnityEngine;

namespace Game {
    public class DestroyerScript : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            //Destroy any object that collides with the Destroyer
            Destroy(other.gameObject);
        }
    }
}