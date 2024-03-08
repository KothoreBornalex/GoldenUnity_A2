using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Guard : Objective
{
    [SerializeField] private CheckDirrection _checkDirection;
    private Transform _target;
    public void Eliminate()
    {
        IsCompleted = true;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (_target == null) return;

        Vector2 direction = _target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3.0f * Time.deltaTime);
    }




    public void SetTarget(Transform target)
    {
        _checkDirection.enabled = false;
        _target = target;
    }

/*    public IEnumerator RotateTowardEmitter(Transform point)
    {
        _checkDirection.enabled = false;

        float time = 1.0f;
        bool continueRotation = true;
        while (continueRotation)
        {
            Vector3 direction = point.position - transform.position;

            // Create quaternion rotation to look towards the target
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the target
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 3.0f * Time.deltaTime);
            time -= Time.deltaTime;
            if(time < 0.0f)
            {
                continueRotation = false;
            }
        }

        yield return null;


    }*/
}
