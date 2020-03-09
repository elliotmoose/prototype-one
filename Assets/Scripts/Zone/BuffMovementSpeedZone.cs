using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMovementSpeedZone: Zone {
	public float AdditionalMovementSpeed = 2f; 
	
	public BuffMovementSpeedZone(){
		name = "BuffMovementSpeedZone";
	}

	void Awake() {
		name = "BuffMovementSpeedZone";
		duration = 29;
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