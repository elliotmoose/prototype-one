using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : Projectile
{

    void Update()
    {
        CheckActivated();
        CheckOutOfRange();
    }

    public new void CheckOutOfRange() // override the function
    {
        if (Vector3.Distance(this._origin, this.transform.position) > this._weaponData.range)
        {
            // attack enemies in range
            Explode();
        }
    }

    public void Explode()
    {
        float explosionRadius = _weaponData.GetAttackPropertyValue("EXPLOSION_RADIUS");
        Collider[] collidersHit = Physics.OverlapSphere(this.gameObject.transform.position, explosionRadius); //2f is the range of the bomb

        int i = 0;
        while (i < collidersHit.Length)
        {
            Entity entity = collidersHit[i].gameObject.GetComponent<Entity>();
            if (_owner.IsSameTeam(entity))
            {
                entity.TakeDamage(this._weaponData.damage);
            }
            i++;
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        Entity entity = col.gameObject.GetComponent<Entity>();
        if (_owner.IsSameTeam(entity))
        {
            Explode();
        }
    }
}
