using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameType
    {
        undefined,
        guessObject,
        guessNumber,
        guessColor
    }

    private GameType _gameMode;

    public GameType GameMode { get { return _gameMode; } private set { _gameMode = value; } }

    private void Awake()
    {
        
    }
}
