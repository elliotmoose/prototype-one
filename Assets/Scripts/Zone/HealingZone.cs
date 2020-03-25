using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone: Zone {
	public float HealPerSec = 80f; 
	
	public HealingZone(){
		name = "HealingZone";
	}

	void Awake() {
		name = "HealingZone";
		duration = 25;
    }

	public override void StayInZone(){		
		PLayerEntity.Heal(HealPerSec * Time.deltaTime);
	}

}
