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
    private Color _color;

    public Color Color { get { return _color; } protected set { _color = value; } }

    public Color[] randomColors;

    public override void GenerateRandom()
    {
        if (randomColors.Length == 0)
        {
            Debug.LogWarning("Can't generate random color, colors list is empty");
            return;
        }

        _color = randomColors[Random.Range(0, randomColors.Length)];
        _value = _color;
    }

    public override bool CompareItem(object obj)
    {
        Color color = (Color)obj;
        return (this._color.Equals(color));
    }

    public override object[] GetChoices()
    {
        object[] objects;
        objects = System.Array.ConvertAll(randomColors, item => item as object);
        return objects;
        //return randomColors;
    }
}
