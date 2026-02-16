using System;
using System.Collections.Generic;
[System.Serializable]
public class CharacterStat
{
    public float baseValue;

    protected bool isDirty = true;
    protected float FinalValue;
    protected float lastBaseValue = float.MinValue;   
    public List<StatModifiers> statModifiers;
    public float Value
    {
        get
        {
            if(isDirty || lastBaseValue != baseValue)
            {
                lastBaseValue = baseValue;
                FinalValue = CalculateFinalValue();
                isDirty = false;
            }
            return FinalValue;
        }
    }


    public CharacterStat()
    {
        statModifiers = new List<StatModifiers>();
    }
    public CharacterStat(float baseValue) : this()
    {
        this.baseValue = baseValue;
    }

    public virtual void AddModifier(StatModifiers mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    protected virtual int CompareModifierOrder(StatModifiers a, StatModifiers b)
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

    public virtual bool RemoveModifier(StatModifiers mod)
    {
        if(statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }
    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for(int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if(statModifiers[i].source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float percentAddSum = 0;

        for(int i = 0; i < statModifiers.Count; i++)
        {
            StatModifiers mod = statModifiers[i];
            if(mod.type == StatModifyType.Flat)
            {
                finalValue += mod.value;
            }

            if(mod.type == StatModifyType.PercentAdd)
            {
                percentAddSum += mod.value;

                if(i + 1 >= statModifiers.Count || statModifiers[i+1].type != StatModifyType.PercentAdd)
                {
                    finalValue *= 1 + percentAddSum; // value as 0.1 for 10%
                    percentAddSum = 0;
                }
            }

            if(mod.type == StatModifyType.PercentMult)
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
