using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;

    public static GameAssets Instance
    {
        get
        {
            if(_instance == null) _instance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();

            return _instance;
        }
    }


    [SerializeField] private SoundBank _soundBank;
    [SerializeField] private UniversalRenderPipelineAsset _renderPipelineAsset;
    [SerializeField] private VolumeProfile _volumeProfile;

    #region Getters & Setters
    public SoundBank SoundBank { get => _soundBank; }
    public UniversalRenderPipelineAsset RenderPipelineAsset { get => _renderPipelineAsset; set => _renderPipelineAsset = value; }
    public VolumeProfile VolumeProfile { get => _volumeProfile; set => _volumeProfile = value; }

    #endregion

}
