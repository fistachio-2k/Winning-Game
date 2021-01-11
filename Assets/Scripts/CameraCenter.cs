using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraCenter : MonoBehaviour
{
    public TextMesh cameraCenter;
    float radius = 3;

    void Update()
    {
        cameraCenter.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane * 20));
        cameraCenter.transform.rotation = Camera.main.transform.rotation;
    }

    public bool isInPlayersView(GameObject obj)
    {
        return Mathf.Abs(Vector3.Distance(obj.transform.position, cameraCenter.transform.position)) < radius;
    }
}
