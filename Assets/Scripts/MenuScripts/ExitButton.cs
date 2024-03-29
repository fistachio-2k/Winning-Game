﻿using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(Collider))] //A collider is needed to receive clicks
public class ExitButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameEventsManager._instance.QuitGame();
    }
}
