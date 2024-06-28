using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class WeaponDatabase : ItemDatabase<Weapon>
{
    private List<Weapon> weaponsList = new List<Weapon>();
    private string searchQuery = "";
    private WeaponType? selectedWeaponType = null;
    private EquipSlot? selectedEquipSlot = null;
    private Rarity? selectedRarity = null;
    private int itemsPerPage = 10;
    private int currentPage = 1;
    private int totalPages = 0;

    public static void ShowWindow()
    {
        GetWindow<WeaponDatabase>("Weapon Database");
    }

    protected override void DrawItemList()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);

        GUILayout.Label("Weapons List", EditorStyles.boldLabel);

        // Search Bar
        searchQuery = EditorGUILayout.TextField("Search", searchQuery);

        // Filters
        selectedWeaponType = (WeaponType?)EditorGUILayout.EnumPopup("Weapon Type", selectedWeaponType ?? WeaponType.None);
        selectedEquipSlot = (EquipSlot?)EditorGUILayout.EnumPopup("Equip Slot", selectedEquipSlot ?? EquipSlot.None);
        selectedRarity = (Rarity?)EditorGUILayout.EnumPopup("Rarity", selectedRarity ?? Rarity.Common);

        // Clear Filters button
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear Filters"))
        {
            searchQuery = "";
            selectedWeaponType = null;
            selectedEquipSlot = null;
            selectedRarity = null;
            GUI.FocusControl(null); // Clear focus to ensure search text field updates visually
        }
        EditorGUILayout.EndHorizontal();

        // Items per Page control
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Items Per Page:");
        int newItemsPerPage = EditorGUILayout.IntField(itemsPerPage);
        if (newItemsPerPage != itemsPerPage)
        {
            itemsPerPage = Mathf.Max(1, newItemsPerPage);
            currentPage = 1;
            Repaint();
        }
        EditorGUILayout.EndHorizontal();

        // Pagination controls
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("First"))
        {
            currentPage = 1;
        }

        if (GUILayout.Button("Previous"))
        {
            currentPage = Mathf.Max(1, currentPage - 1);
        }

        EditorGUILayout.LabelField("Page " + currentPage + " of " + totalPages);

        if (GUILayout.Button("Next"))
        {
            currentPage = Mathf.Min(totalPages, currentPage + 1);
        }

        if (GUILayout.Button("Last"))
        {
            currentPage = totalPages;
        }

        EditorGUILayout.EndHorizontal();

        // Load weapons from folder
        LoadWeapons();

        // Count of items displayed
        int startIndex = (currentPage - 1) * itemsPerPage;

        // Apply search and filters
        List<Weapon> filteredWeapons = weaponsList.FindAll(IsWeaponMatch);

        bool foundWeapons = false;
        int itemsDisplayed = 0;

        for (int i = startIndex; i < filteredWeapons.Count; i++)
        {
            var weapon = filteredWeapons[i];

            foundWeapons = true;
            itemsDisplayed++;

            bool isSelected = (selectedItem == weapon);

            if (GUILayout.Button(weapon.itemName, isSelected ? GUI.skin.box : GUI.skin.button, GUILayout.ExpandWidth(true)))
            {
                selectedItem = weapon;
            }

            if (itemsDisplayed >= itemsPerPage)
            {
                break;
            }
        }

        totalPages = Mathf.CeilToInt((float)filteredWeapons.Count / itemsPerPage);

        EditorGUILayout.EndVertical();

        if (!foundWeapons)
        {
            EditorGUILayout.HelpBox("No weapons meet your criteria.", MessageType.Info);
        }

        string itemCountText = "Showing " + itemsDisplayed + " item";
        if (itemsDisplayed != 1)
        {
            itemCountText += "s";
        }
        EditorGUILayout.LabelField(itemCountText);
    }

    private bool IsWeaponMatch(Weapon weapon)
    {
        if (!string.IsNullOrEmpty(searchQuery) && !weapon.itemName.ToLower().Contains(searchQuery.ToLower()))
        {
            return false;
        }

        if (selectedWeaponType.HasValue && weapon.weaponType != selectedWeaponType.Value)
        {
            return false;
        }

        if (selectedEquipSlot.HasValue && weapon.equipSlot != selectedEquipSlot.Value)
        {
            return false;
        }

        if (selectedRarity.HasValue && weapon.rarity != selectedRarity.Value)
        {
            return false;
        }

        return true;
    }

    protected override void DrawPropertiesSection()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label("Properties Section:", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (selectedItem != null && selectedItem is Weapon)
        {
            Weapon weapon = selectedItem as Weapon;

            EditorGUILayout.LabelField("Name:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(selectedItem.itemName);
            EditorGUILayout.Space();

            weapon.description = EditorGUILayout.TextField("Description: ", weapon.description);
            weapon.baseValue = EditorGUILayout.FloatField("Base Value: ", weapon.baseValue);
            weapon.requiredLevel = EditorGUILayout.IntField("Required Level: ", weapon.requiredLevel);
            weapon.rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity: ", weapon.rarity, GUILayout.Width(EditorGUIUtility.labelWidth + 100));
            EditorGUILayout.Space();

            GUILayout.Label("Weapon Properties:", EditorStyles.boldLabel);
            weapon.weaponType = (WeaponType)EditorGUILayout.EnumPopup("Weapon Type: ", weapon.weaponType, GUILayout.Width(EditorGUIUtility.labelWidth + 100));
            weapon.attackPower = EditorGUILayout.IntField("Attack Power: ", (int)weapon.attackPower);
            weapon.attackSpeed = EditorGUILayout.FloatField("Attack Speed: ", weapon.attackSpeed);
            weapon.durability = EditorGUILayout.FloatField("Durability: ", weapon.durability);
            weapon.range = EditorGUILayout.FloatField("Range: ", weapon.range);
            weapon.criticalHitChance = EditorGUILayout.FloatField("Critical Hit Chance: ", weapon.criticalHitChance);
            weapon.equipSlot = (EquipSlot)EditorGUILayout.EnumPopup("Equip Slot: ", weapon.equipSlot, GUILayout.Width(EditorGUIUtility.labelWidth + 100));

            // Display and edit iconPath
            weapon.iconPath = EditorGUILayout.TextField("Icon Path: ", weapon.iconPath);
        }
        else
        {
            EditorGUILayout.LabelField("No weapon selected");
        }

        EditorGUILayout.EndVertical();
    }

    private void LoadWeapons()
    {
        weaponsList.Clear();

        string folderPath = "Assets/Items/Weapons";
        string[] guids = AssetDatabase.FindAssets("t:Weapon", new[] { folderPath });

        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Weapon weapon = AssetDatabase.LoadAssetAtPath<Weapon>(path);
            if (weapon != null)
            {
                weaponsList.Add(weapon);
            }
        }
    }

    protected override void ExportItemsToCSV()
    {
        string filePath = EditorUtility.SaveFilePanel("Save Weapons to CSV", "", "Weapons.csv", "csv");

        if (string.IsNullOrEmpty(filePath))
            return;

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Name,Description,BaseValue,RequiredLevel,Rarity,WeaponType,AttackPower,AttackSpeed,Durability,Range,CriticalHitChance,EquipSlot,IconPath");

            foreach (var weapon in weaponsList)
            {
                writer.WriteLine($"{weapon.itemName},{weapon.description},{weapon.baseValue},{weapon.requiredLevel},{weapon.rarity},{weapon.weaponType},{weapon.attackPower},{weapon.attackSpeed},{weapon.durability},{weapon.range},{weapon.criticalHitChance},{weapon.equipSlot},{weapon.iconPath}");
            }
        }

        Debug.Log("Weapons exported to CSV");
    }

    protected override void ImportItemsFromCSV()
    {
        string filePath = EditorUtility.OpenFilePanel("Import Weapons from CSV", "", "csv");

        if (string.IsNullOrEmpty(filePath))
            return;

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                bool headerRead = false;

                while ((line = reader.ReadLine()) != null)
                {
                    if (!headerRead)
                    {
                        headerRead = true;
                        continue;
                    }

                    string[] data = line.Split(',');

                    // Ensure correct number of fields
                    if (data.Length != 13)
                    {
                        Debug.LogError($"Error: Incorrect number of fields in CSV line. Line: {line}");
                        continue;
                    }

                    // Add error handling for parsing
                    try
                    {
                        Weapon weapon = ScriptableObject.CreateInstance<Weapon>();
                        weapon.itemName = data[0];
                        weapon.iconPath = data[1];
                        weapon.description = data[2];
                        weapon.baseValue = float.Parse(data[3]);
                        weapon.requiredLevel = int.Parse(data[4]);
                        weapon.rarity = (Rarity)Enum.Parse(typeof(Rarity), data[5]);
                        weapon.weaponType = (WeaponType)Enum.Parse(typeof(WeaponType), data[6]);
                        weapon.attackPower = float.Parse(data[7]);
                        weapon.attackSpeed = float.Parse(data[8]);
                        weapon.durability = float.Parse(data[9]);
                        weapon.range = float.Parse(data[10]);
                        weapon.criticalHitChance = float.Parse(data[11]);
                        weapon.equipSlot = (EquipSlot)Enum.Parse(typeof(EquipSlot), data[12]);

                        // Optionally validate other fields like description, iconPath, etc.

                        string assetPath = $"Assets/Items/Weapons/{weapon.itemName}.asset";
                        AssetDatabase.CreateAsset(weapon, assetPath);
                        weaponsList.Add(weapon);
                    }
                    catch (FormatException e)
                    {
                        Debug.LogError($"Error parsing data from CSV: {e.Message}. Line: {line}");
                        // Optionally handle or skip this line
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error processing data from CSV: {e.Message}. Line: {line}");
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("Weapons imported from CSV");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error reading file: {e.Message}");
        }
    }


    private void OnGUI()
    {
        EditorGUILayout.LabelField("Weapon Database", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.5f));

        EditorGUILayout.BeginVertical(GUI.skin.box);
        DrawTopLeftOptions();
        if (GUILayout.Button("Create New Weapon"))
        {
            WeaponCreation.ShowWindow();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        DrawItemList();
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(position.width * 0.5f));
        DrawPropertiesSection();
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
    }
}
