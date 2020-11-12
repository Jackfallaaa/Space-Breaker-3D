using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu.Settings.Sliders {
    public class SoundEffectsSliderScript : MonoBehaviour {
        private Slider _slider;

        private void Start() {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(OnValueChangedListener);
            _slider.value = PlayerPrefs.GetFloat("soundEffectsVolume", 100);
        }

        private void OnValueChangedListener(float value) {
            AudioManager.instance.SetSoundEffectsVolume(value / 100);
            PlayerPrefs.SetFloat("soundEffectsVolume", value);
        }
    }
}