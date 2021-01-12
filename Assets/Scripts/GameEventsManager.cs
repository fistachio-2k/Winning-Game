using UnityEngine;
using UnityEngine.Events;

public class GameEventsManager : MonoBehaviour
{
    [SerializeField] private UnityEvent ravealCorridor;
    // Start is called before the first frame update


    // Update is called once per frame
    void Start()
    {
        ravealCorridor.Invoke();
    }
}
