using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private WeaponData _weaponData;
    private GameObject _owner;

    private Vector3 _origin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void CheckActivated()
    {
        if (this._weaponData == null || this._owner == null)
        {
            Debug.LogWarning("Please call Activate() on instantiation of this projectile");
        }
    }

    void OnTriggerEnter(Collider col)
    {

        Entity entity = col.gameObject.GetComponent<Entity>();

        //attack ENTITIES of different TAG 
        if (col.gameObject.tag != _owner.tag && entity != null)
        {
            entity.TakeDamage(this._weaponData.damage);
            Destroy(gameObject);
        }
    }

    public Transform GetTargetLocation()
    {
        return null;
    }
}
