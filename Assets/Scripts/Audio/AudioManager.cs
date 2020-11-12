using System;
using UnityEngine;

namespace Audio {
    public class AudioManager : MonoBehaviour {
        public static AudioManager instance;
        public Audio music;
        public Audio[] soundEffects;

        private void Awake() {
            //Implements the singleton design pattern by ensuring multiple instances of AudioManager cannot be made
            if (instance == null) {
                instance = this;
            }
            else {
                Destroy(gameObject);
                return;
            }

            //Allows the use of the same AudioManager between scenes
            DontDestroyOnLoad(gameObject);

            //Create a AudioSource component for the music audio, with the volume set as the player's stored preference
            Create(music, PlayerPrefs.GetFloat("musicVolume", 100));

            //Create an AudioSource component for each sound effect, with the volume set as the player's stored preference
            foreach (var soundEffect in soundEffects)
                Create(soundEffect, PlayerPrefs.GetFloat("soundEffectsVolume", 100));
        }

        private void Create(Audio a, float volume) {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.volume = volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
        }

        private void Start() {
            //Starts the background music AudioSource component, which is set to loop indefinitely
            music.source.Play();
        }

        public static void Play(string audioName) {
            //Static method to call the singleton AudioManager instance
            instance._Play(audioName);
        }

        private void _Play(string audioName) {
            //Find and play a given sound effect
            var a = Array.Find(soundEffects, sound => sound.name == audioName);
            a.source.Play();
        }

        public void SetSoundEffectsVolume(float volume) {
            foreach (var a in soundEffects)
                a.source.volume = volume;
        }

        public void SetMusicVolume(float volume) {
            music.source.volume = volume;
        }
    }
}