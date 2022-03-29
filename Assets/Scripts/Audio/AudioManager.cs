using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        
        private void Awake()
        {
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.audioClip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
            DontDestroyOnLoad(gameObject);
        }

        public void Play(string soundName)
        {
            Sound s = Array.Find(sounds, s => s.name == soundName);
            s.source.Play();
        }
    }
}
