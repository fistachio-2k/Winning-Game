using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class GameEventsManager : MonoBehaviour
{
    [SerializeField] private UnityEvent revealCorridor;
    [SerializeField] private GameObject hazeret;
    [SerializeField] private Renderer pianoRenderrer;
    [SerializeField] private float maxDistanceForCorridorTrigger = 7f;
    [SerializeField] private CinemachineVirtualCamera[] vcams;
    [SerializeField] private GameObject[] HouseModels;
    [SerializeField] private Light menuLight;
    [SerializeField] private MeshRenderer cameraCenter;

    public static GameEventsManager _instance;
    public Vcam currVcam;
    private UnityEvent _mouseClickEvent;
    private int _hazertHash;
    private Camera _camera;
    private HashSet<int> _collectedItems;
    private bool corridorRevealed = false;

    public enum Vcam
    {
        Player,
        Sitting,
        Menu,
    }

    public enum Scene
    {
        Opening,
        Breakfast,
        Lunch,
        DinnerS
    }

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
        currVcam = Vcam.Menu;
        _mouseClickEvent = new UnityEvent();
        _collectedItems = new HashSet<int>();
        _hazertHash = hazeret.GetHashCode();
        cameraCenter.enabled = false;

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

    public void SwitchToVcam(GameEventsManager.Vcam vcam)
    {
        vcams[(int) vcam].Priority = 1;
        vcams[(int) currVcam].Priority = 0;
        currVcam = vcam;
    }

    public UnityEvent GetMouseClickEvent()
    {
        return _mouseClickEvent;
    }

    public void MenuToGame()
    {
        SwitchToVcam(GameEventsManager.Vcam.Player);
        Cursor.visible = false;
        menuLight.enabled = false;
        cameraCenter.enabled = true;
    }

    public void GameToMenu()
    {
        SwitchToVcam(GameEventsManager.Vcam.Menu);
        Cursor.visible = true;
        menuLight.enabled = true;
        cameraCenter.enabled = false;
    }
}
