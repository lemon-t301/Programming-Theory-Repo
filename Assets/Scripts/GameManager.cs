using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameType
    {
        undefined,
        guessObject,
        guessNumber,
        guessColor
    }
    private string _gameTitle;

    [SerializeField]
    private GameObject _worldCanvas;
    [SerializeField]
    private GameObject _choiceButton;
    private GameObject[] _choicesButtons;
    private UnityAction<object> _objectButtonClickedAction;

    public int columns = 4;
    private int rows;
    public Vector2 gridCellSize;
    public Vector2 gridOffset;
    private int _gridCellsCount;
    private Vector3 _gridCellPositionBuffer;

    private GameType _gameMode;
    private Item _gameItem;
    private object[] _choices;
    private object _guess;
    private bool _guessed;
    private bool _gameover;

    public GameType GameMode { get { return _gameMode; } private set { _gameMode = value; } }

    private void Awake()
    {
        if (GameObject.Find("PersistentData") == null)
        {
            SceneManager.LoadScene(0);
            return;
        }
        _gameover = false;

        _gameMode = PersistentData.Instance.GameMode;
        _gameItem = PersistentData.Instance.ChosenGame;
        Debug.Log("chosen _gameMode");
        Debug.Log(_gameMode);
        Debug.Log("chosen _gameItem");
        Debug.Log(_gameItem);
        UpdateGameTitle();

        _choices = _gameItem.GetChoices();
        _choicesButtons = new GameObject[_choices.Length];
        ShowChoices();
    }

    private void ShowChoices()
    {
        for (int i = 0; i < _choices.Length; i++)
        {
            Debug.Log("item");
            Debug.Log(_choices[i]);
            _choicesButtons[i] = DisplayChoice(i);
        }

        if (_gameMode == GameType.guessObject) ArrangeChoicesLayout();
    }

    private GameObject DisplayChoice(int i)
    {
        switch (_gameMode)
        {
            case GameType.guessObject:
                return DisplayObjectChoice((GameObject)_choices[i]);

            case GameType.guessNumber:
                return DisplayNumberChoice((int)_choices[i]);

            case GameType.guessColor:
                return DisplayColorChoice((Color)_choices[i]);
        }

        return null;
    }

    private GameObject DisplayNumberChoice(int number)
    {
        GameObject button = InstantiateChoiceButton();

        button.GetComponent<Button>().onClick.AddListener(() => OnGuessClicked(number));

        button.GetComponentInChildren<TMP_Text>().text = number.ToString();

        return button;
    }

    private GameObject DisplayObjectChoice(GameObject gObject)
    {
        GameObject obj = InstantiateChoiceObject(gObject);

        return obj;
    }

    private GameObject DisplayColorChoice(Color color)
    {
        GameObject button = InstantiateChoiceButton();

        button.GetComponent<Button>().onClick.AddListener(() => OnGuessClicked(color));

        ColorBlock cBlock = button.GetComponent<Button>().colors;

        Color.RGBToHSV(color, out float H, out float S, out float V);
        cBlock.normalColor = color;
        cBlock.highlightedColor = Color.HSVToRGB(H,S - 0.3f,V + 0.4f);
        cBlock.pressedColor  = Color.HSVToRGB(H, S - 0.4f, V + 0.6f);
        cBlock.selectedColor = color;
        button.GetComponent<Button>().colors = cBlock;

        return button;
    }

    private GameObject InstantiateChoiceButton()
    {
        return InstantiateChoiceObject(_choiceButton, _worldCanvas.transform);
    }

    private GameObject InstantiateChoiceObject(GameObject gObject, Transform parent = null)
    {
        Debug.Log($"Instantiating \"{gObject.name} - {gObject.tag}\" - parent is: {parent}", gObject);

        if (parent == null) return Instantiate(gObject, Vector3.zero, gObject.transform.rotation);

        return Instantiate(gObject, Vector3.zero, gObject.transform.rotation, parent);
    }

    private void ArrangeChoicesLayout()
    {
        float rawRows = (float)_choicesButtons.Length / (float)columns;
        Debug.Log($"rawRows: {rawRows}");

        rows = Mathf.CeilToInt(rawRows);
        Debug.Log($"Arranging objects with grid layout: {columns} columns X {rows} rows");
        float posX;
        float posY;
        float posZ;

        float startPosX = -(columns * gridCellSize.x) / 2;
        float startPosY = -(rows * gridCellSize.y) / 2;

        int i = 0;
        for (int row = 0; row < rows; row++)
        {
            if (i >= _choicesButtons.Length) break;

            posY = startPosY + row * gridCellSize.y + gridOffset.y;

            for (int col = 0; col < columns; col++)
            {
                if (i >= _choicesButtons.Length) break;

                posX = startPosX + col * gridCellSize.x + gridOffset.x;
                posZ = _choicesButtons[i].transform.position.z;
                _choicesButtons[i].transform.position = new Vector3(posX, posY, posZ);
                Debug.Log($"Setting \"{_choicesButtons[i].tag}\" position to x:{posX} y:{posY} z:{posZ}");
                i++;
            }
        }
    }

    public void RollTheDice()
    {
        _gameItem.GenerateRandom();
    }

    public void Guess(object guess)
    {
        _guess = guess;
        
        RollTheDice();

        _guessed = _gameItem.CompareItem(_guess);

        Debug.Log($"Guess: {guess}");
        Debug.Log($"Randomized value: {_gameItem.Value}");
        Debug.Log($"_guessed: {_guessed}");

        EventManager.TriggerEvent("Guessed");
    }

    //public void OnGuessClicked(object guess)
    public void OnGuessClicked(object guess)
    {
        Debug.Log($"Guessing: {guess}");

        Guess(guess);
        
        for(int i = 0; i < _choicesButtons.Length; i++)
        {
            Destroy(_choicesButtons[i]);
        }
    }

    private void UpdateGameTitle()
    {
        if (_gameMode == GameType.undefined)
        {
            Debug.LogWarning("_gameMode: GameType can't be undefined.");
            return;
        }

        _gameTitle = GetText(_gameMode);
        EventManager.TriggerEvent("GameTitleChanged", _gameTitle);
    }

    private string GetText(object stringID)
    {
        switch (stringID)
        {
            case GameType.guessObject:
                return "Guess the Object";
            case GameType.guessNumber:
                return "Guess the Number";
            case GameType.guessColor:
                return "Guess the Color";
            default:
                Debug.LogWarning("GetText: stringID is undefined, can't retrieve text.");
                return "Text not found";
        }
    }

    public void NewGame()
    {
        Destroy(GameObject.Find("PersistentData"));
        SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
        if (_objectButtonClickedAction == null) _objectButtonClickedAction = new UnityAction<object>(OnGuessClicked);

        EventManager.StartListening("ObjectItemClicked", _objectButtonClickedAction);
    }

    private void OnDisable()
    {
        EventManager.StopListening("ObjectItemClicked", _objectButtonClickedAction);
    }
}