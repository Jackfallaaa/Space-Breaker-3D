using UnityEngine;

namespace Game {
    public class BackgroundScript : MonoBehaviour {
        private ParticleSystem.MainModule _backgroundMain;
        private ParticleSystem.MainModule _innerMain;
        private ParticleSystem.MainModule _outerMain;

        public ParticleSystem backdropParticle;
        public ParticleSystem innerParticle;
        public ParticleSystem outerParticle;

        private void Start() {
            _outerMain = outerParticle.main;
            _innerMain = innerParticle.main;
            _backgroundMain = backdropParticle.main;
        }

        public void IncreaseSpeed(float multiplier) {
            //Increase the background speed by applying a multiplier to the simulation speed of each component
            _outerMain.simulationSpeed *= multiplier;
            _innerMain.simulationSpeed *= multiplier;
            _backgroundMain.simulationSpeed *= multiplier;
        }
    }
}