using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EntityTrigger : MonoBehaviour
{
    [Header("Entity Trigger Fields")]
    [SerializeField] private bool _isActivated;
    [Header("Unity Events")]
    [SerializeField] private UnityEvent OnToggleActivated;
    [SerializeField] private UnityEvent OnToggleDesactivated;

    private void Start()
    {
        if (_isActivated)
        {
            OnToggleActivated?.Invoke();
        }
    }

    public void ActivateWithoutNotify()
    {
        _isActivated = true;
    }

    public void DesactivateWithoutNotify()
    {
        _isActivated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent<PlayerManager>(out PlayerManager playerManager))
        {
            if (playerManager.PlayerType == PlayerType.GrandMa && playerManager.HasItem)
            {
                if (_isActivated)
                {
                    _isActivated = false;
                    OnToggleDesactivated?.Invoke();
                }
                else
                {
                    _isActivated = true;
                    OnToggleActivated?.Invoke();
                }
            }
        }
    }

    public void PlayCamerasound()
    {
        SoundManager.Instance.PlaySound(GameAssets.instance.SoundBank._cameraOnOff);
    }
    public void PlayDoorSound()
    {
        SoundManager.Instance.PlaySound(GameAssets.instance.SoundBank._doorOpenClose);
    }

}
