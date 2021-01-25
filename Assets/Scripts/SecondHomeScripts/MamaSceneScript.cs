using UnityEngine;

public class MamaSceneScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameEventsManager._instance.PlayMamaEstherScene();
    }
}
