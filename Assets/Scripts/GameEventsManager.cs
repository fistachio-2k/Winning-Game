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
    [SerializeField] private UnityEvent revealCorridor2;
    [SerializeField] private GameObject hazeret;
    [SerializeField] private GameObject recipe;
    [SerializeField] private GameObject mama;
    [SerializeField] private GameObject mira;
    [SerializeField] private GameObject _spatula;
    [SerializeField] private GameObject[] HouseModels;

    [SerializeField] private Renderer pianoRenderrer;
    [SerializeField] private CinemachineVirtualCamera[] vcams;

    [SerializeField] private Light menuLight;
    [SerializeField] private Canvas cameraCenter;
    [SerializeField] private Canvas volumeSliderCanvas;
    [SerializeField] private TextReveal corridor2Text;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SubtitleManager subtitleManager;
    

    public static GameEventsManager _instance;
    private InputManager _inputManager;
    public Vcam currVcam;
    private UnityEvent _mouseClickEvent;
    private int _hazertHash;
    private int _recipeHash;
    private Camera _camera;
    private HashSet<int> _collectedItems;
    private bool corridorRevealed1 = false;
    private bool corridorRevealed2 = false;
    private bool inSettings = false;
    public bool isFrying = false;

    // Vcam aligned with editor cameras order
    public enum Vcam
    {
        Player,
        Sitting,
        Menu,
        Corridor,
        Corridor2
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
        _recipeHash = recipe.GetHashCode();
        currVcam = Vcam.Menu;
        cameraCenter.enabled = false;
        volumeSliderCanvas.enabled = false;
    }

    void Update()
    {
        // Esc clicked
        if (_inputManager.GetEscButton())
        {
            // TODO: Change to a specific menu according to location.
            if (currVcam !=  Vcam.Menu)
            {
                GameToMenu();
            }
            else if (inSettings)
            {
                ToggleMenuSettings();
            }
            else
            {
                MenuToGame();
            }

        }
        // Sitting scenario
        else if (_instance.currVcam == Vcam.Sitting)
        {
            if (_inputManager.GetMouseClick())
            {
                _instance.GetMouseClickEvent().Invoke();
            }
        }
        
        // Corridor1 Reavel Logic
        if (!corridorRevealed1 && _collectedItems.Contains(_hazertHash))
        {
            StartCoroutine(reavelCorridor1());
        }

        // Corridor2 Reavel Logic
        if (!corridorRevealed2 && isFrying)
        {
            StartCoroutine(reavelCorridor2());
        }

    }

    public void AddCollectedItem(int itemHashCode)
    {
        _collectedItems.Add(itemHashCode);
    }

    public bool isRecipeCollected()
    {
        return _collectedItems.Contains(_recipeHash);
    }

    IEnumerator reavelCorridor1()
    {
        corridorRevealed1 = true;
        yield return new WaitForSeconds(1f);
        SwitchToVcam(Vcam.Corridor);
        yield return new WaitForSeconds(2f);
        revealCorridor.Invoke();
        audioManager.Play("Drag");
        yield return new WaitForSeconds(3f);
        SwitchToVcam(Vcam.Player);
    }

    IEnumerator reavelCorridor2()
    {

        corridorRevealed2 = true;
        yield return new WaitForSeconds(1f);
        SwitchToVcam(Vcam.Corridor2);
        yield return new WaitForSeconds(2f);
        revealCorridor2.Invoke();
        audioManager.Play("Drag");
        StartCoroutine(corridor2Text.RevealText());
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
        if(!Array.Find(audioManager.sounds, sound => sound.name == "MainMusic").source.isPlaying && !Array.Find(audioManager.sounds, sound => sound.name == "MainMusic1").source.isPlaying)
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

    public void ToggleMenuSettings()
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

    public void PlayMamaMiraScene()
    {
        subtitleManager.startMamaMiraDialog();
        mira.GetComponent<AudioSource>().Play();
    }

    public void PlayMamaEstherScene()
    {
        if (!isRecipeCollected())
        {
            subtitleManager.startMamaEstherDialog();
            mama.GetComponent<AudioSource>().Play();
        }
        else
        {
            subtitleManager.startAfterRecipyDialog();
            audioManager.Play("AfterRecipe");
            _spatula.GetComponent<Spatula>().timeToFry = true;
        }
    }

    public void PlayAnsweringMachine()
    {
        subtitleManager.startAnsweringMachine();
        audioManager.Play("AnsweringMachine");
    }
}
