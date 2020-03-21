using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeaponNew : Weapon
{
    public GameObject laser;
    public Transform laserSpawnPoint;

    // Start is called before the first frame update

    protected override void Fire()
    {
        GameObject laserObj = GameObject.Instantiate(laser, laserSpawnPoint.transform.position, laserSpawnPoint.transform.rotation) as GameObject;
        LaserNew _laserScript = laserObj.GetComponent<LaserNew>();
        _laserScript.Activate(this._weaponData, this._owner);
        _laserScript.SetOrigin(laserSpawnPoint.transform.position);

        Rigidbody laserRB = laserObj.GetComponent<Rigidbody>();
        laserRB.velocity = laserSpawnPoint.TransformDirection(Vector3.forward*25);
    }

}
