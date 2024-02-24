using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


[System.Serializable]
struct SceneData
{
    public int id;
    public string name;
}

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager instance;

    [Header("Loading Screen Fields")]
    [SerializeField] private List<SceneData> _scenes;
    private string _currentScene;

    [SerializeField] private string _mainMenuScene;
    [SerializeField] private string _levelOneScene;

    [Header("Loading Screen Fields")]
    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private GameObject _loadingBackGround;
    [SerializeField] private Slider _loadingBarr;
    [SerializeField] private Animator _loadingAnimator;

    private float target;

    #region Getters & Setters
    public string MainMenuScene { get => _mainMenuScene;}
    public string LevelOneScene { get => _levelOneScene;}
    public string CurrentScene { get => _currentScene;}

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void LoadSceneWithoutLoadingScreen(int value)
    {
        string sceneName = GetSceneName(value);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        _currentScene = sceneName;
    }

    private string GetSceneName(int sceneID)
    {
        foreach(SceneData sceneData in _scenes)
        {
            if(sceneData.id == sceneID) return sceneData.name;
        }

        return null;
    }

    public async void LoadSceneWithLoadingScreen(int sceneID, bool unloadCurrentMap, string mapToUnload = "")
    {   
        target = 0;
        _loadingBarr.value = 0;
        _loadingCanvas.SetActive(true);

        string sceneName = GetSceneName(sceneID);

        //AudioManager.instance.SetMusic(MusicsEnum.LoadingScreenMusic);





        //Make the fade In appear and wait before it is done.
        _loadingAnimator.Play("FadeIn");
        await Task.Delay(2500);

        if (unloadCurrentMap)
        {
            UnloadScene(mapToUnload);
        }

        //Desactivating the main camera during the loading screen.
        //CameraManager.instance._camera.enabled = false;


        //Switching to the loading screen scene.
        //UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScreen");

        //Activating the background.
        _loadingBackGround.SetActive(true);

        //Activating the loading barr.
        _loadingBarr.gameObject.SetActive(true);

        //Make the fade Out appear and wait before it is done.
        _loadingAnimator.Play("FadeOut");
        await Task.Delay(2500);

        //Loading the targeted scene.
        var scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene.allowSceneActivation = false;

        do
        {
            await Task.Delay(100);
            target = scene.progress;
        } while (scene.progress < 0.9f);
        await Task.Delay(2500);


        _currentScene = sceneName;

        //Make the fade In appear and wait before it is done.
        _loadingAnimator.Play("FadeIn");
        await Task.Delay(2500);

        //Activating the main camera during the loading screen.
        //CameraManager.instance._camera.enabled = true;


        //Activating the scene Switch.
        scene.allowSceneActivation = true;
        _loadingBarr.gameObject.SetActive(false);

        _loadingBackGround.SetActive(false);

        //AudioManager.instance.SetMusic(MusicsEnum.BaseMusic);

        //Make the fade Out appear and wait before it is done.
        _loadingAnimator.Play("FadeOut");

        await Task.Delay(2500);
        _loadingCanvas.SetActive(false);

    }

    void Update()
    {
        _loadingBarr.value = Mathf.MoveTowards(_loadingBarr.value, target, Time.deltaTime);
    }



}
