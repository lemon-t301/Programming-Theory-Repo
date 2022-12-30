using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorItem : Item
{
    /*private Color _color;

    public Color Color { 
        get { return _color; }
        private set { _color = value; }
    }*/
    new private Color _value;

    new public Color Value { get { return _value; } protected set { _value = value; } }

    public Color[] randomColors;

    public override void GenerateRandom()
    {
        if (randomColors.Length == 0)
        {
            Debug.LogWarning("Can't generate random color, colors list is empty");
            return;
        }

        _value = randomColors[Random.Range(0, randomColors.Length)];
    }

    public bool CompareItem(Color color)
    {
        return (this._value.Equals(color));
    }
}
