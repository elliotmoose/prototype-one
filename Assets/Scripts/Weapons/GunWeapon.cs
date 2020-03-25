﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{    

    public GameObject projectile;
    public Transform projectileSpawnPoint;

    protected override void Fire() {

        float bulletNumber =  this._weaponData.GetAttackPropertyValue("BULLET_SPLIT");


        GameObject projectileObj = 	GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;	
			
        Projectile projectileScript = projectileObj.GetComponent<Projectile>();  
        projectileScript.Activate(this._weaponData, this._owner);
        projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

        Rigidbody projectileRB = projectileObj.GetComponent<Rigidbody>();
        projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*20 + new Vector3(0,3,0));
    }
}
