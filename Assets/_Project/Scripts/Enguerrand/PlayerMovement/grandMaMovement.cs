using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandMaMovement : MonoBehaviour
{
    [SerializeField] GameObject _grandMa;
    [SerializeField] float _speed;
    [SerializeField] float _swipeThreshOld = 20f;

    private Vector2 _swipeDown;
    private Vector2 _swipeUp;

    [SerializeField] bool _isMoving = false;
    [SerializeField] bool _isAgainstTheWall = false;
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

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (!_isMoving)
        {
            foreach (Touch touch in Input.touches)
            {
                if(touch.deltaPosition.magnitude > 0)
                {

                }

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
                    //_grandMa.GetComponent<Rigidbody2D>().velocity = _swipe * _speed * Time.fixedDeltaTime;
                    _grandMa.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _speed * Time.fixedDeltaTime);
                    _isMoving = true;
                }
                
            }
            else if (_swipeDown.y - _swipeUp.y < 0 && _dir != Direction.down) //down swipe
            {
                _dir = Direction.down;

                //move downward
                if (!_isMoving && !_DownWallDetection)
                {
                    _grandMa.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_speed * Time.fixedDeltaTime);
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
                    _grandMa.GetComponent<Rigidbody2D>().velocity = new Vector2(_speed * Time.fixedDeltaTime, 0);
                    _isMoving = true;
                }
            }
            else if (_swipeDown.x - _swipeUp.x < 0 && _dir != Direction.left)//Left swipe
            {
                _dir = Direction.left;

                //move left
                if (!_isMoving && !_LeftWallDetection)
                {
                    _grandMa.GetComponent<Rigidbody2D>().velocity = new Vector2(-_speed * Time.fixedDeltaTime, 0);
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
        //_grandMa.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        _grandMa.GetComponent<Rigidbody2D>().velocity -= _grandMa.GetComponent<Rigidbody2D>().velocity * .25f;
        _isAgainstTheWall = true;
        _isMoving = false;
    }

}
