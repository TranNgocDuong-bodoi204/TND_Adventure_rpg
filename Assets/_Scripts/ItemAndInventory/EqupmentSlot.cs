using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class EqupmentSlot : ItemSlot
{
    public EquipmentType equipmentType;
     void OnValidate()
    {
        string name = equipmentType.ToString() +" slot";
        this.gameObject.name = name;
    }
}
