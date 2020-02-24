using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    WeaponData weaponData;

    public void SetWeaponData(WeaponData weaponData) {
        this.weaponData = weaponData;
    }

    // Start is called before the first frame update
    void Start()
    {
        // gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
