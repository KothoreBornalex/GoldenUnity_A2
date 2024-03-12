using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EntityTrigger : MonoBehaviour
{

    [SerializeField] private UnityEvent OnActivationUnityEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerManager>(out PlayerManager playerManager))
        {
            if (playerManager.PlayerType == PlayerType.GrandMa && playerManager.HasItem)
            {
                OnActivationUnityEvent?.Invoke();
            }
        }
    }

}
