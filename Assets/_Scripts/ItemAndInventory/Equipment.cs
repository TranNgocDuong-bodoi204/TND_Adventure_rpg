using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Transform equipmentSlotParent;
    public EqupmentSlot[] equipments;

    void OnValidate()
    {
        if(equipmentSlotParent != null)
        {
            equipments = equipmentSlotParent.GetComponentsInChildren<EqupmentSlot>(); 
        }
    }

    public bool EquipItem(EqupmentSO equipment, out EqupmentSO previousEquipment)
    {
        for(int i = 0; i < equipments.Length; i++)
        {
            if(equipments[i].equipmentType == equipment.equipmentType)
            {
                previousEquipment = (EqupmentSO)equipments[i].itemSO;
                equipments[i].itemSO = equipment;
                return true;
            }
        }
        previousEquipment = null;
        return false;
    }

    public bool UnequipItem(EqupmentSO equipment)
    {
        for(int i = 0 ; i< equipments.Length; i++)
        {
            if(equipments[i].itemSO == equipment)
            {
                equipments[i].itemSO = null;
                return true;
            }
        }
        return false;
    }
}
