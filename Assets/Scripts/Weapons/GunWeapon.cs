using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{    

    public GameObject projectile;
    public Transform projectileSpawnPoint;
    private float _deflectedAngle;
    public GameObject projectileObj;
    public Projectile projectileScript;
    public Rigidbody projectileRB;

    protected override void Fire(float angle, float joystickDistanceRatio) {

        float bulletNumber =  this._weaponData.GetWeaponPropertyValue("BULLET_SPLIT");

        if(bulletNumber == 1){
            _deflectedAngle = 0f;
            
            projectileObj =     GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;  
            projectileScript = projectileObj.GetComponent<Projectile>();  
            projectileScript.Activate(this._weaponData, this._owner);
            projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

            projectileRB = projectileObj.GetComponent<Rigidbody>();
            projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*20 + new Vector3(_deflectedAngle, 0,0));
        }

        else if(bulletNumber == 2){
            _deflectedAngle = 5f;
            
            projectileObj =     GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;  
            projectileScript = projectileObj.GetComponent<Projectile>();  
            projectileScript.Activate(this._weaponData, this._owner);
            projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

            projectileRB = projectileObj.GetComponent<Rigidbody>();
            projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*20 + new Vector3(_deflectedAngle, 0,0));

            projectileObj =     GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;  
            projectileScript = projectileObj.GetComponent<Projectile>();  
            projectileScript.Activate(this._weaponData, this._owner);
            projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

            projectileRB = projectileObj.GetComponent<Rigidbody>();
            projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*20 + new Vector3( -1f* _deflectedAngle, 0,0));
            
        }

        else if(bulletNumber == 3){
            _deflectedAngle = 10f;
            
            projectileObj =     GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;  
            projectileScript = projectileObj.GetComponent<Projectile>();  
            projectileScript.Activate(this._weaponData, this._owner);
            projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

            projectileRB = projectileObj.GetComponent<Rigidbody>();
            projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*20 + new Vector3(-1f*_deflectedAngle, 0,0));

            projectileObj =     GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;  
            projectileScript = projectileObj.GetComponent<Projectile>();  
            projectileScript.Activate(this._weaponData, this._owner);
            projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

            projectileRB = projectileObj.GetComponent<Rigidbody>();
            projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*20 + new  Vector3( _deflectedAngle, 0,0));

            projectileObj =     GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;  
            projectileScript = projectileObj.GetComponent<Projectile>();  
            projectileScript.Activate(this._weaponData, this._owner);
            projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

            projectileRB = projectileObj.GetComponent<Rigidbody>();
            projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*20 + new  Vector3( 0, 0,0));
        }

        else if(bulletNumber == 4){
            _deflectedAngle = 5f;
            
            projectileObj =     GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;  
            projectileScript = projectileObj.GetComponent<Projectile>();  
            projectileScript.Activate(this._weaponData, this._owner);
            projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

            projectileRB = projectileObj.GetComponent<Rigidbody>();
            projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*20 + new  Vector3( -2f*_deflectedAngle, 0,0));

            projectileObj =     GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;  
            projectileScript = projectileObj.GetComponent<Projectile>();  
            projectileScript.Activate(this._weaponData, this._owner);
            projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

            projectileRB = projectileObj.GetComponent<Rigidbody>();
            projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*20 + new  Vector3( -1f*2.75f, 0,0));

            projectileObj =     GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;  
            projectileScript = projectileObj.GetComponent<Projectile>();  
            projectileScript.Activate(this._weaponData, this._owner);
            projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

            projectileRB = projectileObj.GetComponent<Rigidbody>();
            projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*20 + new  Vector3(2f*_deflectedAngle, 0,0));

            projectileObj =     GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;  
            projectileScript = projectileObj.GetComponent<Projectile>();  
            projectileScript.Activate(this._weaponData, this._owner);
            projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

            projectileRB = projectileObj.GetComponent<Rigidbody>();
            projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*20 + new  Vector3( 2.75f, 0,0));
        }

       
    }
}
