using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Settings
{
    ImageQuality,
    MusicVolume,
    EffectVolume,
    Bloom,
    ChromaticAberration,
    Vignette
}


public static class SettingsManager
{
    public static void InitializeAllSettings()
    {
        SetRenderScale(GetFloat(Settings.ImageQuality));
        SetMusicVolume(GetFloat(Settings.MusicVolume));
        SetEffectVolume(GetFloat(Settings.EffectVolume));
        SetBloom(GetToggle(Settings.Bloom));
        SetChromaticAberration(GetToggle(Settings.ChromaticAberration));
        SetVignette(GetToggle(Settings.Vignette));
    }

    #region Set & Get Settings
    public static void SetToggle(Toggle change, Settings setting)
    {
        if (change.isOn)
        {
            PlayerPrefs.SetInt(setting.ToString(), 1);
        }
        else
        {
            PlayerPrefs.SetInt(setting.ToString(), 0);
        }

        switch (setting)
        {
            case Settings.Bloom:
                SetBloom(change.isOn);
                break;
            case Settings.ChromaticAberration:
                SetChromaticAberration(change.isOn);
                break;
            case Settings.Vignette:
                SetVignette(change.isOn);
                break;
        }
    }

    public static void SetFloat(Slider change, Settings setting)
    {
        PlayerPrefs.SetFloat(setting.ToString(), change.value);

        switch(setting)
        {
            case Settings.ImageQuality:
                SetRenderScale(change.value);
                break;
            case Settings.MusicVolume:
                SetMusicVolume(change.value);
                break;
            case Settings.EffectVolume:
                SetEffectVolume(change.value);
                break;
        }
    }


    public static bool GetToggle(Settings setting)
    {
        if (PlayerPrefs.GetInt(setting.ToString()) == 1) return true;
        else return false;
    }

    public static float GetFloat(Settings setting)
    {
        return PlayerPrefs.GetFloat(setting.ToString());
    }
    #endregion


    #region Apply Settings
    private static void SetRenderScale(float value)
    {
        GameAssets.instance.RenderPipelineAsset.renderScale = value;
    }

    private static void SetMusicVolume(float value)
    {
        SoundManager.MusicVolume = value;
    }

    private static void SetEffectVolume(float value)
    {
        SoundManager.EffectVolume = value;
    }

    private static void SetChromaticAberration(bool value)
    {
        PostProcessManager.SetChromaticAberration(value);
    }

    private static void SetVignette(bool value)
    {
        PostProcessManager.SetVignette(value);
    }

    private static void SetBloom(bool value)
    {
        PostProcessManager.SetBloom(value);
    }
    #endregion
}
