using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerWeapon : Weapon
{    
    GameObject flameThrowerGameObject;

    float attackWidth = 28;
    
    public Transform projectileSpawnPoint;
    public GameObject flameThrowerParticleSystemObject;

    protected override void Fire() {
        // GameObject projectileObj = 	GameObject.Instantiate(projectile, projectileSpawnPoint.transform.position,projectileSpawnPoint.transform.rotation) as GameObject;	
			
        // Projectile projectileScript = projectileObj.GetComponent<Projectile>();  
        // projectileScript.Activate(this._weaponData, this._owner);
        // projectileScript.SetOrigin(projectileSpawnPoint.transform.position);

        // Rigidbody projectileRB = projectileObj.GetComponent<Rigidbody>();
        // projectileRB.velocity = projectileSpawnPoint.TransformDirection(Vector3.forward*10);

         //animation
        // if(flameThrowerGameObject == null) {            
        //     GameObject flameThrowerPrefab = GameObject.Find("Resources").GetComponent<PrefabLoader>().skill_flame_thrower_particle_system;                     
        //     flameThrowerGameObject = (GameObject)GameObject.Instantiate(flameThrowerPrefab, this.caster.GetCastPoint(), this.caster.transform.rotation, caster.transform); 
        // }

        // //damage
        // Collider[] colliders = Physics.OverlapSphere(this.caster.transform.position, range);
        // Vector3 vectorToTarget = (target.transform.position - this.caster.transform.position).normalized;
        // foreach(var collider in colliders) {            
        //     Vector3 vectorToCollider = (collider.transform.position - this.caster.transform.position).normalized;
        //     //180deg -> dot > 0
        //     //90deg -> dot > 0.5 
        //     //45deg -> dot > 0.75
        //     var threshold = 1-(attackWidth/180);
        //     if(Vector3.Dot(vectorToTarget, vectorToCollider) > threshold) {
        //         //check is in angled sector infront            
        //         Health health = collider.gameObject.GetComponentInParent<Health>();
        //         if(health != null) {
        //             health.TakeDamage(attackProperties.damage*Time.deltaTime, DamageType.MAGIC, this.caster.gameObject);
        //         }
        //     }
        // }

        // //debug rays
        // // Ray ray = new Ray(caster.transform.position, vectorToTarget);  
        // // Vector3 dirLeft = Quaternion.AngleAxis(-attackWidth/2, Vector3.up) * vectorToTarget;
        // // Ray rayLeft = new Ray(caster.transform.position, dirLeft);  
        // // Vector3 dirRight = Quaternion.AngleAxis(+attackWidth/2, Vector3.up) * vectorToTarget;
        // // Ray rayRight = new Ray(caster.transform.position, dirRight);  
        // // Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        // // Debug.DrawRay(rayLeft.origin, rayLeft.direction * 10, Color.red);
        // // Debug.DrawRay(rayRight.origin, rayRight.direction * 10, Color.blue);


        // //ui
        // var lookPos = target.transform.position - caster.transform.position;
        // var rotation = Quaternion.LookRotation(lookPos);
        // rotation *= Quaternion.Euler(0, -90 + attackWidth/2, 0); //offset angle according to attack width
        // flameThrowerGameObject.transform.rotation = rotation;

        // ParticleSystem flameThrowerParticle = flameThrowerGameObject.GetComponent<ParticleSystem>();
        // var main = flameThrowerParticle.main;
        // main.startSpeed = range;
        // var shape = flameThrowerParticle.shape;
        // shape.arc = attackWidth;
    }
}

