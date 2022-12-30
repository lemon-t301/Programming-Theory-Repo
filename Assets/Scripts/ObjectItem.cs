using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : Item
{
    private string[] _randomKeys;

    public Dictionary<string, GameObject> randomObjects;

    private void Awake()
    {
        randomObjects.Keys.CopyTo(_randomKeys, 0);
    }

    public override void GenerateRandom()
    {
        if (_randomKeys.Length == 0)
        {
            Debug.LogWarning("Can't generate random object, objects list is empty");
            return;
        }

        _value = _randomKeys[Random.Range(0, _randomKeys.Length)];
    }

}
