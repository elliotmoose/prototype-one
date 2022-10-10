using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnHealthChangeEvent(float amount);

public abstract class Entity : MonoBehaviour
{
  protected float _maxHealth = 400;
  protected float _curHealth = 400;
  protected float _movementSpeed = 3;
  protected bool _disabled = false;

  protected GameObject _equippedWeapon;

  protected List<EntityEffect> _entityEffects = new List<EntityEffect>();

  public event OnHealthChangeEvent OnTakeDamageEvent;

  public GameObject EquipWeapon(WeaponData weaponData)
  {
    if (weaponData.type == WeaponType.NULL)
    {
      return null;
    }

    //0. Clean up previously equipped weapon
    if (this._equippedWeapon != null)
    {
      GameObject.Destroy(_equippedWeapon);
    }

    //1. Instantiate weapon prefab (based on weaponId) and attach as child
    GameObject weaponSlot = GetWeaponSlot();

    if (weaponSlot == null)
    {
      Debug.LogWarning("EquipWeapon: This entity has no weapon slot");
      return null;
    }

    Object weaponPrefab = Resources.Load($"Prefabs/Weapons/{weaponData.type.ToString()}");
    Debug.Log($"Loading weapon: {weaponPrefab.name}");
    GameObject newWeaponObject = (GameObject)GameObject.Instantiate(weaponPrefab, weaponSlot.transform.position, weaponSlot.transform.rotation);
    newWeaponObject.transform.SetParent(weaponSlot.transform);
    newWeaponObject.transform.position = weaponSlot.transform.position;
    newWeaponObject.transform.rotation = weaponSlot.transform.rotation;
    newWeaponObject.transform.localScale = Vector3.one;

    //2. Update weapon's weaponData
    Weapon weaponComponent = newWeaponObject.GetComponent<Weapon>();
    weaponComponent.Activate(weaponData, this);
    //3. Assign game object to equipped weapon
    this._equippedWeapon = newWeaponObject;

    return newWeaponObject;
  }

  #region Health
  //getter methods 
  public float GetMaxHealth()
  {
    return _maxHealth;
  }
  public float GetCurHealth()
  {
    return _curHealth;
  }

  //setter methods 
  public void SetMaxHealth(float health)
  {
    //curhealth % will remain
    _curHealth = (_curHealth / _maxHealth) * health;
    _maxHealth = health;
  }

  public void Heal(float heal)
  {
    if (_curHealth < _maxHealth)
    {
      _curHealth += heal;
    }
  }

  //calculates any filters the damage have to go through
  public float FilteredDamage(float raw, DamageType type)
  {
    List<EntityEffect> effects = _entityEffects.FindAll((EntityEffect el) => el.GetType() == typeof(DamageFilterEffect));

    float factor = 1;
    foreach (EntityEffect effect in effects)
    {
      DamageFilterEffect filter = effect as DamageFilterEffect;

      //damagetype none means applies to all damage types
      if (type == filter.triggerDamageType || filter.triggerDamageType == DamageType.NONE)
      {
        factor += (filter.damageMultiplier - 1);
      }
    }
    Debug.Log($"Damage amplified by factor {factor}");
    return raw * factor;
  }

  public void TakeDamage(float damage, DamageType type)
  {
    float damageToTake = FilteredDamage(damage, type);
    _curHealth -= damageToTake; //input how much to decrease by-- POSITIVE VALUE        
    OnTakeDamage(damageToTake);

    if (OnTakeDamageEvent != null) OnTakeDamageEvent(damageToTake);

    if (_curHealth <= 0)
    {
      this.Die();
    }
  }

  protected virtual void OnTakeDamage(float damage) { }

  public abstract void Die();

  #endregion

  public void SetDisabled(bool disabled)
  {
    this._disabled = disabled;
    OnDisabledChanged(disabled);
  }

  protected virtual void OnDisabledChanged(bool disabled)
  {

  }

