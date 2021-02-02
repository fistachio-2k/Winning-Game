using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    private bool _inOpenScene = true;
    private Vector3 _baseRotation;
    private AudioManager audioManager;
    [SerializeField] float duration = 2f;
    [SerializeField] private TextReveal text = null;
    [SerializeField] public bool _isLocked = false;
    [SerializeField] public bool _isLastScene = false;
    public bool isOpen = false;

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

    public IEnumerator OpenClose()
    {   
        if(_isLastScene)
        {
            audioManager.Play("UnlockDoor");
            _isLocked = false;
            StartCoroutine(GameEventsManager._instance.StartLastScene());
        }
        if (!_isLocked)
        {
            audioManager.Play("OpenDoor");
            if (isOpen)
            {
                //close the door
                transform.DORotate(_baseRotation, duration);
                isOpen = false;
            }
            else
            {
                //open the door
                transform.DORotate(_baseRotation + (Vector3.up * 90f) + (Vector3.forward * 3f) + (Vector3.right * -3f), duration);
                isOpen = true;
            }
        }
        else
        {
            audioManager.Play("LockedDoor");
        }
        yield return null;
    }

    public void Unlock()
    {
        _isLastScene = true;
        _isLocked = false;
    }
}
