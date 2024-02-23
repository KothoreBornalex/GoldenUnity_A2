using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PatrolEnnemy : MonoBehaviour
{
    [SerializeField] float _speed = 2f;
    [SerializeField, Range(0f, 50f)]
    private float _rightLimit = 1f;
    [SerializeField, Range(0f, 50f)]
    private float _leftLimit = 1f;
    [SerializeField, Range(0f, 50f)]
    private float _topLimit = 1f;
    [SerializeField, Range(0f, 50f)]
    private float _botomLimit = 1f;

    private Vector3 _rightLimitPos;
    private Vector3 _leftLimitPos;
    private Rigidbody2D _rb;
    
    private float _directionX = 1f;
    private float _directionY = 1f;


    private Vector3 _rightInitial;
    private Vector3 _leftInitial;

    private void Start()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        
        _rightLimitPos = transform.position + new Vector3(_rightLimit, _topLimit, 0);
        _leftLimitPos = transform.position - new Vector3(_leftLimit, _botomLimit, 0);
        _rightInitial = _rightLimitPos;
        _leftInitial = _leftLimitPos;
    }

    private void Update()
    {
        ChooseDirectionX();
        ChooseDirectionY();
        RotateEnnemy();
        //Debug.Log(_directionY);
        
       

    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_directionX * _speed,_directionY * _speed);

    }

    private float ChooseDirectionX()
    {
        if (Mathf.Abs(_rb.velocity.x) < 0.1f)
        {
            _directionX = -_directionX;
        }

        else if (transform.position.x > _rightLimitPos.x)
        {
            _directionX = -1f;
        }

        else if (transform.position.x < _leftLimitPos.x)
        {
            _directionX = 1f;

        }

        else if (_rightLimit == 0 && _leftLimit == 0)
        {
            _directionX = 0f;
        }
        return _directionX;
    }
    private float ChooseDirectionY()
    {
        if (Mathf.Abs(_rb.velocity.y) < 0.1f)
        {
            _directionY = -_directionY;
        }

        else if (transform.position.y > _rightLimitPos.y)
        {
            _directionY = -1f;

        }

        else if (transform.position.y < _leftLimitPos.y)
        {
            _directionY = 1f;

        }

        else if (_topLimit == 0 && _botomLimit == 0)
        {
            _directionY = 0f;
        }
        return _directionY;
       

    }
    
    private void OnDrawGizmos()
    {
        if (!Application.IsPlaying(gameObject))
        {
            _rightLimitPos = transform.position + new Vector3(_rightLimit, _topLimit, 0);
            _leftLimitPos = transform.position - new Vector3(_leftLimit, _botomLimit, 0);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_rightLimitPos, 0.5f);
        Gizmos.DrawSphere(_leftLimitPos, 0.5f);
        Gizmos.DrawLine(_rightLimitPos, _leftLimitPos);
        
    }

    private void RotateEnnemy()
    {
        if (_directionX == 1f && _directionY == 0f)
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
        }
        else if (_directionX == -1f && _directionY == 0f)
        {
            transform.rotation = new Quaternion(0,0,1,0);
        }

        if (_directionY == 1f && _directionX == 0f)
        {
            transform.rotation = new Quaternion(0, 0, 0.7071f, 0.7071f);
            
        }
        else if ( _directionY == -1f && _directionX == 0f)
        {
            transform.rotation = new Quaternion(0, 0, -0.7071f, 0.7071f);
        }
    }
}
