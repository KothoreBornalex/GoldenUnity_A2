using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pathfindingMovement : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] LineRenderer _line;
    [SerializeField] float _minDistance = .1f;
    [SerializeField] float _speed = 1f;
    [SerializeField, Range(.1f,5)] float _width;
    [SerializeField] int _currentIndex;

    private Vector3 _nextPos;
    private Vector2 _startPos;
    private Vector2 _dir;

    //public bool _dirChosen;
    public bool _canMove;

    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 1;
        _nextPos = transform.position;
        _line.startWidth = _line.endWidth = _width;
    }

    private void Update()
    {
        #region touchLinerenderer
        /*if (Input.touchCount > _minDistance)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startPos = touch.position;
                    _dirChosen = false;
                    break;

                case TouchPhase.Moved:
                    _dir = touch.position - _startPos;
                    break;

                case TouchPhase.Ended:
                    _dirChosen = true;
                    break;

            }

            _line.positionCount++;
            _line.SetPosition(_line.positionCount - 1, touch.position);
            _previousPos = touch.position;
        }*/
        #endregion

        if (Input.GetMouseButton(0))
        {
            linepath();
            UpdatePosition(_nextPos);
        }
    }

    void linepath()
    {
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPos.z = 0f;
        if (Vector3.Distance(currentPos, _nextPos) > _minDistance)
        {
            if (_nextPos == transform.position)
            {
                _line.SetPosition(0, currentPos);
            }
            else
            {
                _line.positionCount++;
                _line.SetPosition(_line.positionCount - 1, currentPos);
            }
            _nextPos = currentPos;
        }
    }

    /*IEnumerator SartMoveingGrandma(Vector3 targetPos)
    {
        //_player.transform.position = newPos;
        Vector3 direction = targetPos - _player.transform.position;
        direction = direction.normalized;

        _player.transform.Translate(direction * _speed * Time.deltaTime);
        yield return new WaitForSeconds(.5f);
    }*/

    void playerToPos()
    {
        if(transform.position != _line.GetPosition(_currentIndex)) {
            UpdatePosition(_line.GetPosition(_currentIndex));
        }
        else
        {
            _currentIndex++;
        }
    }

    void UpdatePosition(Vector3 targetPos)
    {
        //check if the player is at the position
        /*if(_player.transform.position == targetPos) 
        {
            Vector3 direction = targetPos - _player.transform.position;
            direction = direction.normalized;

            _player.transform.Translate(direction * _speed * Time.deltaTime);
        }*/
        if (_canMove)
        {
            var posSpeed = _speed * Time.deltaTime;
            _player.transform.position = Vector3.MoveTowards(_player.transform.position, targetPos, posSpeed);
        }

    }
}
