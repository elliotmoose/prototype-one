using UnityEngine;
using System.Collections;


[System.Serializable]
public class LifestealModifier : OnHitModifier
{
  public float lifestealFactor = 0.2f;
  public override void OnHit(Weapon weapon, TakeDamageInfo[] takeDamageInfos)
  {
    float heal = 0;
    foreach (var takeDamageInfo in takeDamageInfos)
    {
      heal += takeDamageInfo.effectiveDamage * lifestealFactor;
    }

    weapon.owner.Heal(heal);
  }
}