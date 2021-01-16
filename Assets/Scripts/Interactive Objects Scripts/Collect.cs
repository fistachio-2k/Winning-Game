using UnityEngine;
using DG.Tweening;

public class Collect : MonoBehaviour
{
    [SerializeField] private float _duration = 3f;
    [SerializeField] private float _distanceFromCamera = -1f;
    private Camera _camera;
    private Sequence _s;
    // Start is called before the first frame update
    
    void Start()
    {
        _camera = Camera.main;
        _s = DOTween.Sequence();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetTestButton2() && !_s.IsActive())
        {
            CollectMe();
        }
    }

    void CollectMe()
    {
        //TODO: add sound!
        _s.Append(transform.DOMove(_camera.transform.position + _camera.transform.forward * _distanceFromCamera, _duration));
        _s.Join(transform.DORotate(Vector3.up * 360f, _duration, RotateMode.WorldAxisAdd).SetEase(Ease.Linear).SetLoops(1));
        _s.Play();
        Destroy(gameObject, _duration);
    }
}
