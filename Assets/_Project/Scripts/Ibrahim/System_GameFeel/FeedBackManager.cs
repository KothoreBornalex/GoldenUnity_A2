using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
    public static FeedBackManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public IEnumerator BouncingBig(Transform transform, float boucingPower, float bouncingSpeed)
    {

        bool effective = true;
        bool growing = true;
        Vector3 baseScale = transform.localScale;
        while (effective)
        { 
            if (growing)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, baseScale * boucingPower, Time.deltaTime * bouncingSpeed);

                if(transform.localScale.magnitude >= (baseScale * boucingPower).magnitude )
                {
                    Debug.Log("Growing set false");
                    growing = false;
                }
            }
            else
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, baseScale, Time.deltaTime * bouncingSpeed);

                if (baseScale.magnitude >= transform.localScale.magnitude)
                {
                    Debug.Log("effective set false");
                    effective = false;
                }
            }


            yield return null;
        }

    }



    public IEnumerator BouncingSmall(Transform transform, float boucingPower, float bouncingSpeed)
    {

        bool effective = true;
        bool growing = true;
        Vector3 baseScale = transform.localScale;
        while (effective)
        {
            if (growing)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, baseScale * boucingPower, Time.deltaTime * bouncingSpeed);

                if (transform.localScale.magnitude <= (baseScale * boucingPower).magnitude)
                {
                    Debug.Log("Growing set false");
                    growing = false;
                }
            }
            else
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, baseScale, Time.deltaTime * bouncingSpeed);

                if (baseScale.magnitude >= transform.localScale.magnitude)
                {
                    Debug.Log("effective set false");
                    effective = false;
                }
            }


            yield return null;
        }

    }

}
