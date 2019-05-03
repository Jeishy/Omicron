using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    // Name of the sound
    public string Name;
    // Audio clip of the sound
    public AudioClip Clip;
    // The sound's volume
    [Range(0f, 1f)]
    public float Volume;
    // The sound's pitch
    [Range(0.1f, 3f)]
    public float Pitch;
    // Audio source that sound plays from
    [HideInInspector] public AudioSource Source;
}
