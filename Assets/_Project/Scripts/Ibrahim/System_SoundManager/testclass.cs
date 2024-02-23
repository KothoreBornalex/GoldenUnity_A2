using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testclass : MonoBehaviour
{
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 3.0f)
        {
            SoundManager.PlaySoundAtPosition(GameAssets.Instance.SoundBank._buttonFocus, transform.position);
            timer = 0;
        }
    }
}
