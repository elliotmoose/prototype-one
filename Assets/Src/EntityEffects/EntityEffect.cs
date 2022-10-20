using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityEffect
{
  public float duration = Mathf.Infinity;//total time effect should be applied
  public float age = 0;//how long effect has been applied
  public bool effectEnded = false;
  public bool unique = false; //whether multiple number of effects with same name can be applied at once
  public bool disabling = false; //whether it prevents movements and attack
  private bool _canceled = false; //if canceled, it wont check age anymore
  public string name;

  protected Entity _targeterEntity;
  protected Entity _targetedEntity;

  public EntityEffect(Entity targeterEntity, Entity targetedEntity, bool unique = false, bool disabling = false)
  {
    this._targeterEntity = targeterEntity;
    this._targetedEntity = targetedEntity;
    this.unique = unique;
    this.disabling = disabling;
  }

  public void FixedUpdate()
  {
    FixedUpdateCooldown();
    if (age < duration && !_canceled)
    {
      effectEnded = false;
      Debug.Log("applying effect");
      FixedUpdateEffect();
    }
    else
    {
      Debug.Log("effect finished");
      OnEffectEnd();
      effectEnded = true;
    }
  }

  public void CancelEffect()
  {
    _canceled = true;
  }

  public Entity GetTargetedEntity()
  {
    return _targetedEntity;
  }

  void FixedUpdateCooldown()
  {
    age += Time.fixedDeltaTime;
    Debug.Log(age);
  }

  public virtual void OnEffectApplied() { }
  //this is for unique effects, when the new effect is replacing the old
  public virtual void OnEffectReapplied(EntityEffect oldEffect) { }
  public virtual void FixedUpdateEffect() { }
  public virtual void OnEffectEnd() { }

}