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

    private void Start()
    {
        _player1 = GameObject.FindGameObjectWithTag("Player1");
        _player2 = GameObject.FindGameObjectWithTag("Player2");
        _viewCone = GetComponent<Light2D>();
        _viewCone.pointLightOuterAngle = _viewAngle;
        _viewCone.pointLightInnerAngle = _viewAngle;
        _viewCone.pointLightOuterRadius = _viewRadius;

    }

    private void Update()
    {
        
        
       
    }

    private void FixedUpdate()
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
        

        //Debug.DrawRay(transform.position, Quaternion.Euler(0f, 0f, -_viewAngle / 2) * transform.up * _viewRadius, Color.blue);
        //Debug.DrawRay(transform.position, Quaternion.Euler(0f, 0f, _viewAngle / 2) * transform.up * _viewRadius , Color.blue);
        /*RaycastHit2D _lineOfSight = Physics2D.Raycast(transform.position, _player1.transform.position - transform.position);
        RaycastHit2D _lineOfSight2 = Physics2D.Raycast(transform.position, _player2.transform.position - transform.position);
        if (_lineOfSight.collider != null || _lineOfSight2.collider != null)
        {
            _hasLineOfSight1 = _lineOfSight.collider.CompareTag("Player1");
            _hasLineOfSight2 = _lineOfSight2.collider.CompareTag("Player2");
            if (_hasLineOfSight1)
            {
                Debug.DrawRay(transform.position, _player1.transform.position - transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, _player1.transform.position - transform.position, Color.red);
            }
            if (_hasLineOfSight2)
            {
                Debug.DrawRay(transform.position, _player2.transform.position - transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, _player2.transform.position - transform.position, Color.red);
            }
        }*/
    }


}
