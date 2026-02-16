using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/ItemSO")]
[Serializable]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string description;
}
