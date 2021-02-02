using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaTrigger1 : MonoBehaviour
{
    [SerializeField] private Collider secondTriggerCollider;

    private void OnTriggerEnter(Collider other)
    {
        GameEventsManager._instance.PlayMamaEstherScene(gameObject);
        GetComponent<Collider>().enabled = false;
        secondTriggerCollider.enabled = true;
    }
}
