using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMovementSpeedZone: Zone {
	public float movementSpeedMultiplyFactor = 0.6f; 
	private MovementSpeedEffect effect;

	public BuffMovementSpeedZone(){
		name = "BuffMovementSpeedZone";
	}

	void Awake() {
		name = "BuffMovementSpeedZone";
		duration = 27;
    }

	public override void OnEnterZone(){
		Debug.Log("enter BuffMovementSpeedZone zone");
		effect = new MovementSpeedEffect(playerEntity, movementSpeedMultiplyFactor, Mathf.Infinity);
		playerEntity.TakeEffect(effect);
	}

	public override void OnExitZone(){
		Debug.Log("exit BuffMovementSpeedZone zone");
		effect.CancelEffect();
	}
}