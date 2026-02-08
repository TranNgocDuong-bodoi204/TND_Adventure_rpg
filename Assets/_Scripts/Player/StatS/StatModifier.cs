using System;
public class StatModifiers
{
    public float value;
    public StatModifyType type;
    public int order;
    public object source;

    public StatModifiers(float value, StatModifyType type, int order, object source)
    {
        this.value = value;
        this.type = type;
        this.order = order;
        this.source = source;
    }

    public StatModifiers(float value, StatModifyType type) : this(value, type, (int)type, null){}
    public StatModifiers(float value, StatModifyType type, int order) : this(value, type, order, null) { }  
    public StatModifiers(float value, StatModifyType type, object source) : this(value, type, (int)type, source) { }
}
