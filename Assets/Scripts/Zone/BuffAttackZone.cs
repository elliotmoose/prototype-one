using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAttackZone: Zone {

	public BuffAttackZone(){
		name = "BuffAttackZone";
	}
	
	void Awake() {
		name = "BuffAttackZone";
		duration = 30;
    }

	public override void OnEnterZone(){
		Debug.Log("enter BuffAttackZone zone");
	}

	public override void OnExitZone(){
		Debug.Log("exit BuffAttackZone zone");
	}

}