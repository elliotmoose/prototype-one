using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserNew : Projectile
{
    void Update(){
        CheckOutOfRange();
        CheckActivated();
    }

    public new void CheckOutOfRange() 
    {
        if(Vector3.Distance(this._origin, this.transform.position) > 0 ) 
        {
            Destroy(this.gameObject);
        }
    }


    public new void Activate(WeaponData weaponData, Entity owner)
    {
        this._weaponData = weaponData;
        this._owner = owner;
        SetRange(weaponData.range);
    }

    public void SetRange(float range){
        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, range);
    }

}


