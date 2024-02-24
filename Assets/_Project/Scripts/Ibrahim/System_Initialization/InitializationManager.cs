using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationManager : MonoBehaviour
{

    void Start()
    {
        SettingsManager.InitializeAllSettings();
        LevelManager.instance.LoadSceneWithoutLoadingScreen(0);
    }

}
