using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerWeapon : Weapon
{    
    float attackWidth = 38;
    
    public Transform projectileSpawnPoint;
    public GameObject flameThrowerParticleSystemObject;

    public override void AttemptFire() 
    {
		if(!CheckActivated())
		{
			return;
		}
        
        Fire();
    }
    protected override void Fire() {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, this.GetWeaponRange());
        Vector3 vectorToTarget = this.transform.forward;
        foreach(var collider in colliders) {            
            Vector3 vectorToCollider = (collider.transform.position - this.transform.position).normalized;
            //180deg -> dot > 0
            //90deg -> dot > 0.5 
            //45deg -> dot > 0.75
            var threshold = 1-(attackWidth/180);
            if(Vector3.Dot(vectorToTarget, vectorToCollider) > threshold) {
                //check is in angled sector infront            
                Entity entity = collider.gameObject.GetComponent<Entity>();
                if(entity != null) {
                    entity.TakeDamage(this.GetWeaponData().damage*Time.deltaTime);
                }
            }
        }

        // //debug rays
        // Ray ray = new Ray(this.transform.position, vectorToTarget);  
        // Vector3 dirLeft = Quaternion.AngleAxis(-attackWidth/2, Vector3.up) * vectorToTarget;
        // Ray rayLeft = new Ray(this.transform.position, dirLeft);  
        // Vector3 dirRight = Quaternion.AngleAxis(+attackWidth/2, Vector3.up) * vectorToTarget;
        // Ray rayRight = new Ray(this.transform.position, dirRight);  
        // Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        // Debug.DrawRay(rayLeft.origin, rayLeft.direction * 10, Color.red);
        // Debug.DrawRay(rayRight.origin, rayRight.direction * 10, Color.blue);


        //ui
        ParticleSystem particleSystem = flameThrowerParticleSystemObject.GetComponent<ParticleSystem>();
        var emission = particleSystem.emission;
        emission.enabled = true;
        var main = particleSystem.main;
        float visualCompensate = 0.2f;
        //main.startSpeed * main.startLifetime = range
        //lifetime = range / start speed
        main.startLifetime = this.GetWeaponRange()/(main.startSpeed.constant) - visualCompensate;

        var shape = particleSystem.shape;
        shape.arc = attackWidth;
    }

    public override void FireStop()
    {
        ParticleSystem particleSystem = flameThrowerParticleSystemObject.GetComponent<ParticleSystem>();
        var emission = particleSystem.emission;
        emission.enabled = false;
    }
}

