using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem<T> : GenericItem<T>
{

    public override void GenerateRandom()
    {
        if (_validValues.Length == 0) return;

        _value = _validValues[Random.Range(0, _validValues.Length)];
    }

    public override bool CompareItem(T value)
    {
        return _value.Equals(value);
    }
}
