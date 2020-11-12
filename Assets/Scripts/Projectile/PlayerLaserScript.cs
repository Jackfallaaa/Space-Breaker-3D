using Game;
using UnityEngine;

namespace Projectile {
    public class PlayerLaserScript : MonoBehaviour {
        private void Start() {
            //Delay before destroying the player laser
            Destroy(gameObject, GameManager.PlayerLaserDestroyDelay);

            //Set the player laser rotation to target the obstacles
            var rotation = transform.rotation;
            transform.Rotate(90, rotation.y, rotation.z);

            //Move the player laser towards the obstacles at a constant speed
            GetComponent<Rigidbody>().AddForce(Vector3.forward * GameManager.PlayerLaserSpeed);
        }
    }
}