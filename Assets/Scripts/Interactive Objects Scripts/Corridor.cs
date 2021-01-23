using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Corridor : MonoBehaviour
{
    [SerializeField]
    private GameObject[] toDisapear;
    [SerializeField]
    private GameObject[] toBeApear;
    [SerializeField]
    private GameObject backWall;
    [SerializeField]
    private Light corridorLight;

    public void RevealCoridor()
    {
        StartCoroutine(CorridorCoRoutine());
    }

    IEnumerator CorridorCoRoutine()
    {
        yield return new WaitForSeconds(1f);
        foreach (GameObject obj in toDisapear)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in toBeApear)
        {
            obj.SetActive(true);
        }
        transform.DOScaleZ(1f, 15f);
        yield return new WaitForSeconds(15f);
        corridorLight.enabled = true;
        backWall.SetActive(false);
    }
}
