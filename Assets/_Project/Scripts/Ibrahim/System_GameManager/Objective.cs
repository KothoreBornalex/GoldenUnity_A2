using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [Header("Objective Fields")]
    [SerializeField] private bool _isCompleted;

    public bool IsCompleted { get => _isCompleted; set => _isCompleted = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected void Complete()
    {
        _isCompleted = true;
    }
}


