using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileWeapon : Weapon
{
    public GameObject bomb;
    public Transform bombSpawnPoint;

    protected override void Fire()
    {
        GameObject bombObj = GameObject.Instantiate(bomb, bombSpawnPoint.transform.position, bombSpawnPoint.transform.rotation);

        MissileProjectile bombScript = bombObj.GetComponent<MissileProjectile>();
        bombScript.Activate(this._weaponData, this._owner);

        bombScript.SetOrigin(bombSpawnPoint.transform.position);

        Rigidbody projectileRB = bombObj.GetComponent<Rigidbody>();
        projectileRB.velocity = bombSpawnPoint.TransformDirection(Vector3.forward * 10);
    }

}
