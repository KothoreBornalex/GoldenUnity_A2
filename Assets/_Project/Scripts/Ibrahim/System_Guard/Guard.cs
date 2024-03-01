using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Objective
{

    public void Eliminate()
    {
        IsCompleted = true;
        gameObject.SetActive(false);
    }
}
