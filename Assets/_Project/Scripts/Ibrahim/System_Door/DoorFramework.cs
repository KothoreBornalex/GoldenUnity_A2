using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFramework : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void ToggleDoor()
    {
        _animator.SetTrigger("Toggle");
    }
}
