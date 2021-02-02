using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForthHouseTrigger : MonoBehaviour
{
    [SerializeField] private GameObject radio3;
    [SerializeField] private GameObject radio4;
    [SerializeField] private GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        //Handle the sounds
        radio3.GetComponent<AudioSource>().Stop();
        radio4.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerExit(Collider other)
    {
        //Close the door after character enter
        if (door.GetComponent<Door>().isOpen)
        {
            StartCoroutine(door.GetComponent<Door>().OpenClose());
        }
        door.GetComponent<Door>()._isLocked = true;
        gameObject.GetComponent<Collider>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}
