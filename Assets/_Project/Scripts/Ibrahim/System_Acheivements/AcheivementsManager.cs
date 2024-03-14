using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class AcheivementsManager : MonoBehaviour
{
    public static AcheivementsManager instance;

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


    public void UnlockSuccess(string successID)
    {
        PlayGamesPlatform.Instance.UnlockAchievement(successID);
    }

}
