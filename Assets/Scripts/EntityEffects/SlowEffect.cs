using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : EntityEffect {
	public float movementSpeedIncrease = -3;	

	// override cooldown = 20;

	public SlowEffect(Entity _targetedEntity):base(_targetedEntity)
	{
		this.cooldown = 5;
		this.name = "SLOW_EFFECT";
	}

	public override void OnEffectApplied()
	{
		float movementSpeed = _targetedEntity.GetMovementSpeed();
		_targetedEntity.SetMovementSpeed(movementSpeed+ movementSpeedIncrease);
	}

	public override void CancelEffect(){
		float movementSpeed = _targetedEntity.GetMovementSpeed();
		_targetedEntity.SetMovementSpeed(movementSpeed - movementSpeedIncrease);
	}
}