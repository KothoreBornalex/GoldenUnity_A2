using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class grandMaMovement : MonoBehaviour
{
    enum Direction
    {
        up,
        down,
        left,
        right
    };

    private Camera _camera;
    private PlayerManager _playerManager;
    private Rigidbody2D _rb;
    private Animator _animator;

    [Header("Player Settings")]
    [SerializeField, Range(10, 1000)] float _speed;
    private float _multipliedSpeed;

    [SerializeField, Range(1, 40)] float _swipeThreshOld = 20f;
    [SerializeField] bool _isMoving = false;
    private Direction _dir;

    [Header("Wall Detection Settings")]
    [SerializeField] LayerMask _wallLayerMask;
    [SerializeField, Range(1, 15)] float _detectionLength;
    [SerializeField, Range(1, 8)] float _minPerimetre;

    private Vector2 _endSwiping;
    private Vector2 _startSwiping;
    private bool _canReceiveInput;
    private bool _detectSwipeOnlyAfterRelease = false;



    private float timer;
    [Header("Unity Events")]
    [SerializeField] private UnityEvent OnCollision;

    


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerManager = GetComponent<PlayerManager>();
        _animator = GetComponentInChildren<Animator>();

        _multipliedSpeed = _speed * _rb.mass;
    }
    private void Start()
    {
        _camera = Camera.main;
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


    private void _playerManager_IsSelected()
    {
        _canReceiveInput = true;
    }
    private void _playerManager_IsUnSelected()
    {
        _canReceiveInput = false;
    }



    private void Update()
    {
        if (_playerManager.IsPaused) return;
        if (!_canReceiveInput) return;
        else
        {
            timer += Time.deltaTime;
        }

        if (timer < 0.15f) return;

        if (!_isMoving)
        {

            foreach (Touch touch in Input.touches)
            {
                //Detects Touch Start on the screen
                if (touch.phase == TouchPhase.Began)
                {
                    //_camera.ScreenToWorldPoint(touch.position);
                    _startSwiping = _camera.ScreenToWorldPoint(touch.position);
                    _endSwiping = _camera.ScreenToWorldPoint(touch.position);
                }

                //Detects Swipe while finger is still moving
                if (touch.phase == TouchPhase.Moved)
                {
                    if (!_detectSwipeOnlyAfterRelease)
                    {
                        _endSwiping = _camera.ScreenToWorldPoint(touch.position);
                        CheckSwipe();
                    }
                }

                //Detects swipe after finger is released
                if (touch.phase == TouchPhase.Ended)
                {
                    _endSwiping = _camera.ScreenToWorldPoint(touch.position);
                    CheckSwipe();
                }
            }

        }
    }



    void CheckSwipe()
    {
        /*if (GetSwipePower() > _swipeThreshOld)
        {
            Debug.Log("Star Swipe: " + _startSwiping);
            Debug.Log("End Swipe: " + _endSwiping);
        }*/


        //check vertical swipe
        if (GetSwipePower() > _swipeThreshOld && verticalSwipe() > horizontalSwipe())
        {
            if (_endSwiping.y - _startSwiping.y > 0) //up swipe
            {
                _dir = Direction.up;
                if (!IsUnObstructed(_dir)) return;

                //move upward
                ApplyMovement(new Vector2(0, _multipliedSpeed * Time.fixedDeltaTime));
                _isMoving = true;
            }
            else if (_endSwiping.y - _startSwiping.y < 0) //down swipe
            {
                _dir = Direction.down;
                if (!IsUnObstructed(_dir)) return;

                //move downward
                ApplyMovement(new Vector2(0, -_multipliedSpeed * Time.fixedDeltaTime));
                _isMoving = true;
            }
        }
        //Check horizontal swipe
        else if (GetSwipePower() > _swipeThreshOld && horizontalSwipe() > verticalSwipe())
        {
            if (_endSwiping.x - _startSwiping.x > 0)//Right swipe
            {
                _dir = Direction.right;
                if (!IsUnObstructed(_dir)) return;

                //move right
                ApplyMovement(new Vector2(_multipliedSpeed * Time.fixedDeltaTime, 0));
                _isMoving = true;
                
            }
            else if (_endSwiping.x - _startSwiping.x < 0)//Left swipe
            {
                _dir = Direction.left;
                if (!IsUnObstructed(_dir)) return;

                //move left
                ApplyMovement(new Vector2(-_multipliedSpeed * Time.fixedDeltaTime, 0));
                _isMoving = true;
                
            }
        }
    }


    void ApplyMovement(Vector2 movement)
    {
        _rb.AddForce(movement, ForceMode2D.Impulse);
    }


    float GetSwipePower()
    {
        Vector2 dir = _startSwiping - _endSwiping;
        return dir.magnitude;
    }

    bool IsUnObstructed(Direction dir)
    {
        RaycastHit2D hit;
        switch (dir)
        {
            case Direction.left:
                hit = Physics2D.Raycast(transform.position, new Vector2(-1, 0), _detectionLength, _wallLayerMask);
                if (CheckHitDistance(hit)) return true;
                else return false;

            case Direction.right:
                hit = Physics2D.Raycast(transform.position, new Vector2(1, 0), _detectionLength, _wallLayerMask);
                if (CheckHitDistance(hit)) return true;
                else return false;

            case Direction.up:
                hit = Physics2D.Raycast(transform.position, new Vector2(0, 1), _detectionLength, _wallLayerMask);
                if (CheckHitDistance(hit)) return true;
                else return false;

            case Direction.down:
                hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), _detectionLength, _wallLayerMask);
                if (CheckHitDistance(hit)) return true;
                else return false;
        }


        return false;

    }

    bool CheckHitDistance(RaycastHit2D hit)
    {
        Debug.DrawLine(transform.position, hit.point, Color.blue);
        Vector2 dir = hit.point - new Vector2(transform.position.x, transform.position.y);
        if (hit.collider != null)
        {
            if (dir.magnitude < _minPerimetre) return false;
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    float verticalSwipe()
    {
        return Mathf.Abs(_endSwiping.y - _startSwiping.y);
    }
    float horizontalSwipe()
    {
        return Mathf.Abs(_endSwiping.x - _startSwiping.x);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision?.Invoke();
        _rb.velocity = Vector3.zero;
        _isMoving = false;
    }

}
