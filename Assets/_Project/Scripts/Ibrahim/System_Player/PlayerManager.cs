using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    private Camera _camera;

    [Header("Selection Fields")]
    [SerializeField, Range(0.1f, 2.0f)] private float _selectionCircleRadius = 1.0f;
    [SerializeField] private bool _debugSelectionCircleRadius;

    private bool _isSelected;

    public event Action IsSelected;
    public event Action IsUnSelected;
    [SerializeField] private UnityEvent IsSelectedUnityEvent;
    [SerializeField] private UnityEvent IsUnSelectedUnityEvent;


    private Vector3 startInputPos;
    private Vector3 endInputPos;
    float timeSinceSelected;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckClickIsInRadius())
            {
                startInputPos = _camera.ScreenToWorldPoint(Input.mousePosition);

                if (_isSelected) return;
                SetSelected();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (CheckClickIsInRadius())
            {
                endInputPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 vector = startInputPos - endInputPos;

                if(vector.magnitude < 0.05f && timeSinceSelected > 0.3f)
                {
                    SetUnSelected();
                    startInputPos = Vector3.zero;
                    endInputPos = Vector3.zero;
                }
            }
        }

        if (_isSelected) timeSinceSelected += Time.deltaTime;
    }

    /*private void ToggleSelection()
    {

        if (CheckClickIsInRadius())
        {
            SetSelected();
        }
    }*/

    private bool CheckClickIsInRadius()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 playerPos = transform.position;
        playerPos.z = 0;

        if (Mathf.Sqrt(Vector3.Distance(mousePos, playerPos)) * Mathf.Sqrt(Vector3.Distance(mousePos, playerPos)) < _selectionCircleRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetSelected()
    {
        IsSelected?.Invoke();
        IsSelectedUnityEvent?.Invoke();

        _isSelected = true;

    }

    private void SetUnSelected()
    {

        IsUnSelected?.Invoke();
        IsUnSelectedUnityEvent?.Invoke();

        _isSelected = false;
        timeSinceSelected = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(_debugSelectionCircleRadius) Gizmos.DrawSphere(transform.position, _selectionCircleRadius);

    }
}
