using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a separate class is needed for visual effects
public class InfectedEffect : MovementSpeedEffect {
	
	DamageFilterEffect damageFilterEffect;
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
		//each infection increases damage take by 20%
		damageFilterEffect = new DamageFilterEffect(_targetedEntity, DamageType.NONE, 1.3f);
		_targetedEntity.TakeEffect(damageFilterEffect);
	}

	public override void OnEffectEnd(){
		base.OnEffectEnd();				
		damageFilterEffect.CancelEffect();
	}
}