using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected string _value;

    public string Value { get { return _value; } protected set { _value = value; } }

    public abstract void GenerateRandom();

    public virtual bool CompareItem(string value)
    {
        return (value == this._value);
    }
}
