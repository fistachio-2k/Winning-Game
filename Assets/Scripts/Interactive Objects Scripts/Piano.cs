using UnityEngine;
using DG.Tweening;
using System.Collections;
public class Piano : MonoBehaviour
{
    [SerializeField] private Transform positionToMove;
    [SerializeField] private float duration = 5f;

    private Vector3 _basePosition;

    private void Start()
    {
        _basePosition = transform.position;
    }
    public void MovePiano()
    {
        StartCoroutine(PianoCoroutine());
    }

    IEnumerator PianoCoroutine()
    {
        yield return new WaitForSeconds(1f);
        transform.DOMoveZ(positionToMove.position.z, duration);
        transform.DOMoveX(positionToMove.position.x, duration);
    }

    public void RestorePianoPosition()
    {
        transform.position = _basePosition;
    }
    
}
