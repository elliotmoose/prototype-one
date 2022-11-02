using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


enum ModifierTypes
{
  Lifesteal,
  Knockback
}

[CustomEditor(typeof(WeaponData))]
public class WeaponDataEditor : Editor
{
  int selected = 0;
  ModifierTypes selectedModifierType = ModifierTypes.Lifesteal;

  void RenderGUIForLifestealModifier(LifestealModifier modifier)
  {
    modifier.lifestealFactor = EditorGUILayout.FloatField("Lifesteal Percent", modifier.lifestealFactor);
  }

  void RenderGUIForKnockbackModifier(KnockbackModifier modifier)
  {
    modifier.knockbackDistance = EditorGUILayout.FloatField("Knockback Distance", modifier.knockbackDistance);
    modifier.knockbackDuration = EditorGUILayout.FloatField("Knockback Distance", modifier.knockbackDuration);
  }

  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();
    serializedObject.Update();
    // EditorGUI.DropdownButton(new Rect(0, 0, 100, 100), new GUIContent("test"), FocusType.Passive);

    WeaponData weaponData = (WeaponData)target;
    foreach (var modifier in weaponData.onHitModifiers)
    {
      if (modifier is LifestealModifier)
      {
        RenderGUIForLifestealModifier(modifier as LifestealModifier);
      }

      if (modifier is KnockbackModifier)
      {
        RenderGUIForKnockbackModifier(modifier as KnockbackModifier);
      }
    }

    List<string> modifierTypes = new List<string>();
    foreach (var modifierType in System.Enum.GetValues(typeof(ModifierTypes)))
    {
      modifierTypes.Add(modifierType.ToString());
    }

    selected = EditorGUILayout.Popup("Modifier Type", ((int)selectedModifierType), modifierTypes.ToArray());
    selectedModifierType = (ModifierTypes)selected;

    // editor button to add modifier
    if (GUILayout.Button("Add Modifier"))
    {
      switch (selectedModifierType)
      {
        case ModifierTypes.Lifesteal:
          weaponData.onHitModifiers.Add(new LifestealModifier());
          break;
        case ModifierTypes.Knockback:
          weaponData.onHitModifiers.Add(new KnockbackModifier());
          break;
      }
    }


    serializedObject.ApplyModifiedProperties();
  }
}
