using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedEffect : MovementSpeedEffect {
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="_targetedEntity"></param>
	/// <param name="movementSpeedDeltaFactor">0.5 means a 50% INCREASE in ms</param>
	/// <param name="duration"></param>
	/// <returns></returns>
	public InfectedEffect(Entity _targetedEntity, float movementSpeedDeltaFactor, float duration):base(_targetedEntity, movementSpeedDeltaFactor, duration)
	{
		this.name = "INFECTED_EFFECT";
	}

	public override void OnEffectApplied()
	{
		base.OnEffectApplied();
	}

	public override void OnEffectEnd(){
		base.OnEffectEnd();		
		
	}
}