using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityEffect{
	public float duration = 0;//total time effect should be applied
	public float age = 0;//how long effect has been applied
	public bool active;
	public string name;

	protected Entity _targetedEntity;

	public EntityEffect(Entity targetedEntity)
	{
		_targetedEntity = targetedEntity;
	}

	public void Update(){
		UpdateCooldown();
		if(age < duration){
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
		age += Time.deltaTime;     
	}

	public virtual void OnEffectApplied(){}
	public virtual void UpdateEffect(){}
	public virtual void CancelEffect(){}

}