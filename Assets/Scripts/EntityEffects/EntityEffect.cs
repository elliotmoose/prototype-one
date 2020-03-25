using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityEffect{
	public float duration = 0;//total time effect should be applied
	public float age = 0;//how long effect has been applied
	public bool active;
	public bool unique = false; //whether multiple number of effects with same name can be applied at once
	public string name;

	protected Entity _targetedEntity;

	public EntityEffect(Entity targetedEntity)
	{
		this._targetedEntity = targetedEntity;
	}

	public EntityEffect(Entity targetedEntity, bool unique)
	{
		this._targetedEntity = targetedEntity;
		this.unique = unique;
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
	//this is for unique effects, when the new effect is replacing the old
	public virtual void OnEffectReapplied(EntityEffect oldEffect){}
	public virtual void UpdateEffect(){}
	public virtual void CancelEffect(){}

}