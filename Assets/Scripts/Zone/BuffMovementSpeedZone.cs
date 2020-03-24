using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMovementSpeedZone: Zone {
	public float AdditionalMovementSpeed = 3f; 
	
	public BuffMovementSpeedZone(){
		name = "BuffMovementSpeedZone";
	}

	void Awake() {
		name = "BuffMovementSpeedZone";
		duration = 27;
    }

	public override void OnEnterZone(){
		Debug.Log("enter BuffMovementSpeedZone zone");
		PLayerEntity.SetMovementSpeed(PLayerEntity.GetMovementSpeed() + AdditionalMovementSpeed);
	}

	public override void OnExitZone(){
		Debug.Log("exit BuffMovementSpeedZone zone");
		PLayerEntity.SetMovementSpeed(PLayerEntity.GetMovementSpeed() - AdditionalMovementSpeed);
	}
}