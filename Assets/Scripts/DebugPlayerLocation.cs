using UnityEngine;

public class DebugPlayerLocation : MonoBehaviour
{

    [SerializeField] private int playerLocation = 0;
    [SerializeField] private bool changeLocation = false;
    [SerializeField] private Transform[] locations;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCamera;


    // Update is called once per frame
    void Update()
    {
        //Debug options for change player location
        if (changeLocation && GameEventsManager._instance.currVcam == GameEventsManager.Vcam.Player)
        {
            changeLocation = false;
            if (locations.Length >= playerLocation)
            {
                Debug.Log("Changing location!");
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = locations[playerLocation].position;
                player.GetComponent<CharacterController>().enabled = true;
            }
        }
    }
}
