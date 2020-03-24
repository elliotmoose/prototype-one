using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{	
	GameObject weaponItem;
	
	protected Entity _owner;		
	protected WeaponData _weaponData;
	private List<UpgradeDescription> _upgrades = new List<UpgradeDescription>();	

	float cooldown = 0;

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

	public virtual void AttemptFire() 
	{	
		if(!CheckActivated())
		{
			return;
		}

		if(cooldown <= 0) 
		{			
			Fire();
			cooldown = 1/_weaponData.fireRate;
		}
	}

	public virtual void AttemptFireDirected(Vector3 direction) 
	{	
		if(!CheckActivated())
		{
			return;
		}

		if(cooldown <= 0) 
		{			
			FireDirected(direction);
			cooldown = 1/_weaponData.fireRate;
		}
	}

    protected virtual void Fire(){}
	protected virtual void FireDirected(Vector3 direction){}
    public virtual void FireStop(){}

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

	#region SHOP RELATED
	//
	public float SellWorth()
	{
		return 0;
	}

	#endregion
}
