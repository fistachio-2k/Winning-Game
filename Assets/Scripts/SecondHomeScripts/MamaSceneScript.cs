using UnityEngine;

public class MamaSceneScript : MonoBehaviour
{
    private bool firstPlay = true;
    private bool secondPlay = true;

    private void OnTriggerEnter(Collider other)
    {
        if(firstPlay || secondPlay)
        {
            GameEventsManager._instance.PlayMamaEstherScene();
            if(!firstPlay && secondPlay)
            {
                secondPlay = false;
            }
            firstPlay = false;
        }
        
        
    }
}
