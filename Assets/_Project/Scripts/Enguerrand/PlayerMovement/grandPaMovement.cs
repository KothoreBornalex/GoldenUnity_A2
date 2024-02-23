using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandPaMovement : MonoBehaviour
{
    [SerializeField] GameObject _grandMa;
    [SerializeField] float _speed;
    [SerializeField] float _swipeThreshOld = 20f;

    private Vector2 _swipeDown;
    private Vector2 _swipeUp;

    [SerializeField] bool _canMove = true;
    [SerializeField] bool _isAgainstTheWall = false;
    private bool _detectSwipeOnlyAfterRelease = false;

    enum Direction
    {
        up,
        down,
        left,
        right
    };
    Direction _dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_canMove)
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

            if (_swipeDown.y - _swipeUp.y > 0) //up swipe
            {
                _dir = Direction.up;

                //move upward
                if (!_isAgainstTheWall)
                {
                    //_grandMa.GetComponent<Rigidbody2D>().velocity = _swipe * _speed * Time.fixedDeltaTime;
                    _grandMa.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _speed * Time.fixedDeltaTime);
                    _canMove = false;
                }
                
            }
            else if (_swipeDown.y - _swipeUp.y < 0) //down swipe
            {
                _dir = Direction.down;

                //move downward
                if (!_isAgainstTheWall)
                {
                    _grandMa.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_speed * Time.fixedDeltaTime);
                    _canMove = false;
                }
            }
        }

        //Check horizontal swipe
        else if (verticalSwipe() > _swipeThreshOld && horizontalSwipe() > verticalSwipe())
        {
            if (_swipeDown.x - _swipeUp.x > 0)//Right swipe
            {
                _dir = Direction.right;

                //move rigth
                if (!_isAgainstTheWall)
                {
                    _grandMa.GetComponent<Rigidbody2D>().velocity = new Vector2(_speed * Time.fixedDeltaTime, 0);
                }
            }
            else if (_swipeDown.x - _swipeUp.x < 0)//Left swipe
            {
                _dir = Direction.left;

                //move left
                if (!_isAgainstTheWall)
                {
                    _grandMa.GetComponent<Rigidbody2D>().velocity = new Vector2(-_speed * Time.fixedDeltaTime, 0);
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
        _isAgainstTheWall = true;
        _canMove = true;
    }

}
