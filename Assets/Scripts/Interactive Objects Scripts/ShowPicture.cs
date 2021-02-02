using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ShowPicture : MonoBehaviour, IInteractable
{
    [SerializeField] private float _duration = 3f;
    [SerializeField] private float _distanceFromCamera = -0.001f;
    [SerializeField] private bool _isANote = false;
    [SerializeField] private bool _isALetter = false;
    [SerializeField] public bool stopShowing = false;

    private Camera _camera;
    private bool _inInteraction = false;
    private Vector3 _baseRotation;
    private Vector3 _basePlacement;
    private Renderer _renderer;
    private Sequence _s;


    void Start()
    {
        _camera = Camera.main;
        _s = DOTween.Sequence();
        _basePlacement = transform.position;
        _baseRotation = transform.rotation.eulerAngles;
    }

    public void Interact()
    {
        if(!stopShowing)
        {
            if (!_inInteraction)
            {
                _inInteraction = true;
                GameEventsManager._instance.DisableMovement();
                StartCoroutine(ShowPic());
            }
            else
            {
                _inInteraction = false;
                StartCoroutine(PutBackPic());
            }
        }
    }

    IEnumerator ShowPic()
    {
        FindObjectOfType<AudioManager>().Play("paper1");
        _s.Append(gameObject.transform.DOMove(_camera.transform.position + _camera.transform.forward * _distanceFromCamera, _duration));
        if (!_isANote && !_isALetter)
        {
            _s.Join(gameObject.transform.DORotate(_camera.transform.rotation.eulerAngles + Vector3.up * -90f, _duration));
        }
        else if(_isANote)
        {
            _s.Join(gameObject.transform.DORotate(_camera.transform.rotation.eulerAngles + Vector3.right * 50 + Vector3.up * 180f, _duration));
        }
        else
        {
            _s.Join(gameObject.transform.DORotate(_camera.transform.rotation.eulerAngles + Vector3.right * 50 + Vector3.up * 180f, _duration));
        }

        _s.Play();
        yield return new WaitForSeconds(1f);
        _inInteraction = true;
    }

    IEnumerator PutBackPic()
    {
        FindObjectOfType<AudioManager>().Play("paper1");
        gameObject.transform.DOMove(_basePlacement, _duration);
        gameObject.transform.DORotate(_baseRotation, _duration);
        yield return new WaitForSeconds(1f);
        GameEventsManager._instance.EnableMovement();
    }

    public void StopShow()
    {
        stopShowing = true;
    }
}
