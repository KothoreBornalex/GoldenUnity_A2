using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandMaMovement : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;
    [SerializeField] float _swipeThreshOld = 20f;

    private Vector2 _swipeDown;
    private Vector2 _swipeUp;

    [SerializeField] bool _isMoving = false;
    [SerializeField] bool _isAgainstTheWall = false;
    private bool _canReceiveInput;

    private bool _detectSwipeOnlyAfterRelease = false;
    private bool _LeftWallDetection = false;
    private bool _RigthWallDetection = false;
    private bool _UpWallDetection = false;
    private bool _DownWallDetection = false;

    enum Direction
    {
        up,
        down,
        left,
        right
    };
    Direction _dir;

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




    void FixedUpdate()
    {
        if (_playerManager.IsPaused) return;
        if (!_canReceiveInput) return;


        if (!_isMoving)
        {
            foreach (Touch touch in Input.touches)
            {
                //Detects Touch Start on the screen
                if (touch.phase == TouchPhase.Began)
                {
                    _swipeUp = touch.position;
                    _swipeDown = touch.position;
                }

                //Detects Swipe while finger is still moving
                if (touch.phase == TouchPhase.Moved)
                {
                    if (!_detectSwipeOnlyAfterRelease)
                    {
                        _swipeDown = touch.position;
                        checkSwipe();
                    }
                }

                //Detects swipe after finger is released
                if (touch.phase == TouchPhase.Ended)
                {
                    _swipeDown = touch.position;
                    checkSwipe();
                }
            }

        }
    }

    void checkSwipe()
    {
        //check vertical swipe
        if (verticalSwipe() > _swipeThreshOld && verticalSwipe() > horizontalSwipe())
        {
            if (_swipeDown.y - _swipeUp.y > 0 && _dir != Direction.up) //up swipe
            {
                _dir = Direction.up;

                //move upward
                if (!_isMoving && !_UpWallDetection)
                {
                    _rb.velocity = new Vector2(0, _speed * Time.fixedDeltaTime);
                    _isMoving = true;
                }
                
            }
            else if (_swipeDown.y - _swipeUp.y < 0 && _dir != Direction.down) //down swipe
            {
                _dir = Direction.down;

                //move downward
                if (!_isMoving && !_DownWallDetection)
                {
                    _rb.velocity = new Vector2(0, -_speed * Time.fixedDeltaTime);
                    _isMoving = true;
                }
            }
        }
        //Check horizontal swipe
        else if (verticalSwipe() > _swipeThreshOld && horizontalSwipe() > verticalSwipe())
        {
            if (_swipeDown.x - _swipeUp.x > 0 && _dir != Direction.right)//Right swipe
            {
                _dir = Direction.right;

                //move rigth
                if (!_isMoving && !_RigthWallDetection)
                {
                    _rb.velocity = new Vector2(_speed * Time.fixedDeltaTime, 0);
                    _isMoving = true;
                }
            }
            else if (_swipeDown.x - _swipeUp.x < 0 && _dir != Direction.left)//Left swipe
            {
                _dir = Direction.left;

                //move left
                if (!_isMoving && !_LeftWallDetection)
                {
                    _rb.velocity = new Vector2(-_speed * Time.fixedDeltaTime, 0);
                    _isMoving = true;
                }
            }
            _swipeUp = _swipeDown;
        }
    }

    float verticalSwipe()
    {
        return Mathf.Abs(_swipeDown.y - _swipeUp.y);
    }
    float horizontalSwipe()
    {
        return Mathf.Abs(_swipeDown.x - _swipeUp.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rb.position -= _rb.velocity * .35f;
        _rb.velocity = Vector3.zero;
        _isAgainstTheWall = true;
        _isMoving = false;
    }

}
