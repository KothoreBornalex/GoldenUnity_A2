using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfindingMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] LineRenderer _line;
    [SerializeField] float _minDistance = .1f;


    private Vector3 _previousPos;
    private Vector2 _startPos;
    private Vector2 _dir;

    public bool _dirChosen;

    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        _previousPos = transform.position;
    }

    private void Update()
    {
        #region touchLinerenderer
        if (Input.touchCount > _minDistance)
        {
            Touch touch = Input.GetTouch(0);
           /* switch (touch.phase)
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

            }*/

            _line.positionCount++;
            _line.SetPosition(_line.positionCount - 1, touch.position);
            _previousPos = touch.position;
        }
        #endregion

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPos.z = 0f;
            if(Vector3.Distance(currentPos, _previousPos) > _minDistance) 
            {
                if(_previousPos == transform.position) 
                { 

                }

                _line.positionCount++;
                _line.SetPosition(_line.positionCount -1, currentPos);
                _previousPos = currentPos;
            }
        }
    }

}
