using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondHouseEnter : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject openBasement;
    [SerializeField] private GameObject wallsOpenBasement;
    [SerializeField] private GameObject breakfastBasment;
    [SerializeField] private GameObject wallsBreakfastBasment;
    bool basementFlag = true;

    private void Start()
    {
        openBasement.SetActive(true);
        breakfastBasment.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        audioManager.Stop("MainMusic");
        audioManager.Stop("MainMusic1");
        audioManager.Play("MainSecond");
        basementFlag = !basementFlag;
        openBasement.SetActive(basementFlag);
        wallsOpenBasement.SetActive(basementFlag);
        breakfastBasment.SetActive(!basementFlag);
        wallsBreakfastBasment.SetActive(!basementFlag);
    }
}
