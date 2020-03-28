using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected WeaponData _weaponData;
    protected Entity _owner;
    public GameObject muzzle;
    public GameObject hit;

    protected Vector3 _origin;
    bool canDealDamage = true; //so that it only deals damage to one unit

    private void Start()
    {
        if (muzzle != null)
        {
            var muzzleVFX = GameObject.Instantiate(muzzle, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = this.transform.forward;
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckActivated();
        CheckOutOfRange();
    }
    
    public void Activate(WeaponData weaponData, Entity owner) 
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

            Quaternion rot = Quaternion.FromToRotation(Vector3.up, this.transform.forward);
            //Vector3 pos = col.ClosestPoint(this.transform.position);

            if(hit != null) 
            {
                var hitVFX = GameObject.Instantiate(hit, this.transform.position, rot);
                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }

                if (entity != null)
                {
                    entity.TakeDamage(this._weaponData.GetDamage());
                    Destroy(this.gameObject);    		
                    canDealDamage = false;
                }
            }            
        }
    }
}
