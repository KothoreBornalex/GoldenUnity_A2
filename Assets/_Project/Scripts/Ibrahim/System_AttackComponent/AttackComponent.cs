using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private LayerMask _enemyLayerMask;
    public event Action OnAttacking;
    [SerializeField] private UnityEvent OnAttackingUnityEvent;
    [SerializeField] private UnityEvent OnFinishedAttackUnityEvent;

    // Start is called before the first frame update
    void Start()
    {
        _collider.includeLayers = _enemyLayerMask;
        _collider.excludeLayers = LayerMask.NameToLayer("Everything");
        _collider.excludeLayers -= _enemyLayerMask;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Contact");
    }

    public void Attack()
    {
        OnAttackingUnityEvent?.Invoke();




        OnFinishedAttackUnityEvent?.Invoke();
    }
}
