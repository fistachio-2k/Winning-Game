using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Dresser : MonoBehaviour
{
    [SerializeField] private Transform positionToMove;
    [SerializeField] private float duration = 5f;

    public void MoveDresser()
    {
        StartCoroutine(PianoCoroutine());
    }

    IEnumerator PianoCoroutine()
    {
        yield return new WaitForSeconds(1f);
        transform.DOMoveZ(positionToMove.position.z, duration);
        transform.DOMoveX(positionToMove.position.x, duration);
    }
}
