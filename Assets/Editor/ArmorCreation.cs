using UnityEditor;
using UnityEngine;

public class ArmorCreation : BaseItemCreation<Armor>
{
    private ArmorType armorType;
    private float defensePower;
    private float resistance;
    private float weight;
    private float movementSpeedModifier;
    private EquipSlot equipSlot;

    public static new void ShowWindow()
    {
        GetWindow<ArmorCreation>("Create New Armor");
    }

    private void OnGUI()
    {
        // Draw common fields
        DrawCommonFields();

        // Draw armor-specific fields
        DrawArmorFields();

        if (GUILayout.Button("Create Armor"))
        {
            Armor newArmor = CreateInstance<Armor>();
            newArmor.armorType = armorType;
            newArmor.defensePower = defensePower;
            newArmor.resistance = resistance;
            newArmor.weight = weight;
            newArmor.movementSpeedModifier = movementSpeedModifier;
            newArmor.equipSlot = equipSlot;

            CreateItem(newArmor);
        }
    }

    void DrawArmorFields()
    {
        EditorGUILayout.LabelField("Armor Properties", EditorStyles.boldLabel);

        armorType = (ArmorType)EditorGUILayout.EnumPopup("Armor Type", armorType);
        defensePower = EditorGUILayout.FloatField("Defense Power", defensePower);
        resistance = EditorGUILayout.FloatField("Resistance", resistance);
        weight = EditorGUILayout.FloatField("Weight", weight);
        movementSpeedModifier = EditorGUILayout.FloatField("Movement Speed Modifier", movementSpeedModifier);
        equipSlot = (EquipSlot)EditorGUILayout.EnumPopup("Equip Slot:", equipSlot);
    }
}