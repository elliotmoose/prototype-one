using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone: Zone {
	public float HealPerSec = 3f; 
	private float _perSec = 0;

	
	public HealingZone(){
		name = "HealingZone";
	}

	void Awake() {
		name = "HealingZone";
		duration = 25;
    }

	public override void StayInZone(){
		// Debug.Log("Healing player");
		_perSec -= Time.deltaTime;     
		if(_perSec < 0){
			PLayerEntity.Heal(HealPerSec);
			_perSec = 1;
		}
	}

}
