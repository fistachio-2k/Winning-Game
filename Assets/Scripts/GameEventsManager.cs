using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using System;
using UnityEngine.UI;

public class GameEventsManager : MonoBehaviour
{
    [SerializeField] private UnityEvent revealCorridor;
    [SerializeField] private GameObject hazeret;
    [SerializeField] private Renderer pianoRenderrer;
    [SerializeField] private CinemachineVirtualCamera[] vcams;
    [SerializeField] private GameObject[] HouseModels;
    [SerializeField] private Light menuLight;
    [SerializeField] private Canvas cameraCenter;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Canvas volumeSliderCanvas;

    public static GameEventsManager _instance;
    private InputManager _inputManager;
    public Vcam currVcam;
    private UnityEvent _mouseClickEvent;
    private int _hazertHash;
    private Camera _camera;
    private HashSet<int> _collectedItems;
    private bool corridorRevealed = false;
    private bool inSettings = false;

    // Vcam aligned with editor cameras order
    public enum Vcam
    {
        Player,
        Sitting,
        Menu,
        Corridor
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
    }

    private void Start()
    {
        _inputManager = InputManager.Instance;
        _camera = Camera.main;
        _mouseClickEvent = new UnityEvent();
        _collectedItems = new HashSet<int>();
        _hazertHash = hazeret.GetHashCode();
        currVcam = Vcam.Menu;
        cameraCenter.enabled = false;
        volumeSliderCanvas.enabled = false;

    }

    void Update()
    {
        // Esc clicked
        if (_inputManager.GetEscButton())
        {
            // TODO: Fix second escape bug
            if(inSettings)
            {
                MenuToSettings();
            }
            _instance.GameToMenu();
        }
        // Sitting scenario
        else if (_instance.currVcam == Vcam.Sitting)
        {
            if (_inputManager.GetMouseClick())
            {
                _instance.GetMouseClickEvent().Invoke();
            }
        }
        
        // Corridor Reavel Logic
        if (!corridorRevealed && _collectedItems.Contains(_hazertHash))
        {
            StartCoroutine(reavelCorridor1());
        }

    }

    public void AddCollectedItem(int itemHashCode)
    {
        _collectedItems.Add(itemHashCode);
    }

    IEnumerator reavelCorridor1()
    {
        corridorRevealed = true;
        yield return new WaitForSeconds(1f);
        SwitchToVcam(Vcam.Corridor);
        yield return new WaitForSeconds(2f);
        revealCorridor.Invoke();
        yield return new WaitForSeconds(3f);
        SwitchToVcam(Vcam.Player);
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
        if (inSettings)
        {
            return;
        }
        if(!Array.Find(audioManager.sounds, sound => sound.name == "MainMusic").source.isPlaying || !Array.Find(audioManager.sounds, sound => sound.name == "MainMusic1").source.isPlaying)
        {
            audioManager.Play("MainMusic");
        }
        audioManager.Play("click");
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

    public void MenuToSettings()
    {
        audioManager.Play("click");
        volumeSliderCanvas.enabled = !volumeSliderCanvas.enabled;
        inSettings = !inSettings;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
