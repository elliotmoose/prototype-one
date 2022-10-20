using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFilterEffect : EntityEffect
{

  public DamageType triggerDamageType = DamageType.NORMAL;
  public float damageMultiplier;

  public DamageFilterEffect(Entity _targeterEntity, Entity _targetedEntity, DamageType triggerDamageType, float damageMultiplier) : base(_targeterEntity, _targetedEntity)
  {
    this.name = "DAMAGE_FILTER_EFFECT";
    this.triggerDamageType = triggerDamageType;
    this.damageMultiplier = damageMultiplier;
  }
}