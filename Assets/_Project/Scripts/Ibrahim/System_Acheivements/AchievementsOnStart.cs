using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsOnStartn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UnlockSuccess(AchievementsBank.Success.AmongUs);
    }


}
