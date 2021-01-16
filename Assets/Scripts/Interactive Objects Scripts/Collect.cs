using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Collect : MonoBehaviour
{
    [SerializeField] private float _duration = 3f;
    [SerializeField] private float _distanceFromCamera = -1f;
    private Camera _camera;
    private Sequence _s;
    private bool _collected = false;
    private Renderer _renderer;
    // Start is called before the first frame update
    
    void Start()
    {
        _camera = Camera.main;
        _renderer = gameObject.GetComponent<Renderer>();
        _s = DOTween.Sequence();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetTestButton2() && !_collected)
        {
            _collected = true;
            GameEventsManager._instance.AddCollectedItem(gameObject.GetHashCode());
            StartCoroutine(CollectMe());
        }
    }

    IEnumerator CollectMe()
    {
        //TODO: add sound!
        _s.Append(transform.DOMove(_camera.transform.position + _camera.transform.forward * _distanceFromCamera, _duration));
        _s.Join(transform.DORotate(Vector3.up * 360f, _duration, RotateMode.WorldAxisAdd).SetEase(Ease.Linear).SetLoops(1));
        _s.Play();
        while (_renderer.isVisible)
        {
            yield return null;
        }
        _renderer.enabled = false;
        Destroy(gameObject, _duration);
    }
}
