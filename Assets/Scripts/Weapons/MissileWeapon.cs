using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileWeapon : Weapon
{
    public GameObject bomb;
    public GameObject aoePrefab;
    private GameObject _aoeObject;
    public Transform bombSpawnPoint;

    public override void AttemptFire(float angle, float joystickDistanceRatio)
    {
        if(!CheckActivated())
		{
			return;
		}

        if(_aoeObject == null) 
        {            
            _aoeObject = GameObject.Instantiate(aoePrefab, GetMissileTargetPosition(angle, joystickDistanceRatio) , Quaternion.identity);
        }
        
        float explosionRadius = _weaponData.GetWeaponPropertyValue("EXPLOSION_RADIUS");

        _aoeObject.transform.position = GetMissileTargetPosition(angle, joystickDistanceRatio);
        _aoeObject.transform.localScale = new Vector3(explosionRadius*2, 0.01f, explosionRadius*2);
        _aoeObject.GetComponent<Renderer>().material.color = cooldown > 0 ? Colors.red : Colors.green;
    }


    public override void FireStop(float angle, float joystickDistanceRatio)
    {
        if(cooldown <= 0) 
        {
            Vector3 targetPosition = GetMissileTargetPosition(angle, joystickDistanceRatio);

            GameObject bombObj = GameObject.Instantiate(bomb, bombSpawnPoint.transform.position, bombSpawnPoint.transform.rotation);

            MissileProjectile bombScript = bombObj.GetComponent<MissileProjectile>();
            bombScript.Activate(this._weaponData, this._owner, bombSpawnPoint.transform.position, targetPosition);

            cooldown = 1/_weaponData.GetWeaponPropertyValue("FIRE_RATE");
        }
        
        GameObject.Destroy(_aoeObject);
    }

    private Vector3 GetMissileTargetPosition(float angle, float joystickDistanceRatio) 
    {
        Vector3 delta = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward * joystickDistanceRatio * GetWeaponRange();
        Vector3 target = bombSpawnPoint.transform.position + delta;
        float floatingHeight = 0.4f; //for cast area
        return new Vector3(target.x, floatingHeight, target.z);
    }
}
