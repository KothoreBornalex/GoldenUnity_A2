using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackComponent : MonoBehaviour
{
    
    private CircleCollider2D _collider;
    [SerializeField] private LayerMask _enemyLayerMask;
    private PlayerManager _playerManager;
    private GameObject _enemy;
    private bool _enemyDetected;

    public event Action OnAttacking;
    [SerializeField] private UnityEvent OnAttackingUnityEvent;
    [SerializeField] private UnityEvent OnFinishedAttackUnityEvent;

    public bool EnemyDetected { get => _enemyDetected;}

    // Start is called before the first frame update
    void Start()
    {
        _playerManager = GetComponentInParent<PlayerManager>();
        _collider = GetComponent<CircleCollider2D>();

        _collider.includeLayers = _enemyLayerMask;
        _collider.excludeLayers = LayerMask.NameToLayer("Everything");
        _collider.excludeLayers -= _enemyLayerMask;
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_playerManager.HasItem)
        {
            _enemyDetected = true;
            _enemy = collision.gameObject;

            Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _enemyDetected = false;
    }



    public void Attack()
    {
        SoundManager.Instance.PlaySound(GameAssets.instance.SoundBank._grandpaHit);
        OnAttackingUnityEvent?.Invoke();

        _enemy.GetComponent<Guard>().Eliminate();

        OnFinishedAttackUnityEvent?.Invoke();
    }
}
