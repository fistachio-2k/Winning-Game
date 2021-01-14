using UnityEngine;
using DG.Tweening;

public class Corridor : MonoBehaviour
{
    [SerializeField]
    private GameObject[] toDisapear;
    [SerializeField]
    private GameObject[] toBeApear;

    public void RevealCoridor()
    {
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
