using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum PotionEffect
{
    None = 0,
    Heal = 1 << 0,
    ManaRestore = 1 << 1,
    BoostStrength = 1 << 2,
    BoostLuck = 1 << 3,
    BoostKnowledge = 1 << 4,
    Fire = 1 << 5,
    Smoke = 1 << 6,
    Invisibility = 1 << 7,
    Invincibility = 1 << 8,
    Poison = 1 << 9,
    Transformation = 1 << 10,
    Haste = 1 << 11,
    Teleportation = 1 << 12,
    Freeze = 1 << 13,
    Charm = 1 << 14,
    Shielding = 1 << 15,
    Confusion = 1 << 16,
    WaterBreathing = 1 << 17,
    NightVision = 1 << 18,
    Levitation = 1 << 19,
    Regeneration = 1 << 20,
    Petrification = 1 << 21,
    Berserk = 1 << 22,
    Silence = 1 << 23,
    Dispel = 1 << 24,
    Ethereal = 1 << 25,
    EnergyBoost = 1 << 26,
    LifeDrain = 1 << 27,
    Intangibility = 1 << 28,
    FeatherWeight = 1 << 29,
    MagnifySenses = 1 << 30,
    AntiMagic = 1 << 31
}