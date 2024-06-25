using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType
{
    None = 0,              // No armor equipped
    Leather = 1 << 0,       // Basic, lightweight armor that offers limited protection.
    Iron = 1 << 1,          // Heavy armor that offers good protection but limits mobility.
    Cloth = 1 << 2,         // Weak armor usually used by mages and archers for better mobility.
    Mail = 1 << 3,          // Strong armor offering a balance between protection and mobility.
    Bronze = 1 << 4,        // Slightly weaker than iron but lighter, often ornamental.
    Gold = 1 << 5,          // Very weak in battle, often used for ceremonial purposes.
    Scale = 1 << 6,         // Made of overlapping scales, offers good protection and some mobility.
    Dragonscale = 1 << 7,   // Exceptionally strong and light, made from the scales of dragons.
    Arcane = 1 << 8,        // Imbued with magical properties, enhances spellcasting abilities.
    Elfweave = 1 << 9,      // Light armor that is both strong and silent, made by elven crafters.
    Mithril = 1 << 10,      // Extremely light and strong, almost impervious to damage.
    Hide = 1 << 11,         // Made from animal hides, offers basic protection.
    Bone = 1 << 12,         // Crafted from bones, has mystical and physical properties.
    Obsidian = 1 << 13,     // Fragile but extremely sharp, offers high damage resistance against specific attacks.
    Crystal = 1 << 14,      // Magical armor that enhances energy but is weak physically.
    Runesteel = 1 << 15,    // Infused with runic magic, offers balanced protection and magic enhancement.
    Kevlar = 1 << 16,       // Modern armor type, offers high protection against projectiles.
    Titanium = 1 << 17,     // Exceptionally strong and lightweight, used in advanced armors.
    Cybernetic = 1 << 18,   // Futuristic armor with integrated technology and gadgets.
    Elemental = 1 << 19,     // Provides strong resistance against elemental attacks like fire, ice, etc.
    Wood = 1 << 20          // Well, it's wood.
}
