using UnityEditor;
using UnityEngine;

public class WeaponCreation : BaseItemCreation<Weapon>
{
    private WeaponType weaponType;
    private int attackPower;
    private float attackSpeed;
    private float durability;
    private float range;
    private float criticalHitChance;
    private EquipSlot equipSlot;

    public static new void ShowWindow()
    {
        GetWindow<WeaponCreation>("Create New Weapon");
    }

    private void OnGUI()
    {
        // Draw common fields
        DrawCommonFields();

        // Draw weapon-specific fields
        DrawWeaponFields();

        if (GUILayout.Button("Create Weapon"))
        {
            Weapon newWeapon = CreateInstance<Weapon>();
            newWeapon.weaponType = weaponType;
            newWeapon.attackPower = attackPower;
            newWeapon.attackSpeed = attackSpeed;
            newWeapon.durability = durability;
            newWeapon.range = range;
            newWeapon.criticalHitChance = criticalHitChance;
            newWeapon.equipSlot = equipSlot;

            CreateItem(newWeapon);
        }
    }

    void DrawWeaponFields()
    {
        EditorGUILayout.LabelField("Weapon Properties", EditorStyles.boldLabel);

        weaponType = (WeaponType)EditorGUILayout.EnumPopup("Weapon Type", weaponType);
        attackPower = (int)EditorGUILayout.FloatField("Attack Power", attackPower);
        attackSpeed = EditorGUILayout.FloatField("Attack Speed", attackSpeed);
        durability = EditorGUILayout.FloatField("Durability", durability);
        range = EditorGUILayout.FloatField("Range", range);
        criticalHitChance = EditorGUILayout.FloatField("Critical Hit Chance", criticalHitChance);
        equipSlot = (EquipSlot)EditorGUILayout.EnumPopup("Equip Slot:", equipSlot);
    }
}