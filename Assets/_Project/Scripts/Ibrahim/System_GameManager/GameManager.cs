using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [Header("Objectives")]
    [SerializeField] private List<Objective> _objectives = new List<Objective>();

    // Game Fields
    private bool _gameIsPaused;


    [Header("UI Elements")]
    [SerializeField] private Canvas _endGameCanvas;
    [SerializeField] private Canvas _pauseButtonCanvas;

    [SerializeField] private GameObject _panelVictory;
    [SerializeField] private GameObject _panelDefeat;


    [Header("Events")]
    [SerializeField] private UnityEvent OnGameEndUnityEvent;
    [SerializeField] private UnityEvent OnVictoryUnityEvent;
    [SerializeField] private UnityEvent OnDefeatUnityEvent;
    public event Action OnGameEnd;


    public List<Objective> Objectives { get => _objectives; set => _objectives = value; }

    public event Action OnPaused;
    public event Action OnUnPaused;

    [SerializeField] private UnityEvent OnPausedUnityEvent;
    [SerializeField] private UnityEvent OnUnPausedUnityEvent;


    // Start is called before the first frame update
    void Start()
    {
        _endGameCanvas.worldCamera = Camera.main;
        _pauseButtonCanvas.worldCamera= Camera.main;
        AddPauseMenu();
    }

    private void AddPauseMenu()
    {
        Debug.Log("Add Pause Menu To Scene");
        LoadingManager.instance.LoadSceneWithoutLoadingScreenWithoutNotify(GameAssets.instance.GameLevelsBank.PauseMenu.SceneName);
    }



    public void EndGame(bool victory)
    {
        _endGameCanvas.gameObject.SetActive(true);
        OnGameEnd?.Invoke();
        OnGameEndUnityEvent?.Invoke();

        if (victory)
        {
            _panelVictory.SetActive(true);
            OnVictoryUnityEvent?.Invoke();
        }
        else
        {
            _panelDefeat.SetActive(true);
            OnDefeatUnityEvent?.Invoke();
        }

        StartCoroutine(LoadConvas());
    }

    private IEnumerator LoadConvas()
    {
        CanvasGroup canvasGroup = _endGameCanvas.GetComponent<CanvasGroup>();
        while(canvasGroup.alpha < 1)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, Time.deltaTime);
            yield return null;
        }
    }

    public void ActivatePause()
    {
        MenuManager.Instance.Canvas.enabled = !MenuManager.Instance.Canvas.enabled;

        OnPaused?.Invoke();
        OnPausedUnityEvent?.Invoke();

        _gameIsPaused = true;

    }

    public void DesactivatePause()
    {
        MenuManager.Instance.Canvas.enabled = !MenuManager.Instance.Canvas.enabled;
        OnUnPaused?.Invoke();
        OnUnPausedUnityEvent?.Invoke();

        _gameIsPaused = false;
       
    }

    public void Retry()
    {
        LoadingManager.instance.LoadSceneWithLoadingScreen(LoadingManager.instance.CurrentScene, true, LoadingManager.instance.CurrentScene);
    }

    public void GoBackToMenu()
    {
        LoadingManager.instance.LoadSceneWithLoadingScreen(GameAssets.instance.GameLevelsBank.MainMenu.SceneName, true, LoadingManager.instance.CurrentScene);
    }

    public bool AreAllObjectivesCompleted()
    {
        for(int i = 0; i < _objectives.Count; i++)
        {
            if (!_objectives[i].IsCompleted)
            {
                return false;
            }
        }

        return true;
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Display the default inspector GUI
        DrawDefaultInspector();

        // Cast the target to MyClass
        GameManager gameManager = (GameManager)target;

        if (GUILayout.Button("Launch Defeat"))
        {
            gameManager.EndGame(false);
        }

        if (GUILayout.Button("Launch Victory"))
        {
            gameManager.EndGame(true);
        }
    }
}
#endif