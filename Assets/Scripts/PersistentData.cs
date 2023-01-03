using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour
{
    private static PersistentData _instance;
    public static PersistentData Instance
    {
        get
        {
            return _instance;
        }
        private set { }
    }

    private Item _chosenGame;
    public Item ChosenGame { get { return _chosenGame; } private set { _chosenGame = value; } }

    private GameManager.GameType _gameMode;
    public GameManager.GameType GameMode { get { return _gameMode; } private set { _gameMode = value; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetPersistentData(Item chosenItem)
    {
        Debug.Log("SetPersistentData:");
        Debug.Log(chosenItem);
        if (chosenItem != null)
        {
            _chosenGame = chosenItem;
            //DontDestroyOnLoad(_chosenItem.gameObject);
            return;
        }
        Debug.LogWarning("Can't set chosen item, Item parameter cannot be null.");
    }

    public void SetPersistentData(GameManager.GameType gameMode)
    {
        if (gameMode != GameManager.GameType.undefined)
        {
            _gameMode = gameMode;
            return;
        }
        Debug.LogWarning("Can't set game mode, GameType parameter cannot be undefined.");
    }

    public void SetPersistentData(Item chosenItem, GameManager.GameType gameMode)
    {
        SetPersistentData(chosenItem);
        SetPersistentData(gameMode);
    }

    public static void SavePersistentData(Item chosenItem, GameManager.GameType gameMode)
    {
        if (_instance == null) return;
        _instance.SetPersistentData(chosenItem, gameMode);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Loaded scene {scene.name}.");
        Debug.Log("Saved _chosenGame");
        Debug.Log(_chosenGame);
        Debug.Log("Saved _gameMode");
        Debug.Log(_gameMode);
    }
}
