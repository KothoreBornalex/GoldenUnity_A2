using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static Dictionary<string, float> soundTimerDictionary = new Dictionary<string, float>();

    private static AudioSource _currentMusic;
    private static AudioSource _nextMusic;

    private static float _musicVolume;
    private static float _effectVolume;

    public static float MusicVolume { get => _musicVolume; set => _musicVolume = value; }
    public static float EffectVolume { get => _effectVolume; set => _effectVolume = value; }

    public static SoundManager Instance;

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

    public void PlaySound(Sound sound)
    {
        switch (sound.Type)
        {
            case SoundType.Music:
                StartCoroutine(PlayMusic(sound));
                break;

            case SoundType.Effect:
                if (CanPlaySound(sound))
                {
                    GameObject soundGameObject = new GameObject("Sound");
                    AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                    audioSource.volume = _effectVolume;

                    audioSource.PlayOneShot(sound.Clip);

                    soundTimerDictionary[sound.Name] = Time.time;
                }
                break;
        }
        
    }

    private static IEnumerator PlayMusic(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Music" + sound.Name);
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = sound.Clip;
        audioSource.loop = true;
        audioSource.Play();
        _nextMusic = audioSource;

        if (_currentMusic)
        {
            Debug.Log("I was here");
            _nextMusic.volume = 0;
            bool currentMusicDone = false;
            bool nextMusicDone = false;

            while (!currentMusicDone || !nextMusicDone)
            {
                
                _currentMusic.volume = Mathf.MoveTowards(_currentMusic.volume, 0, Time.deltaTime * 0.45f);
                _nextMusic.volume = Mathf.MoveTowards(_nextMusic.volume, _musicVolume, Time.deltaTime * 0.3f);
                
                if(_currentMusic.volume == 0 && _nextMusic.volume == _musicVolume)
                {
                    currentMusicDone = true;
                    nextMusicDone = true;
                    _currentMusic.gameObject.SetActive(false);
                    _currentMusic = _nextMusic;
                }

                yield return null;
            }

        }
        else
        {
            _currentMusic = _nextMusic;
            _currentMusic.volume = _musicVolume;
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