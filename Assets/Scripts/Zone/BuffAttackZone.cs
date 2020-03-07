using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAttackZone: Zone {

	public BuffAttackZone(){
		name = "BuffAttackZone";
	}

	public override void OnEnterZone(){
		Debug.Log("player enter zone");
	}

	public override void OnExitZone(){
		Debug.Log("player exit zone");
	}
	
	void Awake() {
		name = "BuffAttackZone";
		duration = 30;
    }


}