using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum ItemRarity
{
  Common = 0,
  Rare = 1,
  Epic = 2,
  Legendary = 3,
  Ultimate = 4,
  Unique = 5,
}

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponData", order = 1)]
[System.Serializable]
public class WeaponData : ScriptableObject
{
  // affects which animation cycle,
  // affects which execution script to run for attack
  public WeaponType type;

  // optional
  public GameObject customPrefab;

  // try load prefab based on name
  public Object defaultPrefab()
  {
    Object weaponPrefab = Resources.Load($"Prefabs/Weapons/{this.type.ToString()}");
    Debug.Log($"Loading weapon: {weaponPrefab.name}");
    return weaponPrefab;
  }

  // damage per animation cycle
  public float[] damage;
  public float[] range;
  public ItemRarity rarity;

  [SerializeReference] public List<OnHitModifier> onHitModifiers = new List<OnHitModifier>();
}