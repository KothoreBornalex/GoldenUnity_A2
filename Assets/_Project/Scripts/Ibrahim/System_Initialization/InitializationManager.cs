using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InitializationManager : MonoBehaviour
{
    [SerializeField] private bool _debug;
    void Start()
    {
        SettingsManager.InitializeAllSettings();

        if (!_debug)
        {
            LoadingManager.instance.LoadSceneWithoutLoadingScreen(GameAssets.instance.GameLevelsBank.MainMenu.SceneName);

            //SoundManager.Instance.PlaySound(GameAssets.instance.SoundBank._mainMenuMusic);
        }
        
    }

}
