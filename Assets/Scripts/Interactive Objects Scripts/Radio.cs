using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    [SerializeField] private TextReveal text;
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Interact()
    {
        
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
