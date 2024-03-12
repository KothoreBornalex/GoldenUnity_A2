using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraDetector : Objective
{

    public void SetObjectiveCompleted()
    {
        IsCompleted = true;
    }

    public void SetObjectiveUnCompleted()
    {
        IsCompleted = false;
    }
}
