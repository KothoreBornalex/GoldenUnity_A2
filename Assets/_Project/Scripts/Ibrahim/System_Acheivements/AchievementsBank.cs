using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementsBank", menuName = "AchievementsBank", order = 1)]
public class AchievementsBank : ScriptableObject
{
    [SerializeField] private string _amogUs_ID;
    [SerializeField] private string _deathForTheGameDesign_ID;
    [SerializeField] private string _love_ID;
    [SerializeField] private string _notThatBad_ID;
    [SerializeField] private string _oneLastTime_ID;



    public string AmogUs_ID { get => _amogUs_ID;}
    public string DeathForTheGameDesign_ID { get => _deathForTheGameDesign_ID;}
    public string Love_ID { get => _love_ID;}
    public string NotThatBad_ID { get => _notThatBad_ID;}
    public string OneLastTime_ID { get => _oneLastTime_ID;}
}
