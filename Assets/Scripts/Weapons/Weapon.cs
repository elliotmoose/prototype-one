using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{	
	protected GameObject _owner;
	GameObject weaponItem;
		
	protected WeaponData _weaponData;

	float cooldown = 0;

	//must be awake so that any SetWeaponData will over write default init
    void Awake()
    {
    	Initialize();
    }

	public void Activate(WeaponData weaponData, GameObject owner)
	{
		this._weaponData = weaponData;
		this._owner = owner;
	}

	private bool CheckActivated()
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

	public void AttemptFire() 
	{	
		if(!CheckActivated())
		{
			return;
		}

		if(cooldown <= 0) 
		{
			Fire();
			cooldown = _weaponData.cooldown;
		}
	}

    protected abstract void Fire();

	public WeaponData GetWeaponData() {
		CheckActivated();

		return _weaponData;
	}

	public float GetWeaponRange() {
		if(!CheckActivated())
		{
			return 0;
		}
		
		return _weaponData.range;
	}
}
