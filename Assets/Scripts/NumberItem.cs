using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberItem : Item
{
    private int _number;

    public int Number { get { return _number; } protected set { _number = value; } }

    public int rangeMin = 1;
    public int rangeMax = 3;

    private int[] _rangeNumbers;

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

        _number = Random.Range(rangeMin, rangeMax);
        _value = _number;
    }

    public override bool CompareItem(object obj)
    {
        int number = (int)obj;
        return (number == this._number);
    }

    public override object[] GetChoices()
    {
        if (_rangeNumbers == null || _rangeNumbers.Length == 0)
        {
            _rangeNumbers = new int[rangeMax - rangeMin + 1];
            int value = rangeMin;

            for (int i = 0; i < _rangeNumbers.Length; i++)
            {
                _rangeNumbers[i] = value;
                value++;
            }
        }

        object[] objects;
        objects = System.Array.ConvertAll(_rangeNumbers, item => item as object);
        return objects;
    }
}
