using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThirdHouseEnter : MonoBehaviour
{
    [SerializeField] private GameObject radio2;
    [SerializeField] private GameObject radio3;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject pointOfView;
    [SerializeField] private GameObject player;
    [SerializeField] float cameraAnimationDuration = 1f;
    [SerializeField] float up = 0.8f;

    private void OnTriggerEnter(Collider other)
    {
        //Handle the sounds
        radio2.GetComponent<AudioSource>().Stop();
        radio3.GetComponent<AudioSource>().Play();

        //Close the door after character enter
        StartCoroutine(door.GetComponent<Door>().OpenClose());
        door.GetComponent<Door>()._isLocked = true;

        //Lower the camera and faster the character steps
        pointOfView.transform.DOMove(pointOfView.transform.position + (Vector3.up * up), cameraAnimationDuration);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.playerSpeed -= 1.0f;
        playerController.gravityValue -= 1.0f;
    }
}
