using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TypedEvent : UnityEvent<object> { }

public class EventManager : MonoBehaviour
{
    public string[] eventNames;
    public string[] typedEventNames;

    private Dictionary<string, UnityEvent> _eventDictionary;
    private Dictionary<string, TypedEvent> _typedEventDictionary;
    private static EventManager _eventManager;

    public static EventManager instance
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!_eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }
                else
                {
                    _eventManager.Init();
                }
            }

            return _eventManager;
        }
    }

    void Init()
    {
        _eventDictionary = new Dictionary<string, UnityEvent>();
        _typedEventDictionary = new Dictionary<string, TypedEvent>();

        for (int i = 0; i < eventNames.Length; i++)
        {
            instance._eventDictionary.Add(eventNames[i], new UnityEvent());
        }

        for (int i = 0; i < typedEventNames.Length; i++)
        {
            instance._typedEventDictionary.Add(typedEventNames[i], new TypedEvent());
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
            return;
        }

        Debug.LogError($"Event {eventName} not defined.");
    }

    public static void StartListening(string eventName, UnityAction<object> listener)
    {
        TypedEvent thisEvent = null;
        if (instance._typedEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
            return;
        }

        Debug.LogError($"Event {eventName} not defined.");
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (_eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void StopListening(string eventName, UnityAction<object> listener)
    {
        if (_eventManager == null) return;
        TypedEvent thisEvent = null;
        if (instance._typedEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public static void TriggerEvent(string eventName, object arguments)
    {
        TypedEvent thisEvent = null;
        if (instance._typedEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(arguments);
        }
    }
}