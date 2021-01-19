using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Radio");
        audioManager.ToggleMainMusic();
    }
}
