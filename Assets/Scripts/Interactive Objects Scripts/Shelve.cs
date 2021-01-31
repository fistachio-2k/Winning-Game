using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Shelve : MonoBehaviour
{
    [SerializeField] private Vector3 position;
    [SerializeField] private float rotationX;
    public void MoveShelve()
    {
        StartCoroutine(ShelveRoutine());
    }

    IEnumerator ShelveRoutine()
    {
        yield return new WaitForSeconds(1f);
        transform.DOLocalMove(position, 15f);
        transform.DOLocalRotate(transform.localRotation.eulerAngles + Vector3.up * rotationX, 15f);
    }
}
