using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandPaMovement : MonoBehaviour
{
    [SerializeField] GameObject _grandPa;
    [SerializeField] LayerMask _obstacleMask;
    [SerializeField] float _speed;
    [SerializeField] float _swipeThreshOld = 20f;

    private Vector2 _swipeDown;
    private Vector2 _swipeUp;

    private bool _canMove;
    [SerializeField] bool _isAgainstTheWall = false;
    private bool _detectSwipeOnlyAfterRelease = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_canMove)
        {
            foreach (Touch touch in Input.touches)
            {
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
                //move upward
                while (_isAgainstTheWall)
                {
                    _grandPa.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _speed * Time.fixedDeltaTime);
                }
            }
            else if (_swipeDown.y - _swipeUp.y < 0) //down swipe
            {
                //move downward
                while (_isAgainstTheWall)
                {
                    _grandPa.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_speed * Time.fixedDeltaTime);
                }
            }
            Debug.Log(_isAgainstTheWall);
        }

        //Check horizontal swipe
        else if (verticalSwipe() > _swipeThreshOld && horizontalSwipe() > verticalSwipe())
        {
            if (_swipeDown.x - _swipeUp.x > 0 && _isAgainstTheWall)//Right swipe
            {
                //move rigth
                _grandPa.GetComponent<Rigidbody2D>().velocity = new Vector2(_speed * Time.fixedDeltaTime, 0);
            }
            else if (_swipeDown.x - _swipeUp.x < 0 && _isAgainstTheWall)//Left swipe
            {
                //move left
                _grandPa.GetComponent<Rigidbody2D>().velocity = new Vector2(-_speed * Time.fixedDeltaTime, 0);
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision = _obstacleMask)
        {
            _isAgainstTheWall = true;
        }
    }
}
