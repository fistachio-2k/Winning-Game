using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SecondHouseEnter : MonoBehaviour
{
    [SerializeField] private GameObject radio1;
    [SerializeField] private GameObject radio2;
    [SerializeField] private GameObject openBasement;
    [SerializeField] private GameObject wallsOpenBasement;
    [SerializeField] private GameObject breakfastBasment;
    [SerializeField] private GameObject wallsBreakfastBasment;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject pointOfView;
    [SerializeField] private GameObject player;
    [SerializeField] float cameraAnimationDuration = 1f;
    [SerializeField] float down = -0.5f;
    bool basementFlag = true;
    

    private void Start()
    {
        openBasement.SetActive(true);
        breakfastBasment.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Handle the sounds
        radio1.GetComponent<AudioSource>().Stop();
        radio2.GetComponent<AudioSource>().Play();

        //Handle the basment
        basementFlag = !basementFlag;
        openBasement.SetActive(basementFlag);
        wallsOpenBasement.SetActive(basementFlag);
        breakfastBasment.SetActive(!basementFlag);
        wallsBreakfastBasment.SetActive(!basementFlag);

    }

    private void OnTriggerExit(Collider other)
    {
        //Lower the camera and faster the character steps
        pointOfView.transform.DOLocalMoveY(pointOfView.transform.localPosition.y + down, cameraAnimationDuration);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.playerSpeed += 1.0f;
        playerController.gravityValue += 1.0f;

        //Close the door after character enter
        StartCoroutine(door.GetComponent<Door>().OpenClose());
        door.GetComponent<Door>()._isLocked = true;
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
