using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
{
    private WeaponData _weaponData;
    private Entity _owner;
    private Vector3 _sourcePosition;
    private Vector3 _targetPosition;
    private float _flightDuration = 1.3f;
    private float _timeElapsed = 0;

    void Update()
    {
        CheckActivated();
        UpdatePosition();
    }

    public void Activate(WeaponData weaponData, Entity owner, Vector3 sourcePosition, Vector3 targetPosition) 
    {
        this._weaponData = weaponData;
        this._owner = owner;
        this._sourcePosition = sourcePosition;
        this._targetPosition = targetPosition;
    }

    void CheckActivated()
    {
        if(this._weaponData == null || this._owner == null || this._sourcePosition  == null || this._targetPosition == null)
        {
            Debug.LogWarning("Please call Activate() on instantiation of this projectile");
        }
    }

    void UpdatePosition() 
    {
        if (_targetPosition != null && _sourcePosition != null)
        {
            _timeElapsed += Time.deltaTime;
            // var trajectory = (targetPos - currentPos).normalized;

            Vector3 sHorizontal = _sourcePosition + (_targetPosition - _sourcePosition)*(_timeElapsed/_flightDuration);

            //initial velocity (vertical)
            float g = Physics.gravity.y;
            float uVertical = -g*_flightDuration/2; //initial velocity is upwards and enough to hit vy=0 at time t/2            
            float x = sHorizontal.x;
            float y = uVertical * _timeElapsed + (0.5f)*g*Mathf.Pow(_timeElapsed,2); //ut + (1/2)at^2
            float z = sHorizontal.z;
            this.transform.position = new Vector3(x, y, z);
        
            if(y <= 0) {
                Explode();
            }
        }
    }

    public void Explode()
    {
        float explosionRadius = _weaponData.GetWeaponPropertyValue("EXPLOSION_RADIUS");
        Collider[] collidersHit = Physics.OverlapSphere(this.gameObject.transform.position, explosionRadius); //2f is the range of the bomb

        int i = 0;
        while (i < collidersHit.Length)
        {
            Entity entity = collidersHit[i].gameObject.GetComponent<Entity>();
            if (_owner.IsOppositeTeam(entity))
            {
                entity.TakeDamage(this._weaponData.GetDamage());
                KnockbackEffect knockbackEffect = new KnockbackEffect(entity, this.transform.position, explosionRadius, 0.35f);
                entity.TakeEffect(knockbackEffect);
            }
            i++;
        }

        Camera.main.GetComponent<StressReceiver>().InduceStress(0.3f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        Entity entity = col.gameObject.GetComponent<Entity>();
        if (_owner.IsOppositeTeam(entity))
        {
            Explode();
        }
    }
}
