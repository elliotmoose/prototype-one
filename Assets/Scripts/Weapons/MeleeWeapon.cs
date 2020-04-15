using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    //public Enemy enemy;
    public float weaponHitWidth = 1;
    protected override void Fire(float angle, float joystickDistanceRatio){
        Collider[] collidersHit = Physics.OverlapBox(this.transform.position, new Vector3(weaponHitWidth*2,2,this.GetWeaponRange()*2), this.transform.rotation);
        foreach (Collider collider in collidersHit)
        {
            if (collider.gameObject.tag != _owner.tag)
            {
                Entity entity = collider.gameObject.GetComponent<Entity>();
                if(entity != null) 
                {
                    entity.TakeDamage(GetWeaponDamage(), DamageType.NORMAL);
                }
            }            
        }
    }
}
