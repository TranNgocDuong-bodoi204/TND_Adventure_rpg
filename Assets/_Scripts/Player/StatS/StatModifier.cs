using System;
public class StatModifiers
{
    public float value;
    public StatModifyType type;
    public int order;

    public StatModifiers(float value, StatModifyType type, int order)
    {
        this.value = value;
        this.type = type;
        this.order = order;
    }

    public StatModifiers(float value, StatModifyType type) : this(value, type, (int)type){}
}
