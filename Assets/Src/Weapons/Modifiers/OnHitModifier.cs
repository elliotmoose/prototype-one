using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class OnHitModifier
{
  abstract public void OnHit(Weapon weapon, TakeDamageInfo[] takeDamageInfos);
}