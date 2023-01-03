using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected object _value;

    public object Value { get { return _value; } protected set { _value = value; } }

    public abstract void GenerateRandom();

    public abstract bool CompareItem(object value);

    public abstract object[] GetChoices();
}
