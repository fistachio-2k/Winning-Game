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
    [SerializeField] private AudioSource cooking;
    [SerializeField] float cameraAnimationDuration = 1f;
    [SerializeField] float up = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        //Handle the sounds
        radio2.GetComponent<AudioSource>().Stop();
        radio3.GetComponent<AudioSource>().Play();
        StartCoroutine(FindObjectOfType<SubtitleManager>().ShowMe(7, "other"));
        cooking.Stop();
    }

    private void OnTriggerExit(Collider other)
    {
        //Close the door after character enter
        StartCoroutine(HigherPlayerView());
        GetComponent<Collider>().enabled = false;
    }

    IEnumerator HigherPlayerView()
    {
        GameEventsManager._instance.DisableMovement();
        if (door.GetComponent<Door>().isOpen)
        {
            StartCoroutine(door.GetComponent<Door>().OpenClose());
        }
        door.GetComponent<Door>()._isLocked = true;

        //Lower the camera and faster the character steps
        pointOfView.transform.DOLocalMoveY(pointOfView.transform.localPosition.y + up, cameraAnimationDuration);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.playerSpeed -= 1.5f;
        playerController.gravityValue -= 1.0f;
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(cameraAnimationDuration);
        GameEventsManager._instance.EnableMovement();
    }
}
