using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SoundManager
{
    public static Dictionary<string, float> soundTimerDictionary = new Dictionary<string, float>();
    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(sound.Clip);

            soundTimerDictionary[sound.Name] = Time.time;
        }
    }

    public static void PlaySoundAtPosition(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            AudioSource.PlayClipAtPoint(sound.Clip, position, 1.0f);

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