using UnityEngine;
using System.Collections.Generic;

public class TakeDamageInfo
{
  public Entity attacker;
  public Entity receiver;
  public float damage;
  public DamageType damageType;
  public bool isCritical;
  public TakeDamageInfo(Entity _attacker, Entity _receiver, float _damage, DamageType _damageType, bool _isCritical = false)
  {
    attacker = _attacker;
    receiver = _receiver;
    damage = _damage;
    damageType = _damageType;
    isCritical = _isCritical;
  }

  public float effectiveDamage
  {
    get
    {
      List<EntityEffect> effects = receiver.entityEffects.FindAll((EntityEffect el) => el.GetType() == typeof(DamageFilterEffect));

      float factor = 1;
      foreach (EntityEffect effect in effects)
      {
        DamageFilterEffect filter = effect as DamageFilterEffect;

        //damagetype none means applies to all damage types
        if (damageType == filter.triggerDamageType || filter.triggerDamageType == DamageType.NONE)
        {
          factor += (filter.damageMultiplier - 1);
        }
      }
      Debug.Log($"Damage amplified by factor {factor}");
      return damage * factor * (isCritical ? 2 : 1);

    }
  }
}