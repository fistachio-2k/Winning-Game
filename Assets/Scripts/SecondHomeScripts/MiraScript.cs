using UnityEngine;

public class MiraScript : MonoBehaviour
{
    [SerializeField] private SubtitleManager subtitleManager;
    [SerializeField] private GameObject mira;

    private void OnTriggerEnter(Collider other)
    {
        subtitleManager.startMamaMiraDialog();
        mira.GetComponent<AudioSource>().Play();
    }
}
