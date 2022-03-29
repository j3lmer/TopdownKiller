using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public class Sound
    {
        public AudioClip audioClip;
        public string name;

        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;

        [HideInInspector]
        public AudioSource source;

        public Sound(string name, AudioClip audioClip)
        {
            this.name = name;
            this.audioClip = audioClip;
        }
    }
}