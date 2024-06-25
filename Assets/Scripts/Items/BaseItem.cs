using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/BaseItem")]
public abstract class BaseItem : ScriptableObject
{
    [Header("General Properties")]
    public string itemName;
    public Sprite icon;
    public string description;
    public float baseValue;
    public Rarity rarity;
    public int requiredLevel; // Required level to use the item
    public EquipSlot equipSlot; // Where the item can be equipped

}

