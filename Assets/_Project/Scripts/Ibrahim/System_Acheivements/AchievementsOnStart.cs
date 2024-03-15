using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AcheivementsManager.instance.UnlockSuccess(GameAssets.instance.AchievementsBank.AmogUs_ID);
    }


}
