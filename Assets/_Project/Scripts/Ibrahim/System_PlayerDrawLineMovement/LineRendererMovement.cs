using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererMovement : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private LineRenderer _linePath;
    [SerializeField] private LayerMask _floorLayerMask;
    [SerializeField] private LayerMask _obstaclesLayerMask;
    [SerializeField] private Rigidbody2D _rb;


    private Camera _camera;

    [Header("Path Fields")]
    [SerializeField, Range(0.1f, 5.0f)] private float _minLengthForValidPath;
    [SerializeField, Range(0.1f, 5.0f)] private float _resetPathSpeed;
    [SerializeField, ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    private Color _drawingPathColor;
    [SerializeField, ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    private Color _errorPathColor;
    [SerializeField, ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    private Color _acheivedPathColor;

    private bool _canDrawPath;
    private bool _isDrawing;
    public event Action<bool> ToggleClearPath;

    Coroutine drawing;



    [Header("Movement Fields")]
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _movingSpeed;
    private bool _isReadyToMove;
    private bool _isMoving;
    private int _targetedVertIndex;
    private Vector3 _normalizedDirection;
    public event Action<bool> ToggleReadyToMove;


    #region  Start & OnEnable & OnDisable
    private void Start()
    {
        if(!_camera) _camera = Camera.main;

        if(_linePath) InitializedPath();
    }

    private void OnEnable()
    {
        if (_playerManager != null) _playerManager.IsSelected += _playerManager_IsSelected;
        if (_playerManager != null) _playerManager.IsUnSelected += _playerManager_IsUnSelected;
    }

    private void OnDisable()
    {
        if (_playerManager != null) _playerManager.IsSelected -= _playerManager_IsSelected;
        if (_playerManager != null) _playerManager.IsUnSelected -= _playerManager_IsUnSelected;
    }

    #endregion

    private void _playerManager_IsSelected()
    {
        _canDrawPath = true;
    }
    private void _playerManager_IsUnSelected()
    {
        _canDrawPath = false;
    }


    private void FixedUpdate()
    {
        

        if(_isMoving)
        {
            if (_linePath.positionCount != 0)
            {
                UpdateMovements();
            }
        }
        else if(!_isReadyToMove)
        {
            if (_canDrawPath)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartLine();
                }

            }

            if (_isDrawing)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    FinishLine();
                }
            }
        }
    }


    #region Drawing Path Functions
    private void StartLine()
    {
        if(drawing != null)
        {
            StopCoroutine(drawing);
        }

        _isDrawing = true;
        SetLineColor(_drawingPathColor);
        drawing = StartCoroutine(DrawLine());
    }

    private void FinishLine()
    {
        StopCoroutine(drawing);
        _isDrawing = false;

        if (CheckPathLength())
        {
            if (CheckPathForCollisions())
            {
                PathInvalid();
            }
            else
            {
                NoCollisionDetected();
            }
        }
        else
        {
            PathInvalid();
        }

    }

    private bool CheckPathLength()
    {
        float distance = 0;

        for(int i = 0; i < _linePath.positionCount; i++)
        {
            if (i != _linePath.positionCount - 1)
            {
                Vector3 vector = _linePath.GetPosition(i) - _linePath.GetPosition(i + 1);
                distance += vector.magnitude;
            }

        }
        Debug.Log("Distance: " + distance);
        if (distance > _minLengthForValidPath) return true; 
        else return false;
    }

    private IEnumerator DrawLine()
    {

        while (true)
        {
            Vector3 position = _camera.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            if(_linePath.positionCount > 0)
            {
                if (CheckDistanceBetweenVertex(position, _linePath.GetPosition(_linePath.positionCount - 1)))
                {
                    _linePath.positionCount++;
                    _linePath.SetPosition(_linePath.positionCount - 1, position);
                }
            }
            else
            {
                _linePath.positionCount++;
                _linePath.SetPosition(_linePath.positionCount - 1, position);
            }
            
            
            yield return null;
        }
    }


    #endregion

    #region Check Path Functions
    private bool CheckDistanceBetweenVertex(Vector3 vert1, Vector3 vert2)
    {
        Vector3 direction = vert1 - vert2;

        if (direction.magnitude > 0.05f) return true;
        else return false;
    }
    private bool CheckPathForCollisions()
    {
        for(int i = 0; i < _linePath.positionCount; i++)
        {
            if (i == _linePath.positionCount - 1) continue;

            Vector3 currentPosition = _linePath.GetPosition(i);
            Vector3 targetPosition = _linePath.GetPosition(i + 1);
            Vector3 direction = targetPosition - currentPosition;
            float distance = direction.magnitude;
            direction = direction.normalized;

            RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, distance, _obstaclesLayerMask);
            if(hit.collider)
            {
                return true;
            }
        }
        return false;
    }


    private void PathInvalid()
    {
        Debug.Log(" Collision");

        SetLineColor(_errorPathColor);
        ClearPath();
    }
    private void NoCollisionDetected()
    {
        Debug.Log("No Collision");
        SetLineColor(_acheivedPathColor);
        _isReadyToMove = true;
        ToggleReadyToMove?.Invoke(_isReadyToMove);

        ToggleClearPath?.Invoke(true);
    }

    #endregion

    #region Utilities Functions
    private void InitializedPath()
    {
        _linePath.positionCount = 0;
    }
    public void ClearPath()
    {
        if(_isMoving)
        {
            _isMoving = false;
            InverseVertsOrder();
            StartCoroutine(DeletePath());
        }
        else
        {
            StartCoroutine(DeletePath());
        }

        _isReadyToMove = false;
        ToggleReadyToMove?.Invoke(_isReadyToMove);
        ToggleClearPath?.Invoke(false);
    }

    private void SetLineColor(Color color)
    {
        _linePath.material.SetColor("_BaseColor", color);
        _linePath.material.SetColor("_EmissionColor", color);
    }
    #endregion

    #region Movements Functions
    public void StartMoving()
    {
        _isReadyToMove = false;
        ToggleReadyToMove?.Invoke(_isReadyToMove);

        InverseVertsOrder();

        _isMoving = true;
        // Feedback.
    }

    private void UpdateMovements()
    {

        if (_linePath.positionCount == 0)
        {
            _isMoving = false;
            ToggleClearPath?.Invoke(false);
        }
            
        
        _targetedVertIndex = _linePath.positionCount - 1;

        Vector3 targetVert = _linePath.GetPosition(_targetedVertIndex);

        //Handling Check of the Target
        Vector3 direction = targetVert - transform.position;
        _normalizedDirection = direction.normalized;

        if (direction.magnitude < 0.05f && _linePath.positionCount > 1)
        {
            _linePath.positionCount--;
            UpdateMovements();
            // Go Next
            return;
        }else if(_linePath.positionCount <= 1)
        {
            _isMoving = false;
            ToggleClearPath?.Invoke(false);
            return;
        }



        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * _rotationSpeed);

        //Handling Position
        _rb.MovePosition(transform.position + _normalizedDirection * _movingSpeed * Time.deltaTime);
        //_rb.AddForce(_normalizedDirection * _movingSpeed, ForceMode2D.Force);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Ray ray = new Ray();
        ray.direction = _normalizedDirection;
        ray.origin = transform.position;
        Gizmos.DrawRay(ray);
    }
    private void InverseVertsOrder()
    {
        List<Vector3> verts = new List<Vector3>();
        for(int i = _linePath.positionCount - 1; i >= 0; i--)
        {
            verts.Add(_linePath.GetPosition(i));
        }

        _linePath.SetPositions(verts.ToArray());
    }

    private IEnumerator DeletePath()
    {
        int deletedVert = 0;
        float currentTimer = 0;
        float timing = 1.0f / (_resetPathSpeed * 10.0f);

        while (_linePath.positionCount > 0)
        {
            currentTimer += Time.deltaTime;

            if(deletedVert < 15)
            {
                _linePath.positionCount--;
                deletedVert++;
            }

            if (currentTimer >= timing)
            {
                deletedVert = 0;
                currentTimer = 0;
            }

            yield return null;
        }
    }
}
