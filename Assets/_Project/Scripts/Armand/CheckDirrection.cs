using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CheckDirrection : GuardGlobalClass
{
    [SerializeField] bool _lookRight;
    [SerializeField] bool _lookLeft;
    [SerializeField] bool _lookUp;
    [SerializeField] bool _lookDown;
    [SerializeField] float _waitingTime;
    private bool _isIspecting = false;




    private void Update()
    {
        if (!_isIspecting)
        {
            StartCoroutine(InspectSuroundings(_waitingTime));
        }
    }

    private void FixedUpdate()
    {
        //Quaternion.AngleAxis(20f, Vector3.forward) * Vector2.right
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator InspectSuroundings(float _waitngTime)
    {
        _isIspecting = true;
        if (_lookRight)
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
            //Debug.Log("hello");
            yield return new WaitForSeconds(_waitingTime);
        }
        

        if (_lookUp)
        {
            transform.rotation = new Quaternion(0, 0, 0.7071f, 0.7071f);
            //Debug.Log("hello3");
            yield return new WaitForSeconds(_waitingTime);
        }

        if (_lookLeft)
        {
            transform.rotation = new Quaternion(0, 0, 1, 0);
            //Debug.Log("hello2");
            yield return new WaitForSeconds(_waitingTime);
        }
        

        if (_lookDown)
        {
            transform.rotation = new Quaternion(0, 0, -0.7071f, 0.7071f);
            //Debug.Log("hello4");
            yield return new WaitForSeconds(_waitingTime);
        }
        _isIspecting = false;
    }
}

