using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    public GameObject vfxPrefab;
    public Material vfxMaterial;
    public GameObject dnaPrefab;

    // Start is called before the first frame update
    public bool isAlive = true;
    public float dnaWorth = 20f; //worth in dna
    public float scoreWorth = 20f; //worth in score
    public EnemyType type = EnemyType.BACTERIA;


    protected NavMeshAgent _navMeshAgent;
    protected NavMeshObstacle _navMeshObstacle;

    protected GameObject _target;

    IEnumerator navMeshCoroutine;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        LinkAnimationEvents();
        Initialize();
    }

    public virtual void Initialize(){}
        
    public void LoadFromEnemyData(EnemyGroupData enemyGroupData) 
    {
        this.SetMovementSpeed(enemyGroupData.movementSpeed);
        this.SetMaxHealth(enemyGroupData.health);
        this.dnaWorth = enemyGroupData.dnaWorth;
        this.scoreWorth = enemyGroupData.scoreWorth;
        this.type = enemyGroupData.type;

        WeaponData weaponData = WeaponData.NewWeaponDataForType(enemyGroupData.weaponType);
        weaponData.SetAttackPropertyValue("DAMAGE", enemyGroupData.damage);
        //TODO: apply damage increment here                        
        this.EquipWeapon(weaponData); //attach weapon
    }

    // Update is called once per frame
    void Update()
    {
        FindTargetIfNeeded();
        UpdateEffects();
        MainBehaviour();
    }

    protected void FindTargetIfNeeded() 
    {
        if(this._target == null)
        {
            this._target = GameObject.Find("Player");
        }
    }

    protected virtual void MainBehaviour() 
    {        
        
        Weapon weaponComponent = GetEquippedWeaponComponent();
        float weaponRange = weaponComponent.GetWeaponRange();
        RotateToTarget();

        if(_target == null) 
        {
            return;
        }
        
        if (Vector3.Distance(_target.transform.position, this.transform.position) < weaponRange)
        {
            Attack();
        }
        else 
        {
            Chase();
        }
    }

    void SetTarget(GameObject target)
    {
        this._target = target;
    }

    protected override void OnDisabledChanged(bool disabled) {
        SetNavMeshAgentEnabled(!disabled);
    }

    void Chase() 
    {   
        if(_disabled) {
            return;
        }

        if(_navMeshAgent.enabled) {
            _navMeshAgent.speed = this._movementSpeed;        
            _navMeshAgent.destination = _target.transform.position;
        }
        else {
            SetNavMeshAgentEnabled(true);       
        }
    }

    void Attack(){
        if(_disabled) {
            return;
        }

        SetNavMeshAgentEnabled(false);                
        GetEquippedWeaponComponent().AttemptFire();
    }

    protected void RotateToTarget()
    {
        if (_disabled)
        {
            return;
        }

        if (_target == null)
        {
            return;
        }

        Quaternion rotation = Quaternion.LookRotation(_target.transform.position - this.transform.position, Vector3.up);
        this.transform.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
    }

    protected override void OnTakeDamage(float damage)
    {
        //if it overshot, compensate        
        if(type ==  EnemyType.INFECTION) 
        {
            return;
        }
        WaveManager.GetInstance().OnEnemyTakeDamage(damage + Mathf.Min(_curHealth, 0));
    }

    public override void Die() 
    {
        isAlive = false;
        if(dnaWorth != 0) {
            DropDna();
        }

        if(vfxPrefab != null && vfxMaterial != null) 
        {
            GameObject deathVfx = GameObject.Instantiate(vfxPrefab, transform.position, transform.rotation);
            deathVfx.GetComponent<ParticleSystemRenderer>().material = vfxMaterial;
            var main = deathVfx.GetComponent<ParticleSystem>().main;                    
            GameObject.Destroy(deathVfx, main.startLifetime.constant);
        }

        ScoreManager.GetInstance().OnEnemyDied(this);
        WaveManager.GetInstance().OnEnemyDied(this);
        GameObject.Destroy(gameObject);
    }

    protected virtual void DropDna() 
    {
        GameObject dnaObject = GameObject.Instantiate(dnaPrefab, this.transform.position, Quaternion.identity);
        dnaObject.GetComponent<DnaItem>().SetWorth(dnaWorth);
    }

    //this has to be staggered so that the enemy won't teleport when the agent is reactivated
    protected void SetNavMeshAgentEnabled(bool enabled) {
        // if(navMeshCoroutine != null) {
        //     StopCoroutine(navMeshCoroutine);
        // }
        
        navMeshCoroutine = _SetNavMeshAgentEnabled(enabled);
        StartCoroutine(navMeshCoroutine);
    }
    IEnumerator _SetNavMeshAgentEnabled(bool enabled) {
        if(_navMeshAgent.enabled != enabled) 
        {
            _navMeshObstacle.enabled = false;
            _navMeshAgent.enabled = false;            
            _navMeshObstacle.enabled = !enabled;
            yield return new WaitForSeconds(0.01f);
            _navMeshAgent.enabled = enabled;
            yield break;
        }
    }
}
