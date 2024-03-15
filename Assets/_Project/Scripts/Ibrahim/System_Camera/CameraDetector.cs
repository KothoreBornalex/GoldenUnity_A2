using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraDetector : Objective
{
    [SerializeField, Range(1, 15)] private float _onIntensity = 10.0f;
    [SerializeField, Range(0, 1)] private float _offIntensity = 0.0f;
    private Light2D _light;

    private void Awake()
    {
        _light = GetComponentInChildren<Light2D>();
    }

    public void SetObjectiveCompleted()
    {
        IsCompleted = true;
    }

    public void SetObjectiveUnCompleted()
    {
        IsCompleted = false;
    }

    private void Update()
    {
        if (IsCompleted)
        {
            if(_light) _light.intensity = Mathf.Lerp(_light.intensity, _offIntensity, Time.deltaTime);
        }
        else
        {
            if(_light) _light.intensity = Mathf.Lerp(_light.intensity, _onIntensity, Time.deltaTime);
        }
    }
}
