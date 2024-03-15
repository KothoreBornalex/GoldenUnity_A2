using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;
using static UnityEngine.GraphicsBuffer;


public enum GuardTypes
{
    FixGuard,
    MovingGuard
}

public class Guard : Objective
{
    [Header("Guard Fields")]
    [SerializeField] private GuardTypes _guardType;
    private Animator _animator;
    private GuardGlobalClass _guardGlobalClass;

    private Transform _target;
    private SpriteRenderer[] _spriteRenderer;
    private Rigidbody2D _rb2D;
    private SpotPlayer _spotPlayer;

    [SerializeField, Range(0f,6f)]float _waitforSecond = 4.0f;
    bool _isDead;
    [SerializeField] UnityEvent OnDeathUnityEvent;

    public void Eliminate()
    {
        Complete();
        _guardGlobalClass.enabled = false;
        _isDead = true;
        OnDeathUnityEvent?.Invoke();
    }

    
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentsInChildren<SpriteRenderer>();

        _guardGlobalClass = GetComponent<GuardGlobalClass>();
        _spotPlayer = GetComponentInChildren<SpotPlayer>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FadeOut();

        if (_target == null) return;

        Vector2 direction = _target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        Quaternion targetRotation = Quaternion.identity;

        switch (_guardType)
        {
            case GuardTypes.FixGuard:
                targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                break;

            case GuardTypes.MovingGuard:
                targetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                break;
        }


        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3.0f * Time.deltaTime);
       
    }

    void FadeOut()
    {
        if (!_isDead) return;
        
        foreach(SpriteRenderer sprite in _spriteRenderer)
        {
            sprite.color = Vector4.Lerp(sprite.color, new Vector4(0, 0, 0, 0), Time.deltaTime);
        }

        _waitforSecond += Time.deltaTime;

        if (_waitforSecond > 4f)
        {
            Destroy(gameObject);
        }
    }


    public void SetTarget(Transform target)
    {
        _guardGlobalClass.enabled = false;
        _rb2D.velocity = new Vector2(0, 0);
        _target = target;
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


#if UNITY_EDITOR
[CustomEditor(typeof(Guard))]
public class GuardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Display the default inspector GUI
        DrawDefaultInspector();

        // Cast the target to MyClass
        Guard guard = (Guard)target;

        if (GUILayout.Button("Eliminate The Guard"))
        {
            guard.Eliminate();
        }
    }
}
#endif