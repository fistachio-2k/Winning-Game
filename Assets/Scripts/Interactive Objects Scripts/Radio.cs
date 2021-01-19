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
        Sound[] sounds = FindObjectOfType<AudioManager>().sounds;
        Sound s = Array.Find(sounds, sound => sound.name == "MainMusic");
        if (s == null)
        {
            Debug.LogWarning("Sound: " + "MainMusic" + " not found!");
            return;
        }
        s.source.volume -= 0.05f;
        //if (s.source.volume = 0f)
        //{
        //    s.source.volume = 0.2;
        //}
    }
}
