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

    protected bool IsDuplicateName(string name, string folderPath)
    {
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name} {name}", new[] { folderPath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            T existingItem = AssetDatabase.LoadAssetAtPath<T>(path);
            if (existingItem != null && existingItem.itemName == name)
            {
                return true;
            }
        }
        return false;
    }

    protected void CreateItem(T newItem)
    {
        // Determine the folder path based on the type of item
        string folderPath = "Assets/Items/";
        if (typeof(T) == typeof(Weapon))
        {
            folderPath += "Weapons/";
        }
        else if (typeof(T) == typeof(Armor))
        {
            folderPath += "Armors/";
        }
        else if (typeof(T) == typeof(Potion))
        {
            folderPath += "Potions/";
        }

        // Check for duplicate names
        if (IsDuplicateName(itemName, folderPath))
        {
            EditorUtility.DisplayDialog("Error", "An item with this name already exists!", "OK");
            return;
        }

        // Assign entered values to the item fields
        newItem.itemName = itemName;
        newItem.icon = icon;
        newItem.description = description;
        newItem.baseValue = baseValue;
        newItem.requiredLevel = requiredLevel;
        newItem.rarity = rarity;

        EditorUtility.SetDirty(newItem);

        // Define the full path for the asset
        string fullPath = folderPath + itemName + ".asset";
        fullPath = AssetDatabase.GenerateUniqueAssetPath(fullPath);

        AssetDatabase.CreateAsset(newItem, fullPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}