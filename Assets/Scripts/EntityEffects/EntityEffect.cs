using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityEffect{
	public float damage = 0;
	public float cooldown = 0;
	public bool active;
	public string name;

	protected Entity _targetedEntity;

	public EntityEffect(Entity targetedEntity)
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

	public Entity GetTargetedEntity(){
		return _targetedEntity;
	}
	
	void UpdateCooldown() 
	{
		cooldown -= Time.deltaTime;     
	}

	public virtual void OnEffectApplied(){}
	public virtual void UpdateEffect(){}
	public virtual void CancelEffect(){}

}