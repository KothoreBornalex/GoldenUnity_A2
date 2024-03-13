using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    [SerializeField] private SoundBank _soundBank;
    [SerializeField] private GameLevelsBank _gameLevelsBank;
    [SerializeField] private AchievementsBank _achievementsBank;
    [SerializeField] private UniversalRenderPipelineAsset _renderPipelineAsset;
    [SerializeField] private VolumeProfile _volumeProfile;

    #region Getters & Setters
    public SoundBank SoundBank { get => _soundBank; }
    public UniversalRenderPipelineAsset RenderPipelineAsset { get => _renderPipelineAsset; set => _renderPipelineAsset = value; }
    public VolumeProfile VolumeProfile { get => _volumeProfile; set => _volumeProfile = value; }
    public GameLevelsBank GameLevelsBank { get => _gameLevelsBank; set => _gameLevelsBank = value; }
    public AchievementsBank AchievementsBank { get => _achievementsBank;}

    #endregion

    public void Initialized()
    {

    }
}
