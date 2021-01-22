using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOutline : MonoBehaviour
{
    public CameraCenter cameraCenter;
    void Start()
    {
        cameraCenter = FindObjectOfType<Canvas>().GetComponent<CameraCenter>();
    }


    void Update()
    {
        if (cameraCenter != null)
        {
            if (cameraCenter.isInPlayersView(gameObject))
            {
                transform.GetComponent<cakeslice.Outline>().EnableOutline();
            }
            else
            {
                transform.GetComponent<cakeslice.Outline>().DisableOutline();
            }
        }
    }
}
