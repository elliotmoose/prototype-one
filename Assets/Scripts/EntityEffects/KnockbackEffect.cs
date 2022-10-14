using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : EntityEffect
{
  private Vector3 _initial;
  private Vector3 _final;
  private Vector3 _direction;

  public KnockbackEffect(Entity _targeterEntity, Entity _targetedEntity, Vector3 explosionOrigin, float distance, float time) : base(_targeterEntity, _targetedEntity)
  {
    this.duration = time;
    this.name = "KNOCKBACK_EFFECT";

    _initial = _targetedEntity.transform.position;
    _direction = _targetedEntity.transform.position - explosionOrigin;


    Vector3 targetFinal = _initial + _direction.normalized * distance;
    Vector3 constrainedFinal = new Vector3(targetFinal.x, _initial.y, targetFinal.z);
    _final = constrainedFinal;
  }

  //returns a quadratic function that eases the input from 0 -> 1 
  float EaseOutQuadratic(float x)
  {
    float y = 1 - Mathf.Pow(x - 1, 2);
    return y;
  }

  //returns a circle function that eases the input from 0 -> 1 
  //\sqrt{\left(1-\left(x-1\right)^{2}\right)}
  float EaseOutCircular(float x)
  {
    float y = Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
    return y;
  }

  public override void UpdateEffect()
  {

    float progress = age / duration;
    _targetedEntity.transform.position = Vector3.Lerp(_initial, _final, EaseOutCircular(progress));
  }

  public override void OnEffectApplied()
  {
    _targetedEntity.SetDisabled(true);
  }

  public override void OnEffectEnd()
  {
    _targetedEntity.SetDisabled(false);
  }
}