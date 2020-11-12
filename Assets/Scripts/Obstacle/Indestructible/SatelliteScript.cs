using Audio;

namespace Obstacle.Indestructible {
    public class SatelliteScript : IndestructibleObstacleScript {
        protected override void DestroyObstacle(bool withParticle, bool withSound) {
            //Play the satellite destroy sound effect
            AudioManager.Play("SatelliteDestroy");

            base.DestroyObstacle(withParticle, withSound);
        }
    }
}