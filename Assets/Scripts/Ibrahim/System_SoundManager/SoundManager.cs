using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SoundManager
{
    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(sound.Clip);
    }

    public static void PlaySoundAtPosition(Sound sound, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(sound.Clip, position, 1.0f);
    }
}