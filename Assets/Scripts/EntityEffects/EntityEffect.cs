using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityEffect{
	public float damage = 0;
	public float cooldown = 0;
	public bool active;
	public string name;

	public float attackDamageIncrease = 0;
	public float attackSpeedIncrease = 0;
	public float movementSpeedIncrease = 0;

	private GameObject _targetedEntity;

	public EntityEffect(GameObject targetedEntity )
	{
		_targetedEntity = targetedEntity;
	}

	public void Update(){
		UpdateCooldown();
		if(cooldown > 0){
			active = true;
			Debug.Log("applying effect");
			UpdateEffect();
		}else{
			Debug.Log("effect finished");
			CancelEffect();
			active = false;
		}
	}

	public void SetEntity(GameObject go){
		_targetedEntity = go;
	}
	public GameObject GetEntity(){
		return _targetedEntity;
	}
	
	void UpdateCooldown() 
	{
		cooldown -= Time.deltaTime;     
	}

	public abstract void UpdateEffect();
	public abstract void CancelEffect();

}