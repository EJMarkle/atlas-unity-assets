using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatisticsPage : EditorWindow
{
    private List<Weapon> weaponsList = new List<Weapon>();
    private List<Armor> armorsList = new List<Armor>();
    private List<Potion> potionsList = new List<Potion>();

    public static void ShowWindow()
    {
        GetWindow<StatisticsPage>("Statistics Page");
    }

    private void OnEnable()
    {
        LoadItems();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Item Statistics", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        DrawGeneralStatistics();
        EditorGUILayout.Space();

        DrawWeaponStatistics();
        EditorGUILayout.Space();

        DrawArmorStatistics();
        EditorGUILayout.Space();

        DrawPotionStatistics();
    }

    private void LoadItems()
    {
        LoadWeapons();
        LoadArmors();
        LoadPotions();
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

    private void LoadPotions()
    {
        potionsList.Clear();

        string folderPath = "Assets/Items/Potions";
        string[] guids = AssetDatabase.FindAssets("t:Potion", new[] { folderPath });

        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Potion potion = AssetDatabase.LoadAssetAtPath<Potion>(path);
            if (potion != null)
            {
                potionsList.Add(potion);
            }
        }
    }

    private void DrawGeneralStatistics()
    {
        GUILayout.Label("General Statistics", EditorStyles.boldLabel);
        GUILayout.Label($"Total Weapons: {weaponsList.Count}");
        GUILayout.Label($"Total Armors: {armorsList.Count}");
        GUILayout.Label($"Total Potions: {potionsList.Count}");
        GUILayout.Label($"Total Items: {weaponsList.Count + armorsList.Count + potionsList.Count}");
    }

    private void DrawWeaponStatistics()
    {
        GUILayout.Label("Weapon Statistics", EditorStyles.boldLabel);

        if (weaponsList.Count > 0)
        {
            int totalAttackPower = 0;
            float totalAttackSpeed = 0f;

            foreach (var weapon in weaponsList)
            {
                totalAttackPower += (int)weapon.attackPower;
                totalAttackSpeed += weapon.attackSpeed;
                // Add more attributes as needed
            }

            float averageAttackPower = (float)totalAttackPower / weaponsList.Count;
            float averageAttackSpeed = totalAttackSpeed / weaponsList.Count;

            GUILayout.Label($"Average Attack Power: {averageAttackPower:F2}");
            GUILayout.Label($"Average Attack Speed: {averageAttackSpeed:F2}");
            // Add more average calculations here
        }
        else
        {
            GUILayout.Label("No weapons found.");
        }
    }

    private void DrawArmorStatistics()
    {
        GUILayout.Label("Armor Statistics", EditorStyles.boldLabel);

        if (armorsList.Count > 0)
        {
            float totalDefensePower = 0f;
            float totalResistance = 0f;

            foreach (var armor in armorsList)
            {
                totalDefensePower += armor.defensePower;
                totalResistance += armor.resistance;
                // Add more attributes as needed
            }

            float averageDefensePower = totalDefensePower / armorsList.Count;
            float averageResistance = totalResistance / armorsList.Count;

            GUILayout.Label($"Average Defense Power: {averageDefensePower:F2}");
            GUILayout.Label($"Average Resistance: {averageResistance:F2}");
            // Add more average calculations here
        }
        else
        {
            GUILayout.Label("No armors found.");
        }
    }

    private void DrawPotionStatistics()
    {
        GUILayout.Label("Potion Statistics", EditorStyles.boldLabel);

        if (potionsList.Count > 0)
        {
            float totalEffectPower = 0f;
            float totalDuration = 0f;

            foreach (var potion in potionsList)
            {
                totalEffectPower += potion.effectPower;
                totalDuration += potion.duration;
                // Add more attributes as needed
            }

            float averageEffectPower = totalEffectPower / potionsList.Count;
            float averageDuration = totalDuration / potionsList.Count;

            GUILayout.Label($"Average Effect Power: {averageEffectPower:F2}");
            GUILayout.Label($"Average Duration: {averageDuration:F2}");
            // Add more average calculations here
        }
        else
        {
            GUILayout.Label("No potions found.");
        }
    }
}