using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "Items/Potion")]
public class Potion : BaseItem
{
    [Header("Potion Properties")]
    public PotionEffect potionEffect;
    public float effectPower;
    public float duration; // Duration of the effect
    public float cooldown; // Cooldown time for using again
    public bool isStackable; // Whether the effect is stackable
}


