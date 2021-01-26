using UnityEngine;

public class MiraScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GameEventsManager._instance.PlayMamaMiraScene();
    }
}
