using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{    
    public GameObject projectile;
    public Transform projectileSpawnPoint;

    public override void Initialize() 
    {
        base.Initialize();
        this.SetWeaponData(WeaponData.StandardWeaponData());
    }
    
    protected override void Fire() {
        GameObject projectileObj = 	GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;	
			
        Projectile projectileScript = projectileObj.GetComponent<Projectile>();
        projectileScript.SetWeaponData(this._weaponData);
        projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

        Rigidbody projectileRB = projectileObj.GetComponent<Rigidbody>();
        projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*10);
    }
}
