using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
{
    private WeaponData _weaponData;
    private Entity _owner;
    private Vector3 _sourcePosition;
    private Vector3 _targetPosition;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public AudioClip shotSFX;
    public AudioClip hitSFX; 
    public List<GameObject> trails;
    private float _flightDuration = 1.45f;
    private float _timeAccelerationFactor = 1.7f;
    private float _timeElapsed = 0;

    private void Start()
    {
        // if (muzzlePrefab != null)
        // {
        //     var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
        //     muzzleVFX.transform.forward = gameObject.transform.forward;
        //     var ps = muzzleVFX.GetComponent<ParticleSystem>();
        //     if (ps != null)
        //         Destroy(muzzleVFX, ps.main.duration);
        //     else
        //     {
        //         var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
        //         Destroy(muzzleVFX, psChild.main.duration);
        //     }
        // }

        // if (shotSFX != null && GetComponent<AudioSource>())
        // {
        //     GetComponent<AudioSource>().PlayOneShot(shotSFX);
        // }
    }

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
            _timeElapsed += Time.deltaTime * _timeAccelerationFactor;
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
        Collider[] collidersHit = Physics.OverlapSphere(this.gameObject.transform.position, explosionRadius);
        if (shotSFX != null && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(hitSFX);
        }

        if (trails.Count > 0)
        {
            for (int j = 0; j < trails.Count; j++)
            {
                trails[j].transform.parent = null;
                var ps = trails[j].GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Stop();
                    Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                }
            }
        }

        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, this.transform.position, this.transform.rotation);
            var ps = hitVFX.GetComponent<ParticleSystem>();
            if (ps == null)
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
            else
                Destroy(hitVFX, ps.main.duration);
        }

        StartCoroutine(DestroyParticle(0f));

        int i = 0;
        while (i < collidersHit.Length)
        {
            Entity entity = collidersHit[i].gameObject.GetComponent<Entity>();
            if (_owner.IsOppositeTeam(entity))
            {
                entity.TakeDamage(this._weaponData.GetDamage(), DamageType.ANTIBACTERIA);
                KnockbackEffect knockbackEffect = new KnockbackEffect(entity, this.transform.position, explosionRadius, 0.35f);
                entity.TakeEffect(knockbackEffect);
            }
            i++;
        }


        Camera.main.GetComponent<StressReceiver>().InduceStress(0.3f);

        //vfx sfx
        StartCoroutine(DestroyParticle(0f));
        if (shotSFX != null && GetComponent<AudioSource>())
        {
            Explode();
            GetComponent<AudioSource>().PlayOneShot(hitSFX);
        }

        if (hitPrefab != null)
        {
            var hitVFX = GameObject.Instantiate(hitPrefab, this.transform.position, this.transform.rotation);
            var ps = hitVFX.GetComponent<ParticleSystem>();
            if (ps == null)
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
            else
            {
                Destroy(hitVFX, ps.main.duration);        
            }                
        }


        Destroy(this.gameObject);
    }

    public IEnumerator DestroyParticle(float waitTime)
    {

        if (transform.childCount > 0 && waitTime != 0)
        {
            List<Transform> tList = new List<Transform>();

            foreach (Transform t in transform.GetChild(0).transform)
            {
                tList.Add(t);
            }

            while (transform.GetChild(0).localScale.x > 0)
            {
                yield return new WaitForSeconds(0.01f);
                transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                for (int i = 0; i < tList.Count; i++)
                {
                    tList[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
