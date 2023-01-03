using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    public string TextChangeEvent;

    private TMP_Text _tmpText;
    private UnityAction<object> _updateText;

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        if (_updateText == null) _updateText = new UnityAction<object>(OnTextChange);
        EventManager.StartListening(TextChangeEvent, _updateText);
    }

    private void OnDisable()
    {
        EventManager.StopListening(TextChangeEvent, _updateText);
    }

    private void OnTextChange(object text)
    {
        UpdateText((string)text);
    }

    public void UpdateText(string text)
    {
        _tmpText.text = text;
    }
}
