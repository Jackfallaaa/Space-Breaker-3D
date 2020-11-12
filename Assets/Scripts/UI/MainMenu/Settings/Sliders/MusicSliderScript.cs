using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu.Settings.Sliders {
    public class MusicSliderScript : MonoBehaviour {
        private Slider _slider;

        private void Start() {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(OnValueChangedListener);
            _slider.value = PlayerPrefs.GetFloat("musicVolume", 100);
        }

        private void OnValueChangedListener(float value) {
            AudioManager.instance.SetMusicVolume(value / 100);
            PlayerPrefs.SetFloat("musicVolume", value);
        }
    }
}