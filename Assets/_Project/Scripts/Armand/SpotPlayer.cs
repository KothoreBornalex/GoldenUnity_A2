using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpotPlayer : MonoBehaviour
{
    private Light2D _viewCone;
    [SerializeField] float _viewAngle;
    [SerializeField] float _viewRadius;
    private GameObject _player1;
    private GameObject _player2;
    private bool _hasLineOfSight = false;
    private float _rotationAngle;
    private float _incrementAngle;
    [SerializeField] float _rotationSpeed;

    private void Start()
    {
        //_player1 = GameObject.FindGameObjectWithTag("Player1");
        //_player2 = GameObject.FindGameObjectWithTag("Player2");
        _viewCone = GetComponent<Light2D>();
        _viewCone.pointLightOuterAngle = _viewAngle;
        _viewCone.pointLightInnerAngle = _viewAngle;
        _viewCone.pointLightOuterRadius = _viewRadius;

    }

    private void FixedUpdate()
    {
        _rotationAngle = Mathf.PingPong(_incrementAngle, _viewAngle);
        _incrementAngle += Time.deltaTime * _rotationSpeed;
        RaycastHit2D _lineOfSight = Physics2D.Raycast(transform.position, Quaternion.Euler(0f, 0f, (-_viewAngle / 2) + _rotationAngle) * transform.up * _viewRadius, _viewRadius);
        Debug.DrawRay(transform.position, Quaternion.Euler(0f, 0f, (-_viewAngle / 2) + _rotationAngle ) * transform.up * _viewRadius, Color.blue);
        if (_lineOfSight.collider != null)
        {
            _hasLineOfSight = _lineOfSight.collider.CompareTag("Player1");

            if (_hasLineOfSight)
            {
                Time.timeScale = 0;
            }

        }
    }

    /*private void FixedUpdate()
    {
        for(int i=0; i < _viewAngle; i++)
        {
            RaycastHit2D _lineOfSight = Physics2D.Raycast(transform.position, Quaternion.Euler(0f, 0f, -_viewAngle / 2 + i) * transform.up * _viewRadius, _viewRadius);

            //Debug.DrawRay(transform.position, Quaternion.Euler(0f, 0f, -_viewAngle / 2 +i) * transform.up * _viewRadius, Color.blue);
            
            if (_lineOfSight.collider != null)
            {
                _hasLineOfSight = _lineOfSight.collider.CompareTag("Player1");
                
                if (_hasLineOfSight)
                {
                    Time.timeScale = 0;
                }
                
            }
            
        }
        

        
    }*/


}
