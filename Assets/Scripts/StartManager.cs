using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public ObjectItem objectItem;
    public NumberItem numberItem;
    public ColorItem colorItem;

    public void StartGuessObjectGame()
    {
        StartGame(GameManager.GameType.guessObject);
    }
    public void StartGuessNumberGame()
    {
        StartGame(GameManager.GameType.guessNumber);
    }
    public void StartGuessColorGame()
    {
        StartGame(GameManager.GameType.guessColor);
    }

    private void StartGame(GameManager.GameType gameMode)
    {
        Item item = null;

        switch (gameMode)
        {
            case GameManager.GameType.guessObject:
                item = objectItem;
                break;

            case GameManager.GameType.guessNumber:
                item = numberItem;
                break;

            case GameManager.GameType.guessColor:
                item = colorItem;
                break;
        }

        PersistentData.SavePersistentData(item, gameMode);
        SceneManager.LoadScene(1);
    }
}
