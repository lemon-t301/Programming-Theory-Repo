using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberItem : Item
{
    new private int _value;

    new public int Value { get { return _value; } protected set { _value = value; } }

    public int rangeMin = 1;
    public int rangeMax = 3;

    public override void GenerateRandom()
    {
        if (rangeMin < 1)
        {
            Debug.LogWarning("Can't generate random number, min range value is too low.");
            return;
        }
        if (rangeMax > 100)
        {
            Debug.LogWarning("Can't generate random number, max range value are too high.");
            return;
        }
        if (rangeMax <= rangeMin)
        {
            Debug.LogWarning("Can't generate random number, ranges are not valid.");
            return;
        }

        _value = Random.Range(rangeMin, rangeMax);
    }

    public bool CompareItem(int number)
    {
        return (number == this._value);
    }
}
