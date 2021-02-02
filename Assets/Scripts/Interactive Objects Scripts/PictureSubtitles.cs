using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureSubtitles : MonoBehaviour, IInteractable
{
    [SerializeField] private int subtitleIdx;
    private SubtitleManager subtitleManager;

    private void Start()
    {
        subtitleManager = FindObjectOfType<SubtitleManager>();
    }
    public void Interact()
    {
        StartCoroutine(FindObjectOfType<SubtitleManager>().ShowMe(subtitleIdx, "other"));
    }
}
