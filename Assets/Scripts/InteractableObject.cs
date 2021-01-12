using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : HouseObjectScript
{

    CameraCenter cameraCenter;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    float amplitude = 0.2f;
    float frequency = 1f;
    float heightFactor = 0.2f;

    void Start()
    {
        cameraCenter = GameObject.Find("CameraCenter").GetComponent<CameraCenter>();
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraCenter.isInPlayersView(gameObject))
        {
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude + heightFactor;
            transform.position = tempPos;
        }
        else
        {
            transform.position = posOffset;
        }
        
    }
}
