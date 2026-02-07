using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CharacterStat
{
    public float baseValue;

    public float Value
    {
        get
        {
            if(isDirty)
            {
                FinalValue = CalculateFinalValue();
                isDirty = false;
            }
            return FinalValue;
        }
    }
    private bool isDirty = true;
    private float FinalValue;

    public List<StatModifiers> statModifiers;

    public CharacterStat(float baseValue)
    {
        this.baseValue = baseValue;
        statModifiers = new List<StatModifiers>();
    }

    public void AddModifier(StatModifiers mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder); // lambda
    }

    public int CompareModifierOrder(StatModifiers a, StatModifiers b)
    {
        if(a.order < b.order)
        {
            return -1;
        }
        else if(a.order > b.order)
        {
            return 1;
        }
        return 0;
    }

    public void RemoveModifier(StatModifiers mod)
    {
        isDirty = true;
        statModifiers.Remove(mod);
    }

    public float CalculateFinalValue()
    {
        float finalValue = baseValue;

        foreach (StatModifiers mod in statModifiers)
        {
            finalValue += mod.value;
            if(mod.type == StatModifyType.Flat)
            {
                finalValue += mod.value;
            }

            if(mod.type == StatModifyType.PercentAdd)
            {
                finalValue *= 1 + mod.value; // value as 0.1 for 10%
            }
        }

        return (float)Math.Round(finalValue,3);
    }

    /* khi tính toán chỉ số sẽ sort order theo thứ tự flat -> percent
     khi đó tính toán sẽ cộng các giá trj flat rồi mới nhân với percent để đúng logic
    */
}
