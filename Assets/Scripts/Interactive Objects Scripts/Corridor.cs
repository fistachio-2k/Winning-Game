using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Corridor : MonoBehaviour
{
    [SerializeField]
    private GameObject[] toDisapear;
    [SerializeField]
    private GameObject[] toBeApear;

    public void RevealCoridor()
    {
        StartCoroutine(CorridorCoRoutine());
    }

    IEnumerator CorridorCoRoutine()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Reavel the MAGIC CORRIDOR !");
        foreach (GameObject obj in toDisapear)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in toBeApear)
        {
            obj.SetActive(true);
        }
        transform.DOScaleZ(2f, 30f);
    }
}
