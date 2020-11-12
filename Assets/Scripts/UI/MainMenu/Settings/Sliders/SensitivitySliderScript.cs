using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu.Settings.Sliders {
    public class SensitivitySliderScript : MonoBehaviour {
        private Slider _slider;

        private void Start() {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(OnValueChangedListener);
            _slider.value = PlayerPrefs.GetFloat("sensitivityMultiplier", GameManager.PlayerSensitivityMultiplier);
        }

        private void OnValueChangedListener(float value) {
            ;
            GameManager.PlayerSensitivityMultiplier = value;
            PlayerPrefs.SetFloat("sensitivityMultiplier", value);
        }
    }
}