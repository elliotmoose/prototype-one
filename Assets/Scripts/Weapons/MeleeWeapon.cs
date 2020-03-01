using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public Enemy enemy;

    protected override void Fire(){

        Collider[] collidersHit = Physics.OverlapBox(this.transform.forward, new Vector3(1,1,1), this.transform.rotation);
        int i = 0;
        while (i < collidersHit.Length){
            if (collidersHit[i].gameObject.tag == "Enemy"){
                enemy.takeDamage();
            }
        }
    }
}
