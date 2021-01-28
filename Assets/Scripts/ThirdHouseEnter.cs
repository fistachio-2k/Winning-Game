using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdHouseEnter : MonoBehaviour
{
    [SerializeField] private GameObject radio2;
    [SerializeField] private GameObject radio3;
    [SerializeField] private GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        radio2.GetComponent<AudioSource>().Stop();
        radio3.GetComponent<AudioSource>().Play();
        StartCoroutine(door.GetComponent<Door>().OpenClose());
        door.GetComponent<Door>()._isLocked = true;
    }
}
