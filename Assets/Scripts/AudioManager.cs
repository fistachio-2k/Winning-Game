using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        Play("MainMusic");
    }



    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void ToggleMainMusic()
    {
        Sound s1 = Array.Find(sounds, sound => sound.name == "MainMusic");
        Sound s2 = Array.Find(sounds, sound => sound.name == "MainMusic1");
        if (s1 == null || s2 == null )
        {
            Debug.LogWarning("Sound: " + "MainMusic" + " not found!");
            return;
        }
        if (s1.source.isPlaying)
        {
            Stop("MainMusic");
            Play("MainMusic1");
        }
        else if(s2.source.isPlaying)
        {
            Stop("MainMusic1");
            Play("MainMusic");
        }
    }
}
