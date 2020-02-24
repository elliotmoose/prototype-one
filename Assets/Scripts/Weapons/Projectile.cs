using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    WeaponData weaponData;

    private Vector3 _origin;

    // Update is called once per frame
    void Update()
    {
        CheckOutOfRange();
    }
    
    public void SetWeaponData(WeaponData weaponData) 
    {
        this.weaponData = weaponData;
    }

    public void SetOrigin(Vector3 origin) 
    {
        this._origin = origin;
    }

    public void CheckOutOfRange() 
    {
        if(Vector3.Distance(this._origin, this.transform.position) > this.weaponData.range) 
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
    	if(col.gameObject.tag == "enemy")
        {
    		Debug.Log("Hit enemy");

            Entity entity = col.gameObject.GetComponent<Entity>();
            if(entity != null) 
            {
                entity.TakeDamage(this.weaponData.damage);
            }

	    	Destroy(gameObject);    		
    	}
    }
}
