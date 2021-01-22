using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraCenter : MonoBehaviour
{
    float radius = 3;

    public bool isInPlayersView(GameObject obj)
    {
        return Mathf.Abs(Vector3.Distance(obj.transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane * 20)))) < radius;
    }
}
