using UnityEngine;
using System.Collections;

/// <summary>
/// Modifier triggers when an attack is hit
/// </summary>
public abstract class OnHitModifier : ScriptableObject
{
  abstract public void OnHit(Weapon weapon, TakeDamageInfo[] takeDamageInfos);
}