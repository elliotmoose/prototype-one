using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : Weapon
{
    public Color startColor;
    public Color endColor;
    private float _maxDamageMultiplier = 2f;
    private float _maxChargeTime = 2;
    private float _curChargeTime = 0;

    private Entity _lastTickFirstEntity;
    public Transform laserSpawnPoint;
    public GameObject hitVfxPrefab;
    private List<GameObject> _hitVfxObjects = new List<GameObject>();

    // Start is called before the first frame update

    public override void AttemptFire(float angle, float distance) 
    {
		if(!CheckActivated())
		{
			return;
		}
        
        Fire(angle, distance);
    }
    
    protected override void Fire(float angle, float distance)
    {
        
        Vector3 direction = transform.forward;
        LineRenderer lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = true;


        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        var psMain = ps.main;
        var emission =  ps.emission;
        // emission.enabled = true;        

        int piercingCount = (int)_weaponData.GetWeaponPropertyValue("PIERCING");
        SetVfxObjectCount(piercingCount);
        
        Vector3 lastObjectHitPoint = Vector3.zero;
        RaycastHit[] hits = Physics.RaycastAll(laserSpawnPoint.transform.position, direction, GetWeaponRange());
        
        bool isFirstHitOfTick = true;
        float chargeProgress = _curChargeTime/_maxChargeTime;
        float damageMultiplier = (_maxDamageMultiplier-1) * chargeProgress + 1;        
        lineRenderer.startColor = Color.Lerp(startColor, endColor, chargeProgress);
        lineRenderer.endColor = Color.Lerp(startColor, endColor, chargeProgress);

        foreach(RaycastHit hit in hits) 
        {
            Entity entity = hit.collider.gameObject.GetComponent<Entity>();
            
            if(this._owner.IsOppositeTeam(entity) && piercingCount != 0) 
            {
                //only for first target
                if(isFirstHitOfTick) 
                {
                    isFirstHitOfTick = false;
                    //if it is the same first target
                    if(_lastTickFirstEntity == entity) 
                    {
                        _curChargeTime += Time.deltaTime;
                    }
                    else 
                    {
                        _curChargeTime = 0;
                        _lastTickFirstEntity = entity;
                    }
                    
                }

                // vfx
                GameObject vfx = _hitVfxObjects[piercingCount-1];
                vfx.SetActive(true);
                vfx.transform.position = hit.point;
                vfx.transform.rotation = transform.rotation;

                lastObjectHitPoint = hit.point;
                
                
                entity.TakeDamage(GetWeaponDamage() * damageMultiplier * Time.deltaTime);
                piercingCount -= 1;
            }
        }

        //if didn't hit anything
        if(isFirstHitOfTick) 
        {
            _curChargeTime = 0;
            _lastTickFirstEntity = null;     
        }

        //turn off vfx for those that didn't hit        
        for(int i=0; i<piercingCount; i++) {
            _hitVfxObjects[i].SetActive(false);
        }

        float distanceToLastObjectHit = (piercingCount == 0) ? (lastObjectHitPoint - laserSpawnPoint.transform.position).magnitude : Mathf.Infinity;
        float laserLength = Mathf.Min(GetWeaponRange(), distanceToLastObjectHit);
        lineRenderer.SetPosition(1, new Vector3(0,0, laserLength));        

        float speed = psMain.startSpeed.constant;
        psMain.startLifetime = laserLength/speed;


        // GameObject laserObj = GameObject.Instantiate(laser, laserSpawnPoint.transform.position, laserSpawnPoint.transform.rotation) as GameObject;
        // LaserNew _laserScript = laserObj.GetComponent<LaserNew>();
        // _laserScript.Activate(this._weaponData, this._owner);
        // _laserScript.SetOrigin(laserSpawnPoint.transform.position);

        // Rigidbody laserRB = laserObj.GetComponent<Rigidbody>();
        // laserRB.velocity = laserSpawnPoint.TransformDirection(Vector3.forward*25);
    }

    public override void FireStop(float angle, float distance) {
        LineRenderer lineRenderer = GetComponentInChildren<LineRenderer>();        
        lineRenderer.enabled = false;

        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        var emission =  ps.emission;
        emission.enabled = false;   

        _curChargeTime = 0;
        _lastTickFirstEntity = null;     

        foreach (GameObject vfx in _hitVfxObjects)
        {
            vfx.SetActive(false);
        }
    }


    public void SetVfxObjectCount(int count) 
    {
        if(_hitVfxObjects.Count > count) 
        {
            for(int i=(_hitVfxObjects.Count-count); i>0; i--) 
            {
                GameObject.Destroy(_hitVfxObjects[i]);
            }
        }
        else 
        {
            for(int i=(count-_hitVfxObjects.Count); i>0; i--) 
            {
                _hitVfxObjects.Add(GameObject.Instantiate(hitVfxPrefab, Vector3.zero, Quaternion.identity));
            }
        }
    }
}
