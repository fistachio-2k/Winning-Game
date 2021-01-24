using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour, IInteractable
{
    private bool _isOpen = false;
    [SerializeField] private bool _isLocked = false;
    private Vector3 _baseRotation;
    [SerializeField] float duration = 2f;

    void Start()
    {
        _baseRotation = transform.rotation.eulerAngles;
    }

    public void Interact()
    {
        StartCoroutine(OpenClose());
    }

    IEnumerator OpenClose()
    {   
        //TODO: Add locked / Open / Close sounds
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
        yield return null;
    }
}
