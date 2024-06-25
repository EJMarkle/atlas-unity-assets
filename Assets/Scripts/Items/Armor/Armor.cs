using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArmor", menuName = "Items/Armor")]
public class Armor : BaseItem
{
    [Header("Armor Properties")]
    public ArmorType armorType;
    public float defensePower;
    public float resistance; // Magical resistance
    public float weight; // Weight of the armor
    public float movementSpeedModifier; // Affects movement speed
}
