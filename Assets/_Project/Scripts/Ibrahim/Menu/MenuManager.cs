using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum PanelsConfiguration
{
    LevelsSelection = 0,
    Settings = 1,
    Credits = 2
}


public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Canvas Fields")]
    [SerializeField] private Canvas _canvas;

    [Header("Menu Panels")]
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _levelSelectionPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private Transform _levelSelectionContentTransform;

    [Header("Settings Input")]
    [SerializeField] private Slider _imageQualitySlider;
    [SerializeField] private Slider _musicEffectSlider;
    [SerializeField] private Slider _soundEffectSlider;
    [SerializeField] private Toggle _chromaticAberrationToggle;
    [SerializeField] private Toggle _vignetteToggle;
    [SerializeField] private Toggle _bloomToggle;

    [Header("Menu Prefabs")]
    [SerializeField] private GameObject _selectionLevelPrefab;

    public Canvas Canvas { get => _canvas; set => _canvas = value; }

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


    private void Start()
    {
        InitializeSettingsInUI();
        FollowAllParameters();

        GetComponent<Canvas>().worldCamera = Camera.main;
        InitializeLevelsSelection();

        //LevelManager.instance.LoadSceneWithLoadingScreen(LevelManager.instance.LevelOneScene, true, LevelManager.instance.MainMenuScene);
    }

    private void InitializeSettingsInUI()
    {
        _bloomToggle.isOn = SettingsManager.GetToggle(Settings.Bloom);
        _chromaticAberrationToggle.isOn = SettingsManager.GetToggle(Settings.ChromaticAberration);
        _vignetteToggle.isOn = SettingsManager.GetToggle(Settings.Vignette);


        _imageQualitySlider.value = SettingsManager.GetFloat(Settings.ImageQuality);
        _musicEffectSlider.value = SettingsManager.GetFloat(Settings.MusicVolume);
        _soundEffectSlider.value = SettingsManager.GetFloat(Settings.EffectVolume);
    }

    private void FollowAllParameters()
    {
        _bloomToggle.onValueChanged.AddListener(delegate {
            SettingsManager.SetToggle(_bloomToggle, Settings.Bloom);
        });

        _chromaticAberrationToggle.onValueChanged.AddListener(delegate {
            SettingsManager.SetToggle(_chromaticAberrationToggle, Settings.ChromaticAberration);
        });

        _vignetteToggle.onValueChanged.AddListener(delegate {
            SettingsManager.SetToggle(_vignetteToggle, Settings.Vignette);
        });





        _imageQualitySlider.onValueChanged.AddListener(delegate {
            SettingsManager.SetFloat(_imageQualitySlider, Settings.ImageQuality);
        });

        _musicEffectSlider.onValueChanged.AddListener(delegate {
            SettingsManager.SetFloat(_musicEffectSlider, Settings.MusicVolume);
        });

        _soundEffectSlider.onValueChanged.AddListener(delegate {
            SettingsManager.SetFloat(_soundEffectSlider, Settings.EffectVolume);
        });
    }


    public void ActivatePanel(int value)
    {
        PanelsConfiguration config = (PanelsConfiguration)value;

        switch (config)
        {
            case PanelsConfiguration.LevelsSelection:
                _levelSelectionPanel.SetActive(true);
                _settingsPanel.SetActive(false);
                _creditsPanel.SetActive(false);
                break;
            case PanelsConfiguration.Settings:
                _settingsPanel.SetActive(true);
                _levelSelectionPanel.SetActive(false);
                _creditsPanel.SetActive(false);
                break;
            case PanelsConfiguration.Credits:
                _creditsPanel.SetActive(true);
                _levelSelectionPanel.SetActive(false);
                _settingsPanel.SetActive(false);
                break;
        }

        PlayButtonSound();
    }



    #region Utilities Functions
    public void ToggleMenu()
    {
        GameManager.Instance.DesactivatePause();
    }

    public void PlayButtonSound()
    {
        SoundManager.Instance.PlaySound(GameAssets.instance.SoundBank._buttonFocus);
    }

    public void StartLevel(string sceneName)
    {
        LoadingManager.instance.LoadSceneWithLoadingScreen(sceneName, true, LoadingManager.instance.CurrentScene);
        SoundManager.Instance.PlaySound(GameAssets.instance.SoundBank._levelOneMusic);
    }

    public void InitializeLevelsSelection()
    {
        if(_levelSelectionPanel != null)
        {
            foreach(GameLevel gameLevel in GameAssets.instance.GameLevelsBank.GameLevels)
            {
                GameObject newSelection = Instantiate(_selectionLevelPrefab, _levelSelectionContentTransform);
                LevelSelectionUI levelSelectionUI = newSelection.GetComponent<LevelSelectionUI>();

                levelSelectionUI.Title.SetText(gameLevel.SceneName);
                levelSelectionUI.Image.sprite = gameLevel.Illustration;

                levelSelectionUI.PlayButton.onClick.AddListener(new UnityEngine.Events.UnityAction(() => 
                {
                    StartLevel(gameLevel.SceneName);
                }));

            }
        }
    }

    public void LeaveTheGame()
    {
        Application.Quit();
    }

    #endregion
}
