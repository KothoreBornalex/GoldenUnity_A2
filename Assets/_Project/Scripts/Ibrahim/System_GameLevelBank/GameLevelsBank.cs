using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLevelsBank", menuName = "GameLevels/Bank", order = 1)]
public class GameLevelsBank : ScriptableObject
{
    [SerializeField] private GameLevel _mainMenu;
    [SerializeField] private GameLevel _pauseMenu;

    [SerializeField] private List<GameLevel> _gameLevels;
    public List<GameLevel> GameLevels { get => _gameLevels;}
    public GameLevel MainMenu { get => _mainMenu;}
    public GameLevel PauseMenu { get => _pauseMenu;}
}
