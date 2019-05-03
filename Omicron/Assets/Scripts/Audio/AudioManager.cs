using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager Instance = null;
    
    private void Awake() 
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            // Set clip, volume and pitch variables the sound's clip, volume and pitch
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
        }
    }

    private void Start() {
        //Play("Test");
    }

    public void Play (string name)
    {
        // Find sound in the sounds array
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        // Do null check and throw error if sound of specified name does not exist
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " not found!");
            return;
        }
        // Play the sound if sound is found
        s.Source.Play();
    }
}
