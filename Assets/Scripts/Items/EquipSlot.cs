using System;

[Flags]
[Serializable]
public enum EquipSlot
{
    Null = 0,
    Head = 1 << 0,
    Chest = 1 << 1,
    Legs = 1 << 2,
    MainHand = 1 << 3,
    OffHand = 1 << 4,
    TwoHand = 1 << 5,
    Accessory = 1 << 6
}