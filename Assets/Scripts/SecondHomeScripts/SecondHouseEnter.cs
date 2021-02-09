using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SecondHouseEnter : MonoBehaviour
{
    [SerializeField] private GameObject radio1;
    [SerializeField] private GameObject radio2;
    [SerializeField] private GameObject wallsOpenBasement;
    [SerializeField] private GameObject breakfastBasment;
    [SerializeField] private GameObject wallsBreakfastBasment;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject pointOfView;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource cooking;
    [SerializeField] float cameraAnimationDuration = 1f;
    [SerializeField] float down = -0.5f;
    bool basementFlag = true;
    

    private void Start()
    {
        breakfastBasment.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Handle the sounds
        radio1.GetComponent<AudioSource>().Stop();
        radio2.GetComponent<AudioSource>().Play();

        //Handle the basment
        basementFlag = !basementFlag;
        wallsOpenBasement.SetActive(basementFlag);
        breakfastBasment.SetActive(!basementFlag);
        wallsBreakfastBasment.SetActive(!basementFlag);

    }

    private void OnTriggerExit(Collider other)
    {
        //Lower the camera and faster the character steps
        cooking.Play();
        StartCoroutine(LowerPlayerView());
        GetComponent<Collider>().enabled = false;
        
    }

    IEnumerator LowerPlayerView()
    {
        GameEventsManager._instance.DisableMovement();

        pointOfView.transform.DOLocalMoveY(pointOfView.transform.localPosition.y + down, cameraAnimationDuration);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.playerSpeed += 1.5f;
        playerController.gravityValue += 1.0f;

        //Close the door after character enter
        if (door.GetComponent<Door>().isOpen)
        {
            StartCoroutine(door.GetComponent<Door>().OpenClose());
        }
        door.GetComponent<Door>()._isLocked = true;
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(cameraAnimationDuration);

        GameEventsManager._instance.EnableMovement();
    }
}
