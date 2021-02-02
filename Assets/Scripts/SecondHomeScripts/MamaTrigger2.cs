using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaTrigger2 : MonoBehaviour
{
    void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameEventsManager._instance.PlayAfterResipy(gameObject);
    }
}
