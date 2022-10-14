using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedEffect : EntityEffect
{
  public float movementSpeedIncrease = 0;
  public float percentageIncrease = 0;

  // override cooldown = 20;


  /// <summary>
  /// 
  /// </summary>
  /// <param name="_targetedEntity"></param>
  /// <param name="movementSpeedDeltaFactor">0.5 means a 50% INCREASE in ms</param>
  /// <param name="duration"></param>
  /// <returns></returns>
  public MovementSpeedEffect(Entity _targeterEntity, Entity _targetedEntity, float movementSpeedDeltaFactor, float duration) : base(_targeterEntity, _targetedEntity)
  {
    this.percentageIncrease = movementSpeedDeltaFactor;
    this.duration = duration;
    this.name = "MOVEMENT_SPEED_EFFECT";
  }

  public override void OnEffectApplied()
  {
    //compute change in movement speed
    float targetMovementSpeed = _targetedEntity.GetMovementSpeed();
    movementSpeedIncrease = targetMovementSpeed * percentageIncrease;

    _targetedEntity.SetMovementSpeed(targetMovementSpeed + movementSpeedIncrease);
  }

  public override void OnEffectEnd()
  {
    //undo movement speed difference
    float movementSpeed = _targetedEntity.GetMovementSpeed();
    _targetedEntity.SetMovementSpeed(movementSpeed - movementSpeedIncrease);
  }
}