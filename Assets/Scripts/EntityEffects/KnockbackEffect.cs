using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : EntityEffect
{
  private Vector3 _direction;
  private float _speed;

  public KnockbackEffect(Entity _targeterEntity, Entity _targetedEntity, Vector3 origin, float distance, float time) : base(_targeterEntity, _targetedEntity)
  {
    this.duration = time;
    this.name = "KNOCKBACK_EFFECT";

    Vector3 _initial = _targetedEntity.transform.position;
    _direction = (_targetedEntity.transform.position - origin).normalized;
    _speed = distance / duration;
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

  public override void FixedUpdateEffect()
  {
    // for position to change quadratically
    // speed coefficient must change linearly
    // speed decreases from 2 -> 0, which averages out at 1
    float coefficient = Mathf.Clamp(1 - age / duration, 0, 1) * 2;
    _targetedEntity.GetComponent<CharacterController>().Move(_direction * Time.fixedDeltaTime * coefficient * _speed);
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