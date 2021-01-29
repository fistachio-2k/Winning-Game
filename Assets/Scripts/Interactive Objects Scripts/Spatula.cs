using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Spatula : MonoBehaviour, IInteractable
{
    [SerializeField] public bool timeToFry = false;
    [SerializeField] float duration = 2f;
    private AudioManager audioManager;
    private Vector3 _baseRotation;
    private Vector3 _basePlacement;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        _baseRotation = transform.rotation.eulerAngles;
        _basePlacement = transform.position;
    }

    public void Interact()
    {
        if (timeToFry)
        {
            StartCoroutine(fryTheOnions());
            GameEventsManager._instance.isFrying = true;
        }
    }

    IEnumerator fryTheOnions()
    {
        audioManager.Play("Frying");
        transform.DORotate(_baseRotation + (Vector3.up * 180f) + (Vector3.forward * 4f) + (Vector3.right * -4f), duration);
        transform.DOMove(_basePlacement + (Vector3.right * 0.9f) + (Vector3.up * 0.2f) + (Vector3.forward), duration);
        yield return new WaitForSeconds(2f);
        audioManager.Stop("Frying");
        yield return null;
    }
}
