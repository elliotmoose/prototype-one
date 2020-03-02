using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : EntityEffect {
	public float movementSpeedIncrease = -3;	
	public float initialMovementSpeed;

	// override cooldown = 20;

	public SlowEffect(GameObject go):base(go)
	{
		this.cooldown = 5;
		this.name = "slow effect";
		initialMovementSpeed = this.GetEntity().GetComponent<Entity>().GetMovementSpeed();
	}

	public override void UpdateEffect(){
		this.GetEntity().GetComponent<Entity>().SetMovementSpeed(initialMovementSpeed + movementSpeedIncrease);
	}
	public override void CancelEffect(){
		this.GetEntity().GetComponent<Entity>().SetMovementSpeed(initialMovementSpeed);
	}
	// public void SlowEffect(){
	// 	_targetedEntity = this.gameObject;
	// }
}