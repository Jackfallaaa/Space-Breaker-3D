using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ButtonScript : MonoBehaviour {
        private void Start() {
            GetComponent<Button>().onClick.AddListener(OnClickListener);
        }

        private void OnClickListener() {
            //Play the Button Click sound effect when the player clicks a button
            AudioManager.Play("ButtonClick");
        }
    }
}