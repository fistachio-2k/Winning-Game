using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Chair : MonoBehaviour
{
    [SerializeField] private Transform positionToMove;
    [SerializeField] private float duration = 2f;

    void Update()
    {
        if (InputManager.Instance.GetTestButton1())
        {
            StartCoroutine(Sitting());
        }
    }

    IEnumerator Sitting()
    {
        transform.DOMove(positionToMove.position, duration);
        yield return new WaitForSeconds(duration);
        GameEventsManager._instance.SwitchToVcam(GameEventsManager.Vcam.Sitting);
    }
}
