using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField]private StatDefinition stat;
    private float currentValue; // giá trị hiện tại
    [SerializeField]private float currentBaseValue; // giá trị base hiện tại
    private List<float> modifierValue = new List<float>();
    private List<float> pointValue = new List<float>();

    public void calculateValue()
    {
        float finalValue = stat.baseValue + modifierValue.Sum() + pointValue.Sum();
        currentBaseValue = finalValue; 
    }
    public void AddModifyValue(float value)
    {
        modifierValue.Add(value);
        calculateValue();
    }
    public void AddPointValue(float value)
    {
        pointValue.Add(value);
        calculateValue();
    }
}
[Serializable]
public class StatDefinition
{
    public StatType type;
    public float baseValue = 100;
}