using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class CorridorPlayerEffect : MonoBehaviour
{
    [SerializeField] 
    private float _heightTarget = -0.5f;
    [SerializeField]
    private Transform _pov = null;
    private Vector3 _originalPos;
    private Vector3 _min;
    private Vector3 _max;
    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private float _corridorLength;

    public Plane EndPlane;

    private void Start()
    {
        var bounds = gameObject.GetComponent<BoxCollider>().bounds;
        _min = bounds.center - Vector3.forward * bounds.extents.z;
        _max = bounds.center + Vector3.forward * bounds.extents.z;
        _corridorLength = Vector3.Distance(_max, _min);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _pov = other.transform.GetChild(0);
            _originalPos = _pov.localPosition;

            Vector3 pos = _pov.position;
            if (Vector3.Distance(pos, _min) < Vector3.Distance(pos, _max))
            {
                _startPoint = _min;
                _endPoint = _max;
                
            }
            else
            {
                _startPoint = _max;
                _endPoint = _min;
            }
            EndPlane = new Plane(_startPoint - _endPoint, _endPoint);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _pov != null)
        {
            float t = EndPlane.GetDistanceToPoint(other.transform.position) / _corridorLength;
            t = 1 - Mathf.Clamp(t, 0f, 1f);
            _pov.transform.DOLocalMoveY(_originalPos.y + _heightTarget * t, 0.5f);
        }
    }
}
