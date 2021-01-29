using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnsweringMachine : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameEventsManager._instance.PlayAnsweringMachine(gameObject);
    }
}
