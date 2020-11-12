using System;
using UnityEngine;

namespace Audio {
    [Serializable]
    public class Audio {
        public AudioClip clip;
        public bool loop;
        public string name;
        public float pitch;
        public AudioSource source;
    }
}