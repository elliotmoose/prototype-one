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
            Destroy(this.gameObject);
        }
    }

    public void Explode()
    {
        Collider[] collidersHit = Physics.OverlapSphere(this.gameObject.transform.position, 2f); //2f is the range of the bomb

        int i = 0;
        while (i < collidersHit.Length)
        {
            Entity entity = collidersHit[i].gameObject.GetComponent<Entity>();
            if (collidersHit[i].gameObject.tag != this._owner.tag && entity != null)
            {
                entity.TakeDamage(this._weaponData.damage);
                Destroy(this.gameObject);
            }
            i++;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        // Entity entity = col.gameObject.GetComponent<Entity>();
        Explode();
    }
}
