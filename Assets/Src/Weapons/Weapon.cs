using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
  GameObject weaponItem;

  public Entity owner;
  public WeaponData weaponData;

  public float cooldown = 0;

  //must be awake so that any SetWeaponData will over write default init
  void Awake()
  {
    Initialize();
  }

  public void Activate(WeaponData weaponData, Entity owner)
  {
    this.weaponData = weaponData;
    this.owner = owner;
  }

  protected bool CheckActivated()
  {
    if (weaponData == null || owner == null)
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

  public virtual void AttemptFire(int comboIndex)
  {
    Debug.Log("Fire");
    if (!CheckActivated())
    {
      // log and print weapon name
      Debug.LogWarning($"Weapon not activated {this.weaponData.name}");
      return;
    }

    Fire(comboIndex);
  }

  protected virtual void Fire(int comboIndex) { }
  public virtual void FireStop() { }

  protected virtual void OnHit(TakeDamageInfo[] takeDamageInfos)
  {
    this.owner.GetOnHitModifiers().ForEach(modifier => modifier.OnHit(this, takeDamageInfos));
  }

  public WeaponData GetWeaponData()
  {
    CheckActivated();

    return weaponData;
  }

  public float GetWeaponDamage(int comboIndex = 0)
  {
    return weaponData.damage[comboIndex];
  }

  public float GetWeaponRange(int comboIndex = 0)
  {
    if (!CheckActivated())
    {
      return 0;
    }

    return weaponData.range[comboIndex];
  }

  #region SHOP RELATED
  //
  public float SellWorth()
  {
    return 0;
  }

  #endregion
}
