using UnityEngine;

namespace Game {
    public class CameraScript : MonoBehaviour {
        public float elevationAngle;
        public float followDistance;
        public Transform player;

        private void LateUpdate() {
            //Set the camera transform to match the player position + an elevation angle and follow distance
            transform.position = player.position + Quaternion.Euler(elevationAngle, 0, 0) * new Vector3(0, 0, -followDistance);

            //Rotate the camera transform so the forward vector points at the target's current position
            transform.LookAt(player);
        }
    }
}