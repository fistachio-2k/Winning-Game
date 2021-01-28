using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondHouseEnter : MonoBehaviour
{
    [SerializeField] private GameObject radio1;
    [SerializeField] private GameObject radio2;
    [SerializeField] private GameObject openBasement;
    [SerializeField] private GameObject wallsOpenBasement;
    [SerializeField] private GameObject breakfastBasment;
    [SerializeField] private GameObject wallsBreakfastBasment;
    [SerializeField] private GameObject door;
    bool basementFlag = true;

    private void Start()
    {
        openBasement.SetActive(true);
        breakfastBasment.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        radio1.GetComponent<AudioSource>().Stop();
        radio2.GetComponent<AudioSource>().Play();
        basementFlag = !basementFlag;
        openBasement.SetActive(basementFlag);
        wallsOpenBasement.SetActive(basementFlag);
        breakfastBasment.SetActive(!basementFlag);
        wallsBreakfastBasment.SetActive(!basementFlag);
        StartCoroutine(door.GetComponent<Door>().OpenClose());
        door.GetComponent<Door>()._isLocked = true;
    }
}
