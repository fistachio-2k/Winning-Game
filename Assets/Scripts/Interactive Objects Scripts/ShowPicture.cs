using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPicture : MonoBehaviour, IInteractable
{
    [SerializeField] private Canvas canvasForPic;
    private bool inInteraction = false;


    void Start()
    {
        canvasForPic.enabled = false;
    }

    public void Interact()
    {
        canvasForPic.enabled = !canvasForPic.enabled;
        if (canvasForPic.enabled)
        {
            inInteraction = true;
        }
    }

    private void Update()
    {
        if(inInteraction && GameEventsManager._instance.GetMouseClick())
        {
            inInteraction = false;
        }
    }
}
