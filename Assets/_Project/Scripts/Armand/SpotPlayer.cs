using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class SpotPlayer : MonoBehaviour
{
    [Header("Detector Fields")]
    [SerializeField] private LayerMask _layersToCollideWith;

    [Header("Unity Events Fields")]
    [SerializeField] private UnityEvent OnPlayerSpottedUnityEvent;

    private Light2D _viewCone;
    private float _viewAngle;
    private float _viewRadius;

    private bool _hasLineOfSight = false;
    private float _rotationAngle;
    private float _incrementAngle;
    private float _rotationSpeed = 200;

    private void Start()
    {
        _rotationSpeed = 200;
        _viewCone = GetComponent<Light2D>();

        _viewAngle = _viewCone.pointLightOuterAngle;
        _viewRadius = _viewCone.pointLightOuterRadius;

    }

    private void FixedUpdate()
    {
        _rotationAngle = Mathf.PingPong(_incrementAngle, _viewAngle);
        _incrementAngle += Time.deltaTime * _rotationSpeed;

        RaycastHit2D _lineOfSight = Physics2D.Raycast(transform.position, Quaternion.Euler(0f, 0f, (-_viewAngle / 2) + _rotationAngle) * transform.up * _viewRadius, _viewRadius, _layersToCollideWith);
        if (_lineOfSight.collider != null)
        {

            Debug.DrawLine(transform.position, _lineOfSight.point, Color.blue);

            if (_lineOfSight.collider.CompareTag("Player"))
            {
                GameManager.Instance.EndGame(false);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Quaternion.Euler(0f, 0f, (-_viewAngle / 2) + _rotationAngle) * transform.up * _viewRadius, Color.blue);
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
