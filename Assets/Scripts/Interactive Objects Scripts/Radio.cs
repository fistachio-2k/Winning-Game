using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    [SerializeField] private TextReveal text;
    [SerializeField] private Sound[] radioSongs;
    private int songIdx = -1;
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        foreach (Sound s in radioSongs)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = 1f;
        }
    }

    public void Interact()
    {
        foreach (Sound s in radioSongs)
        {
            s.source.Pause();
        }
        songIdx++;
        //songIdx = songIdx % (radioSongs.Length - 1);
        radioSongs[songIdx].source.Play();
        //AudioSource s = gameObject.GetComponent<AudioSource>();
        //if (s.isPlaying)
        //{
        //    s.Pause();
        //}
        //else
        //{
        //    audioManager.Play("Radio");
        //    s.Play();
        //}
        if (text != null)
        {
            StartCoroutine(text.RevealText());
        }
        
    }

    public void MuteMusic()
    {
        gameObject.GetComponent<AudioSource>().Stop();
        audioManager.Play("Radio");
        audioManager.Play("WhiteNoise");
    }
}
