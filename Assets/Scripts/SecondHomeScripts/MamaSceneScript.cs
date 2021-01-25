using UnityEngine;

public class MamaSceneScript : MonoBehaviour
{
    [SerializeField] private SubtitleManager subtitleManager;
    [SerializeField] private GameObject mama;

    private void OnTriggerEnter(Collider other)
    {
        if (!GameEventsManager._instance.isRecipeCollected())
        {
            subtitleManager.startMamaEstherDialog();
            mama.GetComponent<AudioSource>().Play();
        }
        else
        {
            subtitleManager.startAfterRecipyDialog();
            //audioManager.Play("AfterRecipyDialog");
        }
    }
}