  public void SetMovementSpeed(float speed)
  {
    this._movementSpeed = speed;
  }

  public float GetMovementSpeed()
  {
    return this._movementSpeed;
  }

  #region Weapon

  public GameObject GetWeaponSlot()
  {
    Queue<Transform> queue = new Queue<Transform>();
    queue.Enqueue(this.transform);
    while (queue.Count > 0)
    {
      var c = queue.Dequeue();
      if (c.name == "WeaponSlot")
        return c.gameObject;
      foreach (Transform t in c)
        queue.Enqueue(t);
    }
    return null;
    // GameObject weaponSlot = this.transform.Find("WeaponSlot").gameObject;
    // return weaponSlot;
  }

  public GameObject GetEquippedWeaponGameObject()
  {
    GameObject weaponSlot = GetWeaponSlot();

    if (weaponSlot == null) return null;
    for (int i = 0; i < weaponSlot.transform.childCount; i++)
    {
      if (weaponSlot.transform.GetChild(i).gameObject.activeSelf == true)
      {
        return weaponSlot.transform.GetChild(i).gameObject;
      }
    }
    return null;
  }


  public Weapon GetEquippedWeaponComponent()
  {
    GameObject weaponGameObject = GetEquippedWeaponGameObject();
    if (weaponGameObject == null)
    {
      Debug.LogWarning("GetWeaponComponent: No weapon gameobject on this entity");
      return null;
    }

    return weaponGameObject.GetComponent<Weapon>();
  }

  public WeaponData GetEquippedWeaponData()
  {
    Weapon weaponComponent = GetEquippedWeaponComponent();
    if (weaponComponent == null)
    {
      Debug.LogWarning("GetWeaponData: No weapon component on this entity");
      return null;
    }

    return weaponComponent.GetWeaponData();
  }

  #endregion


  //receive an effect
  public void TakeEffect(EntityEffect effect)
  {
    if (effect.unique)
    {
      EntityEffect existingEffect = this._entityEffects.Find((thisEffect) =>
      {
        return thisEffect.name == effect.name;
      });

      if (existingEffect != null)
      {
        effect.OnEffectReapplied(existingEffect);
        _entityEffects.Remove(existingEffect); //remove duplicate
        this._entityEffects.Add(effect);
        return;
      }
    }

    effect.OnEffectApplied();
    this._entityEffects.Add(effect);
  }

  public bool HasEffectOfType(System.Type type)
  {
    EntityEffect entityEffect = _entityEffects.Find((effect) => effect.GetType().Equals(type));
    return entityEffect != null;
  }

  public void UpdateEffects()
  {

    for (int i = 0; i < this._entityEffects.Count; i++)
    {
      EntityEffect effect = this._entityEffects[i];
      effect.Update();
      if (effect.effectEnded == true)
      {
        _entityEffects.Remove(effect);
        Debug.Log("entityEffect deleted");
      }
    }
  }

  public bool IsOppositeTeam(Entity other)
  {
    return (other != null && other.tag != this.tag);
  }

  #region Animation
  protected void LinkAnimationEvents()
  {
    AnimationReceiver animationReceiver = GetComponentInChildren<AnimationReceiver>();
    if (animationReceiver == null)
    {
      // Debug.Log("No animation receiver on this object");
      return;
    }

    // Debug.Log("Animations linked");
    animationReceiver.OnAnimationBegin += OnAnimationBegin;
    animationReceiver.OnAnimationCommit += OnAnimationCommit;
    animationReceiver.OnAnimationExecute += OnAnimationExecute;
    animationReceiver.OnAnimationEnd += OnAnimationEnd;
  }

  protected virtual void OnAnimationBegin(string key) { }
  protected virtual void OnAnimationCommit(string key) { }
  protected virtual void OnAnimationExecute(string key) { }
  protected virtual void OnAnimationEnd(string key) { }

  #endregion

}