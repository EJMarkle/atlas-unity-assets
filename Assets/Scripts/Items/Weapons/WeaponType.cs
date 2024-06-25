using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[Flags]
public enum WeaponType
{
    None = 0,              // Unarmed or no weapon equipped.
    Sword = 1 << 0,        // Versatile and balanced, good for slashing and stabbing.
    Bow = 1 << 1,          // Ranged weapon, requires arrows, best for long distances.
    Staff = 1 << 2,        // Often used by mages, can be both a melee and a magical weapon.
    Axe = 1 << 3,          // Heavy and powerful, great for dealing damage but often slow.
    Dagger = 1 << 4,       // Quick and lightweight, excellent for close-quarters combat.
    Improvised = 1 << 5,   // Makeshift weapons like clubs or rocks, generally weak but readily available.
    Blunt = 1 << 6,        // Includes hammers and clubs, good against armored foes.
    Mace = 1 << 7,         // Spiked or flanged weapon, offers a balance between blunt and piercing damage.
    Gun = 1 << 8,          // Modern ranged weapon, requires bullets, offers high damage.
    Crossbow = 1 << 9,     // Like a bow but with mechanized drawing, powerful but slow.
    Sling = 1 << 10,       // Simple ranged weapon, uses rocks or small projectiles.
    Spear = 1 << 11,       // Long reach and good for thrusting, can be thrown.
    Claws = 1 << 12,       // Natural or artificial, excellent for quick, slashing attacks.
    Arrow = 1 << 13,       // Ammunition for bows, can vary in type and quality.
    Whip = 1 << 14,        // Long, flexible weapon, good for disarming but low damage.
    Flail = 1 << 15,       // Ball-and-chain weapon, difficult to use but powerful.
    Wand = 1 << 16,        // Magical weapon, used for casting spells, generally weak in melee.
    Scythe = 1 << 17,      // Large, sweeping weapon, slow but deals heavy damage.
    Katana = 1 << 18,      // Fast, sharp, and lightweight, excels in quick, precise cuts.
    EnergyBlade = 1 << 19, // Futuristic or magical, extremely sharp and energy-based.
    Cursed = 1 << 20,      // Dark magic infused, powerful but often with a drawback.
    Explosive = 1 << 21,   // Includes grenades and dynamite, high area damage but risky.
    Halberd = 1 << 22,     // Polearm with an axe blade, offers reach and versatility.
    Boomerang = 1 << 23,   // Throwable weapon that returns, useful for hitting multiple targets.
    Psychic = 1 << 24,     // Requires mental power, deals magical or psychic damage.
    Exotic = 1 << 25,       // Rare and unusual, often with unique properties or abilities.
    Sickle = 1 << 26
}
