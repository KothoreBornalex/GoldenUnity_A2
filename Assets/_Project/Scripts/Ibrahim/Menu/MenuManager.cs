using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _levelSelectionPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _creditsPanel;

    [Header("Settings Input")]
    [SerializeField] private Slider _imageQualitySlider;
    [SerializeField] private Slider _musicEffectSlider;
    [SerializeField] private Slider _soundEffectSlider;
    [SerializeField] private Toggle _chromaticAberrationToggle;
    [SerializeField] private Toggle _vignetteToggle;
    [SerializeField] private Toggle _bloomToggle;


    private void Start()
    {
        InitializeSettingsInUI();
        FollowAllParameters();

        GetComponent<Canvas>().worldCamera = Camera.main;

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
    public void PlayButtonSound()
    {
        SoundManager.PlaySound(GameAssets.instance.SoundBank._buttonFocus);
    }

    public void StartLevel(int value)
    {
        LevelManager.instance.LoadSceneWithLoadingScreen(value , true, LevelManager.instance.CurrentScene);
    }

    public void LeaveTheGame()
    {
        Application.Quit();
    }

    #endregion
}
