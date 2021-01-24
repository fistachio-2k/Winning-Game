using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondHouseEnter : MonoBehaviour
{
    public AudioManager audioManager;

    private void OnTriggerEnter(Collider other)
    {
        audioManager.Stop("MainMusic");
        audioManager.Stop("MainMusic1");
        audioManager.Play("MainSecond");
    }
}
