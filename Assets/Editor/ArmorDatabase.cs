using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ArmorDatabase : ItemDatabase<Armor>
{
    private List<Armor> armorsList = new List<Armor>();

    public static void ShowWindow()
    {
        GetWindow<ArmorDatabase>("Armor Database");
    }

    protected override void DrawItemList()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);

        GUILayout.Label("Armors List", EditorStyles.boldLabel);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Load armors from folder
        LoadArmors();

        foreach (var armor in armorsList)
        {
            // Check if the current armor is selected
            bool isSelected = (selectedItem == armor);

            // Display button with the armor's name, highlighting if selected
            if (GUILayout.Button(armor.itemName, isSelected ? GUI.skin.box : GUI.skin.button, GUILayout.ExpandWidth(true)))
            {
                selectedItem = armor;
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    protected override void DrawPropertiesSection()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label("Properties Section:", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (selectedItem != null && selectedItem is Armor)
        {
            Armor armor = selectedItem as Armor;

            EditorGUILayout.LabelField("Name:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(selectedItem.itemName);
            EditorGUILayout.Space();

            armor.description = EditorGUILayout.TextField("Description: ", armor.description);
            armor.baseValue = EditorGUILayout.FloatField("Base Value: ", armor.baseValue);
            armor.requiredLevel = EditorGUILayout.IntField("Required Level: ", armor.requiredLevel);
            armor.rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity: ", armor.rarity);
            EditorGUILayout.Space();

            GUILayout.Label("Armor Properties:", EditorStyles.boldLabel);
            armor.armorType = (ArmorType)EditorGUILayout.EnumPopup("Armor Type: ", armor.armorType);
            armor.defensePower = EditorGUILayout.FloatField("Defense Power: ", armor.defensePower);
            armor.resistance = EditorGUILayout.FloatField("Resistance: ", armor.resistance);
            armor.weight = EditorGUILayout.FloatField("Weight: ", armor.weight);
            armor.movementSpeedModifier = EditorGUILayout.FloatField("Movement Speed Modifier: ", armor.movementSpeedModifier);
            armor.equipSlot = (EquipSlot)EditorGUILayout.EnumPopup("Equip Slot: ", armor.equipSlot);
        }
        else
        {
            EditorGUILayout.LabelField("No armor selected");
        }

        EditorGUILayout.EndVertical();
    }

    private void LoadArmors()
    {
        armorsList.Clear();

        string folderPath = "Assets/Items/Armors";
        string[] guids = AssetDatabase.FindAssets("t:Armor", new[] { folderPath });

        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Armor armor = AssetDatabase.LoadAssetAtPath<Armor>(path);
            if (armor != null)
            {
                armorsList.Add(armor);
            }
        }
    }

    protected override void ExportItemsToCSV()
    {
        string filePath = EditorUtility.SaveFilePanel("Export Armors to CSV", "", "armors.csv", "csv");
        if (string.IsNullOrEmpty(filePath))
            return;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Item Name,Icon Path,Armor Type,Defense Power,Resistance,Weight,Movement Speed Modifier,Base Value,Rarity,Required Level,Equip Slot,Description");

        foreach (var armor in armorsList)
        {
            sb.AppendLine($"{armor.itemName},,{armor.armorType},{armor.defensePower},{armor.resistance},{armor.weight},{armor.movementSpeedModifier},{armor.baseValue},{armor.rarity},{armor.requiredLevel},{armor.equipSlot},{armor.description}");
        }

        File.WriteAllText(filePath, sb.ToString());
        AssetDatabase.Refresh();
        Debug.Log("Armors exported to CSV successfully.");
    }

    protected override void ImportItemsFromCSV()
    {
        string filePath = EditorUtility.OpenFilePanel("Import Armors from CSV", "", "csv");
        if (string.IsNullOrEmpty(filePath))
            return;

        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length <= 1)
        {
            Debug.LogError("CSV file is empty or does not contain valid data.");
            return;
        }

        armorsList.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            if (values.Length != 12)
            {
                Debug.LogError($"Line {i + 1} is malformed and will be skipped.");
                continue;
            }

            Armor armor = ScriptableObject.CreateInstance<Armor>();
            armor.itemName = values[0];
            // Skipping Icon Path (values[1]) assuming it's not necessary for the import
            armor.armorType = (ArmorType)System.Enum.Parse(typeof(ArmorType), values[2]);
            armor.defensePower = float.Parse(values[3]);
            armor.resistance = float.Parse(values[4]);
            armor.weight = float.Parse(values[5]);
            armor.movementSpeedModifier = float.Parse(values[6]);
            armor.baseValue = float.Parse(values[7]);
            armor.rarity = (Rarity)System.Enum.Parse(typeof(Rarity), values[8]);
            armor.requiredLevel = int.Parse(values[9]);
            armor.equipSlot = (EquipSlot)System.Enum.Parse(typeof(EquipSlot), values[10]);
            armor.description = values[11];

            armorsList.Add(armor);

            // Save the newly created Armor object as an asset
            string assetPath = $"Assets/Items/Armors/{armor.itemName}.asset";
            AssetDatabase.CreateAsset(armor, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Armors imported from CSV successfully.");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Armor Database", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        // Left panel (top and bottom squares)
        EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.5f));

        // Top square (options and create new armor)
        EditorGUILayout.BeginVertical(GUI.skin.box);
        DrawTopLeftOptions();
        if (GUILayout.Button("Create New Armor"))
        {
            ArmorCreation.ShowWindow();
        }
        EditorGUILayout.EndVertical();

        // Bottom square (armors list)
        EditorGUILayout.BeginVertical(GUI.skin.box);
        DrawItemList();
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();

        // Right panel (tall rectangle for properties)
        EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(position.width * 0.5f));
        DrawPropertiesSection();
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
    }
}
