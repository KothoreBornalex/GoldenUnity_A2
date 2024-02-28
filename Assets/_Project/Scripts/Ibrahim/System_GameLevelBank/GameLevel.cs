using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class GameLevel
{
    [SerializeField] private int _levelID;
    [SerializeField] private string _sceneName;
    [SerializeField] private Sprite _illustration;

    public int LevelID { get => _levelID; set => _levelID = value; }
    public string SceneName { get => _sceneName; set => _sceneName = value; }
    public Sprite Illustration { get => _illustration; set => _illustration = value; }
}
