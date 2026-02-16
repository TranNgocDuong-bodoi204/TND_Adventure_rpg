using System;
using UnityEngine;

[CreateAssetMenu(fileName ="EqupmentSO", menuName = "ScriptableObjects/EqupmentSO")]
public class EqupmentSO : ItemSO
{
    public int damage;
    public int armor;
    public int crit;

    [Space]
    public int damgePercent;
    public int armorPercent;
    public int critPercent;
    [Space]
    public EquipmentType equipmentType;
}

public enum EquipmentType
{
    Mu,
    Ao,
    Quan,
    Giay,
    Gang,
}
