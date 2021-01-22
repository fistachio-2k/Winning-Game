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
        audioManager.ToggleMainMusic();
        StartCoroutine(text.RevealText());
    }
}
