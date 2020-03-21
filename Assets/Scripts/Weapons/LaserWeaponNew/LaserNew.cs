using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserNew : MonoBehaviour
{
    protected WeaponData _weaponData;
    protected GameObject _owner;
    protected Vector3 _origin;
    protected float _range;

    void Update(){
        CheckOutOfRange();
        CheckActivated();
    }

    public void CheckOutOfRange() 
    {
        if(Vector3.Distance(this._origin, this.transform.position) > 0 ) 
        {
            Destroy(this.gameObject);
        }
    }

    protected void CheckActivated()
    {

        if(this._weaponData == null || this._owner == null)
        {
            Debug.LogWarning("Please call Activate() on instantiation of this projectile");
        }
    }

    public void Activate(WeaponData weaponData, GameObject owner)
    {
        // Debug.Log("CALLED");
        this._weaponData = weaponData;
        this._owner = owner;
        this._range = weaponData.range;
        SetRange(this._range);
    }

    public void SetOrigin(Vector3 origin){
        this._origin = origin;
    }

    public void SetRange(float range){
        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, range);
    }

    void OnTriggerStay(Collider col)
    {
        //means parent died already
        if(this._owner == null) 
        {
            return;
        }
        //attack ENTITIES of different TAG 
        if(col.gameObject.tag != _owner.tag)
        {           
            Entity entity = col.gameObject.GetComponent<Entity>();
            if(entity != null)
            {
                Debug.Log("enemy taking damage");
                Debug.Log(this._weaponData);
                entity.TakeDamage(this._weaponData.damage);
                Destroy(this.gameObject);           
            }
        }
    }
}


// public class LaserNew : Projectile
// {
//     public float range;
//     override public void CheckOutOfRange() 
//     {
//         if(Vector3.Distance(this._origin, this.transform.position) > 0) 
//         {
//             Destroy(this.gameObject);
//         }
//     }

//     override public void Activate(WeaponData weaponData, GameObject owner) 
//     {
//         this._weaponData = weaponData;
//         this._owner = owner;
//     }

// }
