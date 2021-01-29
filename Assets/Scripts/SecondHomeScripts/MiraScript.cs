using UnityEngine;

public class MiraScript : MonoBehaviour
{
    private bool firstPlay = true;

    private void OnTriggerEnter(Collider other)
    {
        if (firstPlay)
        {
            GameEventsManager._instance.PlayMamaMiraScene();
            firstPlay = false;
        }
    }
}
