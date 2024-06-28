using UnityEditor;
using UnityEngine;

public class PotionCreation : BaseItemCreation<Potion>
{
    private PotionEffect potionEffect;
    private float effectPower;
    private float duration;
    private float cooldown;
    private bool isStackable;

    public static new void ShowWindow()
    {
        GetWindow<PotionCreation>("Create New Potion");
    }

    private void OnGUI()
    {
        // Draw common fields
        DrawCommonFields();

        // Draw potion-specific fields
        DrawPotionFields();

        if (GUILayout.Button("Create Potion"))
        {
            Potion newPotion = CreateInstance<Potion>();
            newPotion.potionEffect = potionEffect;
            newPotion.effectPower = effectPower;
            newPotion.duration = duration;
            newPotion.cooldown = cooldown;
            newPotion.isStackable = isStackable;

            CreateItem(newPotion);
        }
    }

    void DrawPotionFields()
    {
        EditorGUILayout.LabelField("Potion Properties", EditorStyles.boldLabel);

        potionEffect = (PotionEffect)EditorGUILayout.EnumPopup("Potion Effect", potionEffect);
        effectPower = EditorGUILayout.FloatField("Effect Power", effectPower);
        duration = EditorGUILayout.FloatField("Duration", duration);
        cooldown = EditorGUILayout.FloatField("Cooldown", cooldown);
        isStackable = EditorGUILayout.Toggle("Is Stackable", isStackable);
    }
}