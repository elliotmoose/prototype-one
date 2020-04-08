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
		if(_weaponData == null || _owner == null)  
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
    }

	void UpdateCooldown() 
	{
		cooldown -= Time.deltaTime;     
	}

	public virtual void AttemptFire(float angle, float joystickDistanceRatio) 
	{	
		if(!CheckActivated())
		{
			return;
		}

		if(cooldown <= 0) 
		{			
			Fire(angle, joystickDistanceRatio);
			cooldown = 1/_weaponData.GetWeaponPropertyValue("FIRE_RATE");
		}
	}

    protected virtual void Fire(float angle, float joystickDistanceRatio){}
    public virtual void FireStop(float angle, float joystickDistanceRatio){}

	public WeaponData GetWeaponData() {
		CheckActivated();

		return _weaponData;
	}
	
	public float GetWeaponDamage() {
		return _weaponData.GetWeaponPropertyValue("DAMAGE");
	}

	public float GetWeaponRange() {
		if(!CheckActivated())
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
