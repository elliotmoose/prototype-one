using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserWeapon : Weapon
{
    public Transform laserSpawnPoint;

    protected override void Fire(float angle, float joystickDistanceRatio)
    {
        Vector3 direction = transform.forward;
        LineRenderer lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = true;

        // ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        // var psMain = ps.main;
        // var emission =  ps.emission;
        // emission.enabled = true;        

        
        Vector3 lastObjectHitPoint = Vector3.zero;
        RaycastHit[] hits = Physics.RaycastAll(laserSpawnPoint.transform.position, direction, GetWeaponRange());
        Debug.DrawRay(laserSpawnPoint.transform.position, direction.normalized * GetWeaponRange(), Color.red, 10);
        

        
        // Debug.draw
        foreach(RaycastHit hit in hits) 
        {
            Entity entity = hit.collider.gameObject.GetComponent<Entity>();
            
            if(this._owner.IsOppositeTeam(entity)) 
            {
                lastObjectHitPoint = hit.point;
                entity.TakeDamage(GetWeaponDamage() * Time.deltaTime);
            }
        }
                
        float laserLength = GetWeaponRange();
        lineRenderer.SetPosition(1, new Vector3(0,0, laserLength));        

        // float speed = psMain.startSpeed.constant;
        // psMain.startLifetime = laserLength/speed;


        // GameObject laserObj = GameObject.Instantiate(laser, laserSpawnPoint.transform.position, laserSpawnPoint.transform.rotation) as GameObject;
        // LaserNew _laserScript = laserObj.GetComponent<LaserNew>();
        // _laserScript.Activate(this._weaponData, this._owner);
        // _laserScript.SetOrigin(laserSpawnPoint.transform.position);

        // Rigidbody laserRB = laserObj.GetComponent<Rigidbody>();
        // laserRB.velocity = laserSpawnPoint.TransformDirection(Vector3.forward*25);
    }

    public override void FireStop(float angle, float joystickDistanceRatio) {
        LineRenderer lineRenderer = GetComponentInChildren<LineRenderer>();        
        lineRenderer.enabled = false;

        // ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        // var emission =  ps.emission;
        // emission.enabled = false;        
    }

}
