using UnityEditor;
using UnityEngine;

public class PotionDatabase : ItemDatabase<Potion>
{
    [MenuItem("Window/Item Manager/Potion Database")]
    public static void ShowWindow()
    {
        GetWindow<PotionDatabase>("Potion Database");
    }

    protected override void DrawItemList()
    {
        // Implementation for drawing the list of potion items
        GUILayout.Label("Potion List", EditorStyles.boldLabel);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(position.width - propertiesSectionWidth), GUILayout.Height(position.height));

        GUILayout.EndScrollView();
    }

    protected override void DrawPropertiesSection()
    {
        // Implementation for drawing the properties of the selected potion
        if (selectedItem != null)
        {
            GUILayout.Label("Potion Properties", EditorStyles.boldLabel);
            // Display and edit properties of selectedItem (cast to Potion)
        }
    }

    protected override void ExportItemsToCSV()
    {
        // 
    }

    protected override void ImportItemsFromCSV()
    {
        // 
    }
}
