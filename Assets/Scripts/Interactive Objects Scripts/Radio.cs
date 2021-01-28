using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    [SerializeField] private TextReveal text;
    public void Interact()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Radio");
        AudioSource s = gameObject.GetComponent<AudioSource>();
        if (s.isPlaying)
        {
            s.Pause();
        }
        else
        {
            s.Play();
        }
        if (text != null)
        {
            StartCoroutine(text.RevealText());
        }
        
    }
}
