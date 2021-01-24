using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondHouseEnter : MonoBehaviour
{
    public AudioManager audioManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        audioManager.Stop("MainMusic");
        audioManager.Stop("MainMusic1");
        audioManager.Stop("MainSecond");
    }
}
