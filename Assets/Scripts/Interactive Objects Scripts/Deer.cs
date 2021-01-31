using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Deer : MonoBehaviour
{
    public void MoveDeer()
    {
        StartCoroutine(DeerCoroutine());
    }

    IEnumerator DeerCoroutine()
    {
        yield return new WaitForSeconds(1f);
        transform.DOLocalMove(new Vector3(0.6f,0f,10f), 15f);
        transform.DOLocalRotate(transform.localRotation.eulerAngles + Vector3.up * 90f, 15f);
    }
}
