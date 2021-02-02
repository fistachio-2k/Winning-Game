using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Deer : MonoBehaviour
{
    [SerializeField] private float _duration = 10f;
    public void MoveDeer()
    {
        StartCoroutine(DeerCoroutine());
    }

    IEnumerator DeerCoroutine()
    {
        yield return new WaitForSeconds(1f);
        transform.DOLocalMove(new Vector3(0.72f,0f,10f), _duration);
        transform.DOLocalRotate(transform.localRotation.eulerAngles + Vector3.up * 90f, _duration);
    }
}
