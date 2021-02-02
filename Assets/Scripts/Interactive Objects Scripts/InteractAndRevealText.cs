using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAndRevealText : MonoBehaviour, IInteractable
{
    [SerializeField] private TextReveal text;

    public void Interact()
    {
        if(text != null)
        {
            StartCoroutine(text.RevealText());
        }
    }
}
