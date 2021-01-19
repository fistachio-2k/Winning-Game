using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOutline : MonoBehaviour
{
    public CameraCenter cameraCenter;
    void Start()
    {
        
    }


    void Update()
    {
        if(cameraCenter.GetComponent<CameraCenter>().isInPlayersView(gameObject))
        {
            transform.GetComponent<cakeslice.Outline>().EnableOutline();
        }
        else
        {
            transform.GetComponent<cakeslice.Outline>().DisableOutline();
        }
    }
}
