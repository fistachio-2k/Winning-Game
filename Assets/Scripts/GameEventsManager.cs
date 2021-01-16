using UnityEngine;
using UnityEngine.Events;

public class GameEventsManager : MonoBehaviour
{
    [SerializeField] private UnityEvent ravealCorridor;
    public static GameEventsManager _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        ravealCorridor.Invoke();
    }
}
