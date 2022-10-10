using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
  GameObject weaponItem;

  protected Entity _owner;
  protected WeaponData _weaponData;
  private List<UpgradeDescription> _upgrades = new List<UpgradeDescription>();

  public float cooldown = 0;

  //must be awake so that any SetWeaponData will over write default init
  void Awake()
  {
    Initialize();
  }

  public void Activate(WeaponData weaponData, Entity owner)
  {
    this._weaponData = weaponData;
    this._owner = owner;
  }

  protected bool CheckActivated()
  {
    if (_weaponData == null || _owner == null)
    {
      Debug.LogWarning($"This weapon has not been activated: {this.transform.name}");
      return false;
    }

    return true;
  }

  private void Initialize()
  {
    weaponItem = gameObject.transform.GetChild(0).gameObject;
  }

  // Update is called once per frame
  void Update()
  {
    UpdateCooldown();
    WeaponUpdate();
  }

  void UpdateCooldown()
  {
    cooldown -= Time.deltaTime;
  }

  public virtual void WeaponUpdate()
  {

  }

  public virtual void AttemptFire()
  {
    Debug.Log("Fire");
    if (!CheckActivated())
    {
      // log and print weapon name
      Debug.LogWarning($"Weapon not activated {this._weaponData.name}");
      return;
    }

    if (cooldown <= 0)
    {
      Fire();
      cooldown = 1 / _weaponData.GetWeaponPropertyValue("FIRE_RATE");
    }
    else
    {
      Debug.Log("Weapon on cooldown");
    }
  }

  protected virtual void Fire() { }
  public virtual void FireStop() { }

  public WeaponData GetWeaponData()
  {
    CheckActivated();

    return _weaponData;
  }

  public float GetWeaponDamage()
  {
    return _weaponData.GetWeaponPropertyValue("DAMAGE");
  }

  public float GetWeaponRange()
  {
    if (!CheckActivated())
    {
      return 0;
    }

    return _weaponData.GetWeaponPropertyValue("RANGE");
  }

  #region SHOP RELATED
  //
  public float SellWorth()
  {
    return 0;
  }

  #endregion
}
