using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface ISelectionResponse
{
    void OnDeselect(Transform selection);
    void OnSelect(Transform selection);
}

internal class HighlightSelectionResponse : MonoBehaviour, ISelectionResponse
{

    public void OnSelect(Transform selection)
    {
        Outline outline = selection.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = true;
        }
    }

    public void OnDeselect(Transform selection)
    {
        Outline outline = selection.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }
    }
}
