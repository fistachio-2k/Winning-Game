using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Chair : MonoBehaviour
{
    [SerializeField] private Transform positionToMove;
    [SerializeField] private float duration = 2f;
    [SerializeField] private TextReveal text;
    private bool testFlag = false;
    void Update()
    {
        if (InputManager.Instance.GetTestButton1())
        {
            if (testFlag)
            {
                StartCoroutine(StandUp());
                testFlag = !testFlag;
            }
            else
            {
                StartCoroutine(Sitting());
                testFlag = !testFlag;
            }
        }
    }

    IEnumerator Sitting()
    {
        Vector3 basePos = transform.position;
        transform.DOMove(positionToMove.position, duration);
        yield return new WaitForSeconds(duration);
        GameEventsManager._instance.SwitchToVcam(GameEventsManager.Vcam.Sitting);
        yield return new WaitForSeconds(duration / 2);
        StartCoroutine(text.RevealText());
        transform.DOMove(basePos, duration);
    }

    IEnumerator StandUp()
    {
        Vector3 basePos = transform.position;
        transform.DOMove(positionToMove.position, duration);
        GameEventsManager._instance.SwitchToVcam(GameEventsManager.Vcam.Player);
        yield return new WaitForSeconds(duration / 2);
        transform.DOMove(basePos, duration);
    }
}
