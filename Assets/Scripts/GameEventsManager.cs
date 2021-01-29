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
    private int _keyHash;
    private Camera _camera;
    private HashSet<int> _collectedItems;
    private bool corridorRevealed1 = false;
    private bool corridorRevealed2 = false;
    private bool backToFirstScene = false;
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
        _keyHash = key.GetHashCode();
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
        yield return new WaitForSeconds(1f);
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
        player.transform.DOMove((Vector3.right * -1.39f) + (Vector3.up * 2.69f) + (Vector3.forward * -0.8f), 3f);
        yield return new WaitForSeconds(2f);
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
        if(!radio1.GetComponent<AudioSource>().isPlaying)
        {
            radio1.GetComponent<AudioSource>().Play();
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
        if (inSettings)
        {
            MenuToSettings();
        }
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
            spatula.GetComponent<Spatula>().timeToFry = true;
        }
    }

    public void PlayAnsweringMachine(GameObject AnsweringMachine)
    {
        subtitleManager.startAnsweringMachine();
        AnsweringMachine.GetComponent<AudioSource>().Play();
    }
}
