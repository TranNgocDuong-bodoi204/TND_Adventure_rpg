using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<ItemSO> items;
    [SerializeField] public Transform itemSlotParent;
    [SerializeField] public ItemSlot[] itemSlots;

    void OnValidate()
    {
        if(itemSlotParent != null)
        {
            itemSlots = itemSlotParent.GetComponentsInChildren<ItemSlot>(); 
        }
        UpdateInventoryUI();
    }

    public List<ItemSlot> GetItemSLots()
    {
        List<ItemSlot> slots = new List<ItemSlot>();
        for(int i = 0; i < items.Count && i < itemSlots.Length; i++)
        {
            slots.Add(itemSlots[i]);
        }
        return slots;
    }

    public bool AddItem(ItemSO item)
    {
        if(IsFull())
        {
            Debug.Log("Inventory Full");
            return false;
        }
        items.Add(item);
        UpdateInventoryUI();
        return true;
    }
    public bool IsFull() => items.Count >= itemSlots.Length;
    public bool RemoveItem(ItemSO item)
    {
        if(items.Contains(item))
        {
            items.Remove(item);
            UpdateInventoryUI();
            return true;
        }
        Debug.Log("Item not found in inventory");
        return false;
    }

    public void UpdateInventoryUI()
    {
        int i = 0;
        for(; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].itemSO = items[i];
        }
        
        for(; i < itemSlots.Length; i++)
        {
            itemSlots[i].itemSO = null;
        }
    }
}
