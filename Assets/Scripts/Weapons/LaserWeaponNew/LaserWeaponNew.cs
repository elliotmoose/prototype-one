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
        Vector3 direction = transform.forward;
        LineRenderer lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = true;

        int piercingCount = (int)_weaponData.GetAttackPropertyValue("PIERCING");
        
        Vector3 lastObjectHitPoint = Vector3.zero;
        RaycastHit[] hits = Physics.RaycastAll(laserSpawnPoint.transform.position, direction, GetWeaponRange());
        foreach(RaycastHit hit in hits) 
        {
            Entity entity = hit.collider.gameObject.GetComponent<Entity>();
            
            if(this._owner.IsOppositeTeam(entity) && piercingCount != 0) 
            {
                lastObjectHitPoint = hit.point;
                entity.TakeDamage(GetWeaponDamage() * Time.deltaTime);
                piercingCount -= 1;
            }
        }
                
        float distanceToLastObjectHit = (piercingCount == 0) ? (lastObjectHitPoint - laserSpawnPoint.transform.position).magnitude : Mathf.Infinity;
        float laserLength = Mathf.Min(GetWeaponRange(), distanceToLastObjectHit);
        lineRenderer.SetPosition(1, new Vector3(0,0, laserLength));        

        // GameObject laserObj = GameObject.Instantiate(laser, laserSpawnPoint.transform.position, laserSpawnPoint.transform.rotation) as GameObject;
        // LaserNew _laserScript = laserObj.GetComponent<LaserNew>();
        // _laserScript.Activate(this._weaponData, this._owner);
        // _laserScript.SetOrigin(laserSpawnPoint.transform.position);

        // Rigidbody laserRB = laserObj.GetComponent<Rigidbody>();
        // laserRB.velocity = laserSpawnPoint.TransformDirection(Vector3.forward*25);
    }

    public override void FireStop() {
        LineRenderer lineRenderer = GetComponentInChildren<LineRenderer>();        
        lineRenderer.enabled = false;
    }

}
