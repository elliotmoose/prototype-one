using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{	
	public Material m1;
	public Material m2;

	GameObject weaponItem;
		
	protected WeaponData _weaponData;

	float cooldown = 0;

    void Start()
    {
    	Initialize();
    }

	public virtual void Initialize() 
	{
		weaponItem = gameObject.transform.GetChild(0).gameObject;		
	}

	public virtual void SetWeaponData(WeaponData weaponData) 
	{
		this._weaponData = weaponData;
	}

	public void SetWeaponHighlighted(bool highlighted) {
		weaponItem.GetComponent<Renderer>().material = highlighted ? m2 : m1;
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
		if(cooldown <= 0) 
		{
			Fire();
			cooldown = _weaponData.cooldown;
		}
	}

    protected abstract void Fire();

	public float GetWeaponRange() {
		return _weaponData.range;
	}
}
