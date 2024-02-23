using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Settings PlayerPrefs Keys")]
    private string _keyBloom = "bloom";
    private string _keyChromaticAberration = "chromaticAberration";
    private string _keyVignette = "vignette";
    private string _keyImageQuality = "imageQuality";
    private string _keymusicVolume = "musicVolume";
    private string _keyeffectVolume = "effectVolume";

    private void Start()
    {
        InitializeSettingsInUI();
        FollowAllParameters();
    }

    private void InitializeSettingsInUI()
    {
        if (PlayerPrefs.GetInt(_keyBloom) == 1) _bloomToggle.isOn = true;
        else _bloomToggle.isOn = false;

        if (PlayerPrefs.GetInt(_keyChromaticAberration) == 1) _chromaticAberrationToggle.isOn = true;
        else _chromaticAberrationToggle.isOn = false;

        if (PlayerPrefs.GetInt(_keyVignette) == 1) _vignetteToggle.isOn = true;
        else _vignetteToggle.isOn = false;


        _imageQualitySlider.value = PlayerPrefs.GetFloat(_keyImageQuality);
        _musicEffectSlider.value = PlayerPrefs.GetFloat(_keymusicVolume);
        _soundEffectSlider.value = PlayerPrefs.GetFloat(_keyeffectVolume);

    }

    private void FollowAllParameters()
    {



        _bloomToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(_bloomToggle, _keyBloom);
        });

        _chromaticAberrationToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(_chromaticAberrationToggle, _keyChromaticAberration);
        });

        _vignetteToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(_vignetteToggle, _keyVignette);
        });





        _imageQualitySlider.onValueChanged.AddListener(delegate {
            SliderValueChanged(_imageQualitySlider, _keyImageQuality);
        });

        _musicEffectSlider.onValueChanged.AddListener(delegate {
            SliderValueChanged(_musicEffectSlider, _keymusicVolume);
        });

        _soundEffectSlider.onValueChanged.AddListener(delegate {
            SliderValueChanged(_soundEffectSlider, _keyeffectVolume);
        });
    }

    void ToggleValueChanged(Toggle change, string parameterName)
    {
        if (change.isOn)
        {
            PlayerPrefs.SetInt(parameterName, 1);
        }
        else
        {
            PlayerPrefs.SetInt(parameterName, 0);
        }
    }

    void SliderValueChanged(Slider change, string parameterName)
    {
        PlayerPrefs.SetFloat(parameterName, change.value);
    }

    public void SetBloomSettings()
    {
    }
}
