using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class BaseItemCreation<T> : EditorWindow where T : BaseItem
{
    protected string itemName;
    protected Sprite icon;
    protected string description;
    protected float baseValue;
    protected int requiredLevel;
    protected Rarity rarity;
    
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BaseItemCreation<T>), true, "Create New Item");
    }

    protected void DrawCommonFields()
    {
        itemName = EditorGUILayout.TextField("Name:", itemName);
        icon = (Sprite)EditorGUILayout.ObjectField("Icon:", icon, typeof(Sprite), false);
        description = EditorGUILayout.TextField("Description:", description);
        baseValue = EditorGUILayout.FloatField("Base Value:", baseValue);
        requiredLevel = EditorGUILayout.IntField("Required Level:", requiredLevel);
        rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity:", rarity);
    }

    protected void CreateItem<T>() where T : BaseItem
    {
        T newItem = CreateInstance<T>();

        // Determine the folder path based on the type of item
        string folderPath = "Assets/Items/";
        if (typeof(T) == typeof(Weapon))
        {
            folderPath += "Weapons/";
        }
        else if (typeof(T) == typeof(Armor))
        {
            folderPath += "Armor/";
        }
        else if (typeof(T) == typeof(Potion))
        {
            folderPath += "Potions/";
        }

        // Create the directory if it doesn't exist
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            System.IO.Directory.CreateDirectory(Application.dataPath + folderPath.Substring("Assets".Length));
            AssetDatabase.Refresh();
        }

        // Define the full path for the asset
        string fullPath = folderPath + itemName + ".asset";
        fullPath = AssetDatabase.GenerateUniqueAssetPath(fullPath);

        AssetDatabase.CreateAsset(newItem, fullPath);
        
        newItem.itemName = itemName;
        newItem.baseValue = baseValue;
        newItem.rarity = rarity;
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}
