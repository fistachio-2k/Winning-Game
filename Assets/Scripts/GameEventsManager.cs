using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class GameEventsManager : MonoBehaviour
{
    [SerializeField] private UnityEvent revealCorridor;
    [SerializeField] private UnityEvent revealCorridor2;
    [SerializeField] private UnityEvent restoreCorridor;

    // ============ Trigger objects ============ //
    [SerializeField] private GameObject hazeret;
    [SerializeField] private GameObject recipe;
    [SerializeField] private GameObject key;
    // ======================================== //
    [SerializeField] private GameObject mama;
    [SerializeField] private GameObject mira;
    [SerializeField] private GameObject spatula;
    [SerializeField] private GameObject radio1;
    [SerializeField] private GameObject[] HouseModels;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private GameObject openBasement;
    [SerializeField] private GameObject wallsOpenBasement;
    [SerializeField] private Renderer pianoRenderrer;

    [SerializeField] private Light menuLight;
    [SerializeField] private Canvas cameraCenter;
    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] private TextReveal corridor2Text;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SubtitleManager subtitleManager;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private CinemachineVirtualCamera[] vcams;


    public static GameEventsManager _instance;
    private InputManager _inputManager;
    public Vcam currVcam;
    private UnityEvent _mouseClickEvent;
    private int _hazertHash;
    private int _recipeHash;
    private int _keyHash;
    private Camera _camera;
    private HashSet<int> _collectedItems;
    private bool corridorRevealed1 = false;
    private bool corridorRevealed2 = false;
    private bool backToFirstScene = false;
    private bool inSettings = false;
    private bool inStart = true;
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
        _keyHash = key.GetHashCode();
        currVcam = Vcam.Menu;
        cameraCenter.enabled = false;
        settingsCanvas.enabled = false;
    }

    void Update()
    {
        // Esc clicked
        if (_inputManager.GetEscButton())
        {
            if (currVcam !=  Vcam.Menu)
            {
                ToggleGameSettings();
                audioManager.Play("click");
            }
            else if (inSettings)
            {
                ToggleStartSettings();
                audioManager.Play("click");
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

        // Back to first scene
        if (!backToFirstScene &&_collectedItems.Contains(_keyHash))
        {
            StartCoroutine(BackToEndScene());
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
        yield return new WaitForSeconds(1f);
        audioManager.Play("Drag");
        yield return new WaitForSeconds(3f);
        SwitchToVcam(Vcam.Player);
    }

    IEnumerator reavelCorridor2()
    {
        corridorRevealed2 = true;
        yield return new WaitForSeconds(2f);
        SwitchToVcam(Vcam.Corridor2);
        yield return new WaitForSeconds(2f);
        revealCorridor2.Invoke();
        StartCoroutine(corridor2Text.RevealText());
        yield return new WaitForSeconds(1f);
        audioManager.Play("Drag");
        yield return new WaitForSeconds(3f);
        SwitchToVcam(Vcam.Player);
    }

    IEnumerator BackToEndScene()
    {
        backToFirstScene = true;
        yield return new WaitForSeconds(1f);
        player.transform.DOMove((Vector3.right * -0.416f) + (Vector3.up * 1.9f) + (Vector3.forward * 3.363f), 3f);
        yield return new WaitForSeconds(2f);

        //return first scene basment
        openBasement.SetActive(true);
        wallsOpenBasement.SetActive(true);
        restoreCorridor.Invoke();
        mainMenu.SetActive(false);
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

    public bool GetMouseClick()
    {
        return _inputManager.GetMouseClick();
    }

    public void StartToGame()
    {
        if(!radio1.GetComponent<AudioSource>().isPlaying)
        {
            radio1.GetComponent<AudioSource>().Play();
        }
        audioManager.Play("click");
        SwitchToVcam(GameEventsManager.Vcam.Player);
        Cursor.visible = false;
        menuLight.enabled = false;
        cameraCenter.enabled = true;
        inStart = false;
    }

    public void ToggleGameSettings()
    {
        if (inStart)
        {
            StartToGame();
            settingsCanvas.enabled = false;
            return;
        }
        Cursor.visible = !Cursor.visible;
        cameraCenter.enabled = !cameraCenter.enabled;
        inSettings = !inSettings;
        settingsCanvas.enabled = !settingsCanvas.enabled;
    }

    public void ToggleStartSettings()
    {
        audioManager.Play("click");
        settingsCanvas.enabled = !settingsCanvas.enabled;
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

    public void PlayMamaEstherScene(GameObject mamaTrigger)
    {
        if (!isRecipeCollected())
        {
            StartCoroutine(subtitleManager.startMamaEstherDialog());
            mama.GetComponent<AudioSource>().Play();
        }
        else
        {
            subtitleManager.startAfterRecipyDialog();
            mamaTrigger.GetComponent<AudioSource>().Play();
            spatula.GetComponent<Spatula>().timeToFry = true;
        }
    }

    public void PlayAnsweringMachine(GameObject AnsweringMachine)
    {
        subtitleManager.startAnsweringMachine();
        AnsweringMachine.GetComponent<AudioSource>().Play();
    }

    public void EnableMovement()
    {
        playerController.movementAllowed = true;
    }

    public void DisableMovement()
    {
        playerController.movementAllowed = false;
    }
}
