using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    //public Enemy enemy;

    protected override void Fire(){

        Collider[] collidersHit = Physics.OverlapBox(this.transform.forward, new Vector3(1,1,1), this.transform.rotation);
        int i = 0;
        while (i < collidersHit.Length){
            Entity entity = collidersHit[i].gameObject.GetComponent<Entity>();
            if (collidersHit[i].gameObject.tag != _owner.tag && entity != null)
            {
                entity.TakeDamage(this._weaponData.damage);
            }
            i++;
        }
    }
}
