using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
	public GameObject projectile;
	public Material m1;
	public Material m2;
	public Transform projectileSpawnPoint;
	Joystick attackJoystick;
	Transform weapon;
	GameObject weaponItem;
		
	WeaponData weaponData;

	float cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        attackJoystick = GameObject.Find("AttackJoystick").GetComponent<Joystick>();;        
    	weaponItem = gameObject.transform.GetChild(0).gameObject;
		weaponData = WeaponData.StandardWeaponData();
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

    public void Fire(){    
		if(cooldown <= 0) {
			GameObject projectileObj = 	GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;	
			
			Projectile projectileScript = projectileObj.GetComponent<Projectile>();
			projectileScript.SetWeaponData(this.weaponData);
			projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

			Rigidbody projectileRB = projectileObj.GetComponent<Rigidbody>();
			projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*10);

			cooldown = weaponData.cooldown;
		}
    }

	public float GetWeaponRange() {
		return weaponData.range;
	}
}
