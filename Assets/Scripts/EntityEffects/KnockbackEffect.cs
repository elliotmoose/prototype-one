using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : EntityEffect {
	public float movementSpeedIncrease = -3;	

	private Vector3 _initial;
	private Vector3 _final;
	private Vector3 _direction;

	public KnockbackEffect(Entity _targetedEntity, Vector3 explosionOrigin, float distance, float time):base(_targetedEntity)
	{
		this.duration = time;
		this.name = "KNOCKBACK_EFFECT";

		_initial = _targetedEntity.transform.position;
		_direction = _targetedEntity.transform.position - explosionOrigin;


		Vector3 targetFinal = _initial + _direction.normalized * distance;
		Vector3 constrainedFinal = new Vector3(targetFinal.x, _initial.y ,targetFinal.z);
		_final = constrainedFinal;
	}


	public override void UpdateEffect() {

		float progress = age/duration;
		_targetedEntity.transform.position =  Vector3.Lerp(_initial, _final, progress);
	}

	public override void OnEffectApplied()
	{
		_targetedEntity.SetDisabled(true);
	}

	public override void CancelEffect(){
		_targetedEntity.SetDisabled(false);
	}
}