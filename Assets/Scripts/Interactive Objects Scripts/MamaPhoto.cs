using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaPhoto : MonoBehaviour, IInteractable
{
    [SerializeField] private TextReveal phototText;

    public void Interact()
    {
        StartCoroutine(phototText.RevealText());
    }
}
