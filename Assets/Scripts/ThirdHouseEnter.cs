using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdHouseEnter : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    private void OnTriggerEnter(Collider other)
    {
        audioManager.Stop("MainSecond");
        audioManager.Play("MainMusic2");
    }
}
