using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLoot : MonoBehaviour
{
    [SerializeField] private PlayerType _playerType;
    [SerializeField] private UnityEvent OnGettingItemUnityEvent;
    private LayerMask _playerLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        _playerLayerMask = LayerMask.NameToLayer("Player");
    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.TryGetComponent<PlayerManager>(out PlayerManager manager))
        {
            if(manager.PlayerType == _playerType)
            {
                manager.GetItem();
                OnGettingItemUnityEvent?.Invoke();
            }
            
        }
    }
}
