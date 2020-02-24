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
		weaponData = WeaponData.StandardWeapon();
    }

	public void SetWeaponHighlighted(bool highlighted) {
		weaponItem.GetComponent<Renderer>().material = highlighted ? m2 : m1;
	}

    // Update is called once per frame
    void Update()
    {


		cooldown -= Time.deltaTime;        
    }

    public void Fire(){    
		if(cooldown <= 0) {
			GameObject projectileObj = 	GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;	
			
			Projectile projectileScript = projectileObj.GetComponent<Projectile>();
			projectileScript.SetWeaponData(this.weaponData);

			Rigidbody projectileRB = projectileObj.GetComponent<Rigidbody>();
			projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*10);

			cooldown = weaponData.cooldown;
		}
    }

	public float GetWeaponRange() {
		return weaponData.range;
	}
}
