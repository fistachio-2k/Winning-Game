using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(MeshCollider))]
public class Chair : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform positionToMove;
    [SerializeField] private float duration = 2f;
    [SerializeField] public TextReveal text;

    IEnumerator SitDown()
    {
        Vector3 basePos = transform.position;
        transform.DOMove(positionToMove.position, duration);
        yield return new WaitForSeconds(duration);
        GameEventsManager._instance.SwitchToVcam(GameEventsManager.Vcam.Sitting);
        yield return new WaitForSeconds(duration / 2);
        StartCoroutine(text.RevealText());
        transform.DOMove(basePos, duration);
        GetComponent<MeshCollider>().enabled = false;
    }

    IEnumerator StandUp()
    {
        //TODO Mira: add sound!
        GetComponent<MeshCollider>().enabled = true;
        Vector3 basePos = transform.position;
        transform.DOMove(positionToMove.position, duration);
        GameEventsManager._instance.SwitchToVcam(GameEventsManager.Vcam.Player);
        yield return new WaitForSeconds(duration / 2);
        transform.DOMove(basePos, duration);
    }

    public void StandUpWrapper()
    {
        GameEventsManager._instance.GetMouseClickEvent().RemoveListener(StandUpWrapper);
        StartCoroutine(StandUp());
    }
    public void Interact()
    {
        GameEventsManager._instance.GetMouseClickEvent().AddListener(StandUpWrapper);
        StartCoroutine(SitDown());
    }
}
