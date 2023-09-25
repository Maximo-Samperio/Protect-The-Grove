using UnityEngine.Audio;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;                                  // I place all sounds in an array

    public static AudioManager instance;                    // Creates an instance of AM for scene transition
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);                      // Does exactly that

        foreach (Sound s in sounds)                         // I assign the following to all sounds in the array:
        {
            s.source = gameObject.AddComponent<AudioSource>();  // Audio source
            s.source.clip = s.clip;                         // The audio itself

            s.source.volume = s.volume;                     // Volume of the clip
            s.source.pitch = s.pitch;                       // Pitch of the clip
            s.source.loop = s.loop;                         // Bool for looping the clip
        }
    }

    void Start()
    {
        //Play("Star_Striker");
    }
    public void Play(string name)                           //Void to play sounds
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);      //Var to find a sound in the array where the specified name in X case scenario matches one in the array

        if (s == null)                                      // Console log to check for typos'
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        //if (PauseMenu.GameIsPaused)
        //{
        //    s.source.pitch *= .5f;
        //}
        //else
        //{
        //    s.source.pitch = 1f;
        //}

        s.source.Play();                                    // Plays the audio from the source
    }
}