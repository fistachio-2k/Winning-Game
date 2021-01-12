using UnityEngine;
using UnityEngine.Events;

public class GameEventsManager : MonoBehaviour
{
    [SerializeField] private UnityEvent ravealCorridor;
    public static GameEventsManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        ravealCorridor.Invoke();
    }
}
