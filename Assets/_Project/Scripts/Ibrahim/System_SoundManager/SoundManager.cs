using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SoundManager
{
    public static Dictionary<string, float> soundTimerDictionary = new Dictionary<string, float>();


    private static float _musicVolume;
    private static float _effectVolume;

    public static float MusicVolume { get => _musicVolume; set => _musicVolume = value; }
    public static float EffectVolume { get => _effectVolume; set => _effectVolume = value; }



    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

            switch (sound.Type)
            {
                case SoundType.Music:
                    audioSource.volume = _musicVolume;
                    break;
                case SoundType.Effect:
                    audioSource.volume = _effectVolume;
                    break;
            }

            audioSource.PlayOneShot(sound.Clip);

            soundTimerDictionary[sound.Name] = Time.time;
        }
    }

    public static void PlaySoundAtPosition(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            switch (sound.Type)
            {
                case SoundType.Music:
                    AudioSource.PlayClipAtPoint(sound.Clip, position, _musicVolume);
                    break;
                case SoundType.Effect:
                    AudioSource.PlayClipAtPoint(sound.Clip, position, _effectVolume);
                    break;
            }

            soundTimerDictionary[sound.Name] = Time.time;
        }
    }


    private static bool CanPlaySound(Sound sound)
    {
        if (sound.Spammable) return true;
        else
        {
            if (soundTimerDictionary.ContainsKey(sound.Name))
            {
                float lastTimeSoundPlayed = soundTimerDictionary[sound.Name];
                if (lastTimeSoundPlayed + sound.Clip.length < Time.time)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}