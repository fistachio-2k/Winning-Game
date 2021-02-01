using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    [SerializeField] private TextReveal text;
    [SerializeField] private Sound[] radioSongs;
    [SerializeField] private bool first = false; // temp! TODO - to delete
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
        if (first)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    public void Interact()
    {
        //foreach (Sound s in radioSongs)
        //{
        //    s.source.Pause();
        //}
        //songIdx += 1;
        //songIdx = songIdx % (radioSongs.Length - 1);
        //radioSongs[songIdx].source.Play();
        AudioSource s = gameObject.GetComponent<AudioSource>();
        if (s.isPlaying)
        {
            s.Pause();
        }
        else
        {
            audioManager.Play("Radio");
            s.Play();
        }
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
