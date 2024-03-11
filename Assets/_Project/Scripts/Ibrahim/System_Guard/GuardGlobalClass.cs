using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGlobalClass : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        if (_animator) _animator = GetComponentInChildren<Animator>();
    }

    #region Animations Functions

    public void Anim_StartWalk()
    {
        if (_animator) _animator.SetBool("isWalking", true);
    }

    public void Anim_StopWalk()
    {
        if (_animator) _animator.SetBool("isWalking", false);
    }


    public void Anim_TriggerSpotting()
    {
        if (_animator) _animator.SetTrigger("isSpotting");
    }

    public void Anim_ToggleIsDead()
    {
        if (_animator) _animator.SetBool("isDead", true);
    }

    #endregion
}
