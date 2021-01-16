using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventsManager : MonoBehaviour
{
    [SerializeField] private UnityEvent revealCorridor;
    [SerializeField] private GameObject hazeret;
    [SerializeField] private Renderer pianoRenderrer;
    [SerializeField] private float maxDistanceForCorridorTrigger = 7f;
    private int _hazertHash;
    public static GameEventsManager _instance;
    private Camera _camera;
    private HashSet<int> _collectedItems;
    private bool corridorRevealed = false;
    //private Dictionary<UnityEvent, bool> eventsActivisionDict;
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

        _camera = Camera.main;
        _collectedItems = new HashSet<int>();
        _hazertHash = hazeret.GetHashCode();

    }

    void Update()
    {
        if (!corridorRevealed && _collectedItems.Contains(_hazertHash) && pianoRenderrer.isVisible &&
            Vector3.Distance(_camera.transform.position, pianoRenderrer.transform.position) < maxDistanceForCorridorTrigger)
        {
            corridorRevealed = true;
            revealCorridor.Invoke();
        }

    }

    public void AddCollectedItem(int itemHashCode)
    {
        _collectedItems.Add(itemHashCode);
    }



}
