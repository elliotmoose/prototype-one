using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEffect : EntityEffect
{
  GameObject burnParticleEffectPrefab;
  public GameObject burnParticleEffectReference;
  public float damage = 0;

  // override cooldown = 20;

  public BurnEffect(float duration, float damage, GameObject burnParticleEffectPrefab, Entity _targeterEntity, Entity _targetedEntity) : base(_targeterEntity, _targetedEntity, true)
  {
    this.duration = duration;
    this.damage = damage;
    this.burnParticleEffectPrefab = burnParticleEffectPrefab;
    this.name = "BURN_EFFECT";
  }

  public override void OnEffectApplied()
  {
    burnParticleEffectReference = GameObject.Instantiate(burnParticleEffectPrefab, _targetedEntity.transform.position, _targetedEntity.transform.rotation, _targetedEntity.transform);
    // _targetedEntity.SetMovementSpeed(movementSpeed+ movementSpeedIncrease);
  }

  public override void OnEffectReapplied(EntityEffect oldEffect)
  {
    BurnEffect oldBurnEffect = oldEffect as BurnEffect;
    burnParticleEffectReference = oldBurnEffect.burnParticleEffectReference;
    // burnParticleEffectReference = GameObject.Instantiate(burnParticleEffectPrefab, _targetedEntity.transform.position, _targetedEntity.transform.rotation, _targetedEntity.transform);		
    // _targetedEntity.SetMovementSpeed(movementSpeed+ movementSpeedIncrease);
  }

  public override void FixedUpdateEffect()
  {
    _targetedEntity.TakeDamage(new TakeDamageInfo(this._targeterEntity, this._targetedEntity, damage * Time.deltaTime, DamageType.ANTIVIRUS));
  }

  public override void OnEffectEnd()
  {
    GameObject.Destroy(burnParticleEffectReference);
    // _targetedEntity.SetMovementSpeed(movementSpeed - movementSpeedIncrease);
  }
}