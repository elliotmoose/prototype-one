using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected WeaponData _weaponData;
    protected GameObject _owner;

    protected Vector3 _origin;

    // Update is called once per frame
    void Update()
    {
        CheckActivated();
        CheckOutOfRange();
    }
    
    public void Activate(WeaponData weaponData, GameObject owner) 
    {
        this._weaponData = weaponData;
        this._owner = owner;
    }

    public void SetOrigin(Vector3 origin) 
    {
        this._origin = origin;
    }

    protected void CheckActivated()
    {
        if(this._weaponData == null || this._owner == null)
        {
            Debug.LogWarning("Please call Activate() on instantiation of this projectile");
        }
    }
    public void CheckOutOfRange() 
    {
        if(Vector3.Distance(this._origin, this.transform.position) > this._weaponData.range) 
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        //means parent died already
        if(_owner == null) 
        {
            return;
        }

        //attack ENTITIES of different TAG 
    	if(col.gameObject.tag != _owner.tag)
        {    		
            Entity entity = col.gameObject.GetComponent<Entity>();
            if(entity != null)
            {
                entity.TakeDamage(this._weaponData.damage);
                Destroy(this.gameObject);    		
            }
    	}
    }
}
