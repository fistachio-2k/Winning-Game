using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaSceneScript : MonoBehaviour
{
    [SerializeField] private SubtitleManager subtitleManager;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(subtitleManager.ShowMe(0, "mama"));
    }
}
