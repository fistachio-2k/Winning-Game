using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string volumePref = "volumePref";
    private int firstPlayInt;
    private float volumeFloat;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource[] soundEffects;

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
        Play("BackroundSound");

        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        if (firstPlayInt == 0)
        {
            volumeFloat = .125f;
            volumeSlider.value = volumeFloat;
            PlayerPrefs.SetFloat(volumePref, volumeFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        { 
            volumeFloat = PlayerPrefs.GetFloat(volumePref);
            volumeSlider.value = volumeFloat;
        }
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

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Pause();
    }

    public void ToggleMusic(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null )
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (s.source.isPlaying)
        {
            Pause(name);
        }
        else
        {
            Play(name);
        }
    }

    public void updateSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = volumeSlider.value;
        }
        for (int i = 0; i < soundEffects.Length; i++)
        {
            soundEffects[i].volume = volumeSlider.value;
        }
    }

    public void SaveSoundSetttings()
    {
        PlayerPrefs.SetFloat(volumePref, volumeSlider.value);
    }
}
