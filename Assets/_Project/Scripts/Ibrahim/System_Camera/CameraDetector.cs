using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraDetector : Objective
{
    [SerializeField] private Light2D _light;
    [SerializeField] private SpotPlayer _detector;

    public void Desactivate()
    {
        IsCompleted = true;
        _light.intensity = 0.3f;
        _detector.enabled = false;
    }
}
