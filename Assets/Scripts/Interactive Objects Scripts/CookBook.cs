using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookBook : MonoBehaviour, IInteractable
{
    [SerializeField] private TextReveal bookText;

    public void Interact()
    {
        StartCoroutine(bookText.RevealText());
    }
}
