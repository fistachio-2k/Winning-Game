using UnityEngine;
using DG.Tweening;


public class Fridge : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _fridgeDoor;
    [SerializeField] private float _openAngle = 80f;
    [SerializeField] private float _duration = 3f;
    private Sequence _sF;
    private Sequence _sB;
    private bool _isOpen = false;

    private void Start()
    {
        _sF = DOTween.Sequence();
        _sF.SetAutoKill(false);

        _sB = DOTween.Sequence();
        _sB.SetAutoKill(false);
    }

    public void OpenCloseFridge()
    {
        //TODO: add sound!
        if (_isOpen)
        {
            _sB.Append(_fridgeDoor.DOLocalRotate(new Vector3(0f, 0f, 0f), _duration));
            _sB.Join(_fridgeDoor.DOLocalMoveZ(1.69f, _duration));
            _sB.Play();
            _isOpen = false;
        }
        else
        {
            _sF.Append(_fridgeDoor.DOLocalRotate(new Vector3(0f, -_openAngle, 0f), _duration));
            _sF.Join(_fridgeDoor.DOLocalMoveZ(1.6f, _duration));
            _sF.Play();
            _isOpen = true;
        }
    }


    public void Interact()
    {
        OpenCloseFridge();
    }
}
