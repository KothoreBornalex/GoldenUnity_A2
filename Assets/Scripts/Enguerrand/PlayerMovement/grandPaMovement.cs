using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandPaMovement : MonoBehaviour
{
    [SerializeField] GameObject _grandPa;
    [SerializeField] float _speed;

    private Vector2 _startPos;
    private Vector2 _endPos;

    private bool _canMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canMove)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _startPos = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _endPos = Input.GetTouch(0).position;
                if (_endPos.x < _startPos.x)
                {
                    grandPaMoveDirLeft();
                }

                if (_endPos.x > _startPos.x)
                {
                    grandPaMoveDirRigth();
                }
            }
        }
    }

    void grandPaMoveDirLeft()
    {
        _grandPa.transform.position = new Vector2(_grandPa.transform.position.x +1, _grandPa.transform.position.y);
    }
    void grandPaMoveDirRigth()
    {
        _grandPa.transform.position = new Vector2(_grandPa.transform.position.x - 1, _grandPa.transform.position.y);
    }
}
