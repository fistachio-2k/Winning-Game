using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecipeBox : MonoBehaviour, IInteractable
{
    private bool _isOpen = false;
    private Vector3 _baseRotation;
    [SerializeField] private bool _isLocked = false;
    [SerializeField] float duration = 2f;
    private AudioManager audioManager;

    void Start()
    {
        _baseRotation = transform.rotation.eulerAngles;
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Interact()
    {
        StartCoroutine(OpenClose());
    }

    IEnumerator OpenClose()
    {
        if (!_isLocked)
        {
            _isOpen = !_isOpen;
            if (_isOpen)
            {
                transform.DORotate(_baseRotation, duration);
            }
            else
            {
                transform.DORotate(_baseRotation + (Vector3.up * 90f) + (Vector3.forward * 3f) + (Vector3.right * -3f), duration);
            }
        }
        else
        {
            audioManager.Play("LockedDoor");
        }
        yield return null;
    }
}
