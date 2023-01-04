using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericItem<T> : MonoBehaviour
{
    protected T _value;
    protected T[] _validValues;

    public T Value { get { return _value; } protected set { _value = value; } }
    public T[] ValidValues { get { return _validValues; } protected set { _validValues = value; } }

    public abstract void GenerateRandom();

    public abstract bool CompareItem(T value);
}
