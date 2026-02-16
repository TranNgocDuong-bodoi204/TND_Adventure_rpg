using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Inventory inventory;
    public Equipment equipment;

    void OnEnable()
    {
        ItemSlot.onItemClicked += EquipItem;
    }
    void OnDisable()
    {
        ItemSlot.onItemClicked -= EquipItem;
    }

    private void EquipItem(ItemSO item)
    {
        Debug.Log($"EquipItem called with {item.itemName}");
        if(item is EqupmentSO equipmentItem)
        {
            Equip(equipmentItem);
        }
    }

    public void Equip(EqupmentSO equipment)
    {
        if(inventory.RemoveItem(equipment))
        {
            if(this.equipment.EquipItem(equipment, out EqupmentSO previousEquipment))
            {
                if(previousEquipment != null)
                {
                    inventory.AddItem(previousEquipment);
                }
            }
            else
            {
                inventory.AddItem(equipment);
            }
        }
    }
    public void UnEquip(EqupmentSO equpment)
    {
        if(equipment.UnequipItem(equpment) && !inventory.IsFull())
        {
            inventory.AddItem(equpment);
        }
    }
}
