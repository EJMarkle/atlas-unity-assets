using UnityEngine;
using UnityEditor;
using System.IO;

public class DatabasesManager : EditorWindow
{
    [MenuItem("Window/Item Manager/Databases Manager")]
    public static void ShowWindow()
    {
        GetWindow<DatabasesManager>("Databases Manager");
    }

    private void OnGUI()
    {
        // Title
        EditorGUILayout.LabelField("Item Database Manager", EditorStyles.boldLabel);

        // Description
        EditorGUILayout.HelpBox(
            "Welcome to the Item Database Manager! Here, you can access and manage different databases related to weapons, potions, armors, and other items.",
            MessageType.Info);

        // Information about overall data
        GUILayout.Label("Overall Statistics:", EditorStyles.boldLabel);
        GUILayout.Label($"Total Items: {AssetDatabase.FindAssets("t:BaseItem").Length}");
        GUILayout.Label($"Total Weapons: {AssetDatabase.FindAssets("t:Weapon").Length}");
        GUILayout.Label($"Total Potions: {AssetDatabase.FindAssets("t:Potion").Length}");
        GUILayout.Label($"Total Armors: {AssetDatabase.FindAssets("t:Armor").Length}");
        

        // Separator
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Space();

        // Buttons to access other windows
        // if (GUILayout.Button("Weapon Database")) WeaponDatabase.ShowWindow();
        // if (GUILayout.Button("Potion Database")) PotionDatabase.ShowWindow();
        // if (GUILayout.Button("Armor Database")) ArmorDatabase.ShowWindow();
    }
}