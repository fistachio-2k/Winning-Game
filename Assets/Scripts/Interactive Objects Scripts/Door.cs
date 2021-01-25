using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    private bool _isOpen = false;
    private bool _inOpenScene = true;
    [SerializeField] private bool _isLocked = false;
    private Vector3 _baseRotation;
    [SerializeField] float duration = 2f;
    [SerializeField] private TextReveal text = null;
    private AudioManager audioManager;

    void Start()
    {
        _baseRotation = transform.rotation.eulerAngles;
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Interact()
    {
        StartCoroutine(OpenClose());
        if (_inOpenScene)
        {
            if(text != null)
            {
                StartCoroutine(text.RevealText());
            }
        }
    }

    IEnumerator OpenClose()
    {   
        if (!_isLocked)
        {
            _isOpen = !_isOpen;
            audioManager.Play("OpenDoor");
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
