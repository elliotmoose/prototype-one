using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone: Zone {
	float HealPerSec = 30; 
	
	public HealingZone(){
		name = "HealingZone";
	}

	void Awake() {
		name = "HealingZone";
		duration = 25;
    }

	public override void StayInZone(){		
		playerEntity.Heal(HealPerSec * Time.deltaTime);
	}

}
