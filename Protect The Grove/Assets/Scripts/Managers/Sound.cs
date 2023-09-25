using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound                      // I create a class for the sounds in which I add:
{
    public string name;                 // The name of the sound

    public AudioClip clip;              // The sound clip

    [Range(-80f, 1f)]
    public float volume;                // A volume + slider for the sound
    [Range(.1f, 3f)]
    public float pitch;                 // A pitch + slider for the sound

    public bool loop;                   // A bool for looping the sound if so desired

    [HideInInspector]
    public AudioSource source;          // The source of the sound
}