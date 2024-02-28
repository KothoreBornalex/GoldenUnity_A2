using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEditor;
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

public class LoadingManager : MonoBehaviour
{
    
    public static LoadingManager instance;

    [Header("Loading Screen Fields")]
    private string _currentScene;

    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private Image _loadingIllustration;

    [SerializeField] private Slider _loadingBarr;
    [SerializeField] private Animator _loadingAnimator;

    private float target;

    #region Getters & Setters
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
        }

    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void LoadSceneWithoutLoadingScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        _currentScene = sceneName;
    }

    public void LoadSceneWithoutLoadingScreenWithoutNotify(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private string GetSceneName(int sceneID)
    {
        foreach (GameLevel sceneData in GameAssets.instance.GameLevelsBank.GameLevels)
        {
            if(sceneData.LevelID == sceneID) return sceneData.SceneName;
        }

        return null;
    }

    public async void LoadSceneWithLoadingScreen(string sceneName, bool unloadCurrentMap, string mapToUnload = "")
    {   
        target = 0;
        _loadingBarr.value = 0;
        _loadingCanvas.SetActive(true);

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
        Debug.Log("Backgroudn set active");
        _loadingIllustration.enabled = true;


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

        _loadingIllustration.enabled = false;
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
