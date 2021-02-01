using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementTrigger : MonoBehaviour
{
    [SerializeField] private Door door;

    private void OnTriggerEnter(Collider other)
    {
        door.Interact();
    }
}
