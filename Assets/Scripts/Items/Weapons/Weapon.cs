using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Items/Weapon")]
public class Weapon : BaseItem
{
    [Header("Weapon Properties")]
    public WeaponType weaponType;
    public float attackPower;
    public float attackSpeed;
    public float durability; // Durability of the weapon
    public float range; // Range of the weapon
    public float criticalHitChance; // Chance of a critical hit
}


