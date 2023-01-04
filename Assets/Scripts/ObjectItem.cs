using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : Item
{
    private GameObject _objValue;

    public GameObject ObjValue { get { return _objValue; } protected set { _objValue = value; } }

    public GameObject[] randomObjects;

    public override void GenerateRandom()
    {
        if (randomObjects.Length == 0)
        {
            Debug.LogWarning("Can't generate random object, objects list is empty");
            return;
        }

        _objValue = randomObjects[Random.Range(0, randomObjects.Length)];
        _value = _objValue.tag;
    }

    public override bool CompareItem(object obj)
    {
        GameObject gObject = (GameObject)obj;

        return _value.ToString() == gObject.tag;
    }

    public override object[] GetChoices()
    {
        return randomObjects;
    }
}
