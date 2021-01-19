using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraCenter : MonoBehaviour
{
    public TextMesh _cameraCenter;
    float radius = 5;

    private void Awake()
    {
        _cameraCenter = GetComponent<TextMesh>();
    }
    void Update()
    {
        _cameraCenter.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane * 20));
        _cameraCenter.transform.rotation = Camera.main.transform.rotation;
    }

    public bool isInPlayersView(GameObject obj)
    {
        return Mathf.Abs(Vector3.Distance(obj.transform.position, _cameraCenter.transform.position)) < radius;
    }
}
