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
    [SerializeField] private UnityEvent revealCorridor3;

    // ============ Trigger objects ============ //
    [SerializeField] private GameObject hazeret;
    [SerializeField] private GameObject recipe;
    [SerializeField] private GameObject recipeBig;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject gezer;
    [SerializeField] private GameObject egg1;
    [SerializeField] private GameObject egg2;
    [SerializeField] private GameObject fish;
    // ======================================== //
    [SerializeField] private GameObject mira;
    [SerializeField] private GameObject radio1;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Chair chair;

    [SerializeField] private Canvas cameraCenter;
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] private GameObject settings3D;
    [SerializeField] private TextReveal corridor2Text;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SubtitleManager subtitleManager;
    [SerializeField] private Light menuLight;
    [SerializeField] private Animator fallingAnim;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private CinemachineVirtualCamera[] vcams;


    public static GameEventsManager _instance;
    private InputManager _inputManager;
    public Vcam currVcam;
    private Vcam _lastVcam = Vcam.Menu;
    private UnityEvent _mouseClickEvent;
    private int _hazertHash;
    private int _recipeHash;
    private int _keyHash;
    private int _gezerHash;
    private int _fishHash;
    private int _egg1Hash;
    private int _egg2Hash;
    private Camera _camera;
    private HashSet<int> _collectedItems;
    private bool corridorRevealed1 = false;
    private bool corridorRevealed2 = false;
    private bool corridorRevealed3 = false;
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
        Corridor2,
        Corridor3,
        Settings,
        Murder,
        Falling
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
        _gezerHash = gezer.GetHashCode();
        _fishHash = fish.GetHashCode();
        _egg1Hash = egg1.GetHashCode();
        _egg2Hash = egg2.GetHashCode();
        currVcam = Vcam.Menu;
        settings3D.SetActive(false);
        cameraCenter.enabled = false;
        endCanvas.enabled = false;
    }

    void Update()
    {
        // Esc clicked
        if (_inputManager.GetEscButton())
        {
            StartCoroutine(ToggleSettings());
            audioManager.Play("click");
        }
        // Sitting scenario
        else if (_instance.currVcam == Vcam.Sitting)
        {
            if (_inputManager.GetMouseClick() && !inSettings)
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
        if (!corridorRevealed2 && _collectedItems.Contains(_gezerHash) && _collectedItems.Contains(_fishHash) 
            && _collectedItems.Contains(_egg1Hash) && _collectedItems.Contains(_egg2Hash))
        {
            StartCoroutine(reavelCorridor2());
        }

        // Corridor3 Reavel Logic
        if (!corridorRevealed3 && _collectedItems.Contains(_keyHash))
        {
            StartCoroutine(reavelCorridor3());
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
        DisableMovement();
        corridorRevealed1 = true;
        yield return new WaitForSeconds(1f);
        SwitchToVcam(Vcam.Corridor);
        yield return new WaitForSeconds(2f);
        revealCorridor.Invoke();
        yield return new WaitForSeconds(1f);
        audioManager.Play("Drag");
        yield return new WaitForSeconds(3f);
        SwitchToVcam(Vcam.Player);
        EnableMovement();
    }

    IEnumerator reavelCorridor2()
    {
        DisableMovement();
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
        EnableMovement();
    }

    IEnumerator reavelCorridor3()
    {
        DisableMovement();
        corridorRevealed3 = true;
        yield return new WaitForSeconds(1f);
        SwitchToVcam(Vcam.Corridor3);
        revealCorridor3.Invoke();
        yield return new WaitForSeconds(1f);
        audioManager.Play("Drag");
        yield return new WaitForSeconds(5f);
        SwitchToVcam(Vcam.Player);
        EnableMovement();
    }

    public void SwitchToVcam(GameEventsManager.Vcam vcam)
    {
        _lastVcam = currVcam;
        vcams[(int) vcam].Priority = 1;
        vcams[(int) currVcam].Priority = 0;
        currVcam = vcam;
    }

    public UnityEvent GetMouseClickEvent()
    {
        return _mouseClickEvent;
    }

    public IEnumerator StartToGame()
    {
        if(!radio1.GetComponent<AudioSource>().isPlaying)
        {
            radio1.GetComponent<AudioSource>().Play();
        }
        audioManager.Play("click");
        menuLight.enabled = false;
        mainMenu.transform.DOLocalJump(mainMenu.transform.position - Vector3.up * 0.25f, 0.25f ,1 ,1.5f);
        yield return new WaitForSeconds(1.5f);
        _instance.GetMouseClickEvent().AddListener(chair.StandUpWrapper);
        StartCoroutine(chair.text.RevealText());
        yield return new WaitForSeconds(5f);
        SwitchToVcam(GameEventsManager.Vcam.Sitting);
        Cursor.visible = false;
        cameraCenter.enabled = true;
        inStart = false;
        Destroy(mainMenu, 2f);
       
    }

    public IEnumerator ToggleSettings()
    {
        inSettings = !inSettings;
        Image im = settingsCanvas.GetComponent<Image>();
        if (!inStart)
        {
            cameraCenter.enabled = !cameraCenter.enabled;
            Cursor.visible = !Cursor.visible;
            im.DOFade(1f, 1f);
            yield return new WaitForSeconds(1f);
        }

        if (currVcam == Vcam.Settings)
        {
            SwitchToVcam(_lastVcam);
        }
        else
        {
            SwitchToVcam(Vcam.Settings);
        }

        if (!inStart)
        {
            yield return new WaitForSeconds(1f);
            im.DOFade(0f, 1f);
        }
        yield return new WaitForSeconds(1f);
        settings3D.SetActive(!settings3D.activeSelf);
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

    public void PlayMamaEstherScene(GameObject mamaTrigger1)
    {
        if (!isRecipeCollected())
        {
            StartCoroutine(subtitleManager.startMamaEstherDialog());
            mamaTrigger1.GetComponent<AudioSource>().Play();
            mamaTrigger1.GetComponent<Collider>().enabled = false;
        }
    }

    public void PlayAfterResipy(GameObject mamaTrigger2)
    {
        if (isRecipeCollected())
        {
            subtitleManager.startAfterRecipyDialog();
            mamaTrigger2.GetComponent<AudioSource>().Play();
            recipeBig.SetActive(true);
            egg1.GetComponent<Collider>().enabled = true;
            egg2.GetComponent<Collider>().enabled = true;
            gezer.GetComponent<Collider>().enabled = true;
            fish.GetComponent<Collider>().enabled = true;
            mamaTrigger2.GetComponent<Collider>().enabled = false;
        }
    }

    public void PlayAnsweringMachine(GameObject AnsweringMachine)
    {
        subtitleManager.startAnsweringMachine();
        AnsweringMachine.GetComponent<AudioSource>().Play();
    }

    public IEnumerator StartLastScene()
    {
        Cursor.visible = false;
        Image im = settingsCanvas.GetComponent<Image>();
        im.DOFade(1f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        audioManager.Play("LastDialog");
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(subtitleManager.StartLastDialog());
        yield return new WaitForSeconds(22f);
        im.DOFade(0f, 0.2f);
        SwitchToVcam(Vcam.Murder);
        yield return new WaitForSeconds(1f);
        SwitchToVcam(Vcam.Falling);
        yield return new WaitForSeconds(1f);
        fallingAnim.enabled = true;
        audioManager.Play("endSound");
        yield return new WaitForSeconds(3f);
        im.DOFade(1f, 0.2f);
        Cursor.visible = false;
        endCanvas.enabled = true;
        cameraCenter.enabled = false;
        audioManager.Play("endMusic");
        
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
