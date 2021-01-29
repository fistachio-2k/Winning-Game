using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshCollider))]
public class Fridge : MonoBehaviour, IInteractable
{
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
        //TODO Mira: add sound!
        if (_isOpen)
        {
            FindObjectOfType<AudioManager>().Play("Open");
            _sB.Append(transform.DOLocalRotate(new Vector3(0f, 90f, 0f), _duration));
            _sB.Join(transform.DOLocalMoveZ(1.7f, _duration));
            _sB.Play();
            _isOpen = false;
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Open");
            _sF.Append(transform.DOLocalRotate(new Vector3(3f, 0f, 0f), _duration));
            _sF.Join(transform.DOLocalMoveZ(1.64f, _duration));
            _sF.Play();
            _isOpen = true;
        }
    }


    public void Interact()
    {
        OpenCloseFridge();
    }
}
