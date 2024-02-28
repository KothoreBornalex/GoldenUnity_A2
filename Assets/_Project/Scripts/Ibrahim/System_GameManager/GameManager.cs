using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private UnityEvent _onGameEnd;
    [SerializeField] private UnityEvent _onVictory;
    [SerializeField] private UnityEvent _onDefeat;

    public List<Objective> Objectives { get => _objectives; set => _objectives = value; }


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
        LoadingManager.instance.LoadSceneWithoutLoadingScreen(GameAssets.instance.GameLevelsBank.PauseMenu.SceneName);
    }



    public void EndGame(bool victory)
    {
        _endGameCanvas.gameObject.SetActive(true);
        _onGameEnd?.Invoke();

        if (victory)
        {
            _panelVictory.SetActive(true);
            _onVictory?.Invoke();
        }
        else
        {
            _panelDefeat.SetActive(true);
            _onDefeat?.Invoke();
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

    public void TogglePause()
    {
        if (_gameIsPaused)
        {
            MenuManager.Instance.ToggleMenu();
            _gameIsPaused = false;
        }
        else
        {
            MenuManager.Instance.ToggleMenu();
            _gameIsPaused = true;
        }

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