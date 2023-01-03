using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : Item
{
    public GameObject[] randomObjects;

    public override void GenerateRandom()
    {
        if (randomObjects.Length == 0)
        {
            Debug.LogWarning("Can't generate random object, objects list is empty");
            return;
        }

        _value = randomObjects[Random.Range(0, randomObjects.Length)].tag;
    }

    public override bool CompareItem(object obj)
    {
        GameObject gObject = (GameObject)obj;

        return ((string)_value == gObject.tag);
    }

    public override object[] GetChoices()
    {
        return randomObjects;
    }
}
