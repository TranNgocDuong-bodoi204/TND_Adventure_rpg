"README.md": """
# Unity Inventory System

## Overview

This package includes a lightweight and modular inventory system for Unity using C#.

- ScriptableObject-based items
- Dynamic item addition/removal
- UI toggle with keypress (default: I)
- Easy integration and customization

## How to Use

1. Import `Scripts/Inventory` into your Unity project's Assets folder.
2. Create UI with item slots (e.g. using a GridLayoutGroup) and link it to the `InventoryUI.cs` script.
3. Create item assets via `Create > Inventory > Item` in the Unity editor.
4. Use `InventoryManager.Instance.AddItem(item)` to add items at runtime.

## Requirements

- Unity 2021+ (but compatible with earlier versions)
- UnityEngine.UI (for the UI components)

This inventory system is a solid starting point for RPGs, survival games, or item collection mechanics.

---

Created for Unity developers looking for a simple but expandable inventory solution.
"""