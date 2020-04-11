using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Boss : Enemy
{
    public enum BossState 
    {
        CHASING,
        CHARGING,
        ATTACKING
    }

    Animator animator;

    private float maxAttackDuration = 1;
    private float curAttackDuration = 0;
    private float attackAngleArc = 60; //60 degrees laser    
    private float randomDirectionCoefficient = 1; //decides if it is clockwise or counter
    private Vector3 _laserDirectionCenter = new Vector3(-999,-999,-999);
    private Vector3 NULL_VECTOR3 = new Vector3(-999,-999,-999);

    private bool attackExecuted = false;
    
    private float maxAttackCooldown = 2;
    private float curAttackCooldown = 0;

    public BossState state = BossState.CHASING;
    private float chargeAttackRange = 6.5f; //if below threshold will start charging attack

    private float animationChargeSpeed = 0.4f;
    private float animationAttackSpeed = 0.8f;

    
    public override void Initialize() 
    {
        animator = GetComponentInChildren<Animator>();
    }

    //Called once per frame
    protected override void MainBehaviour()
    {        
        Weapon weaponComponent = GetEquippedWeaponComponent();
        float weaponRange = weaponComponent.GetWeaponRange();
        
        curAttackCooldown -= Time.deltaTime;
        switch (state)
        {
            case BossState.CHASING:
                RotateToTarget();
                Chase();

                if (ShouldStartCharging()) 
                {
                    state = BossState.CHARGING;
                    SetNavMeshAgentEnabled(false);
                    animator.SetBool("attack", true);
                    animator.SetFloat("chargeSpeed", animationChargeSpeed);
                }
                
                break;

            case BossState.CHARGING: 
                //animation trigger sets next state
                //attack starts after charging animation finishes
                break;

            case BossState.ATTACKING:       

                if(attackExecuted) 
                {
                    curAttackDuration += Time.deltaTime;

                    Attack();

                    if(ShouldFinishAttack()) 
                    {
                        FinishAttack();
                    }
                }              


                break;
            
        }
    }

    bool ShouldStartCharging() 
    {
        return Vector3.Distance(_target.transform.position, this.transform.position) < chargeAttackRange && curAttackCooldown <= 0;
    }

    new void Chase() 
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

    protected override void OnAnimationExecute(string key) {        
        if(key == "attack") 
        {       
            if(this.state == BossState.CHARGING)
            {
                animator.SetFloat("chargeSpeed", animationAttackSpeed);              
            }
            else if(this.state == BossState.ATTACKING)
            {
                attackExecuted = true;
                //freeze animation                                    
                animator.SetFloat("attackSpeed", 0.02f);
            }            
        }
        else if (key == "walk") 
        {
            Camera.main.GetComponent<StressReceiver>().InduceStress(0.2f);
        }
    }
    

    protected override void OnAnimationEnd(string key) {        
        if(key == "attack") 
        {   
            //finished chargin
            if(this.state == BossState.CHARGING) 
            {
                curAttackDuration = 0;
                attackExecuted = false;
                float[] coeffValues = {-1,1};        
                randomDirectionCoefficient = coeffValues[Random.Range(0, 2)];
                this.state = BossState.ATTACKING; //go on to next state
                animator.SetFloat("attackSpeed",animationAttackSpeed);                                
            }
            else if (this.state == BossState.ATTACKING) 
            {                
                curAttackCooldown = maxAttackCooldown; //start cd
                _laserDirectionCenter = NULL_VECTOR3;
                animator.SetBool("attack", false);                
                this.state = BossState.CHASING;
            }
        }
    }

    
    new void Attack(){                  
        GameObject weapon = GetEquippedWeaponGameObject();
        
        if(_target == null)
        {
            return;
        }

        if(_laserDirectionCenter == NULL_VECTOR3) {
            _laserDirectionCenter = _target.transform.position - weapon.transform.position;
            _laserDirectionCenter = new Vector3(_laserDirectionCenter.x,0,_laserDirectionCenter.z);
        }        
        
        Camera.main.GetComponent<StressReceiver>().InduceStress(0.1f);

        //to get downward angle
        float y = weapon.transform.position.y;
        float horizontalBuffer = 3;
        float x = (_target.transform.position - weapon.transform.position).magnitude + horizontalBuffer;
        float angle = Mathf.Asin(y/x) / Mathf.PI * 180;        
            
        Quaternion horizontalOffset = Quaternion.Euler(0, Mathf.Lerp(randomDirectionCoefficient*-attackAngleArc/2, randomDirectionCoefficient*attackAngleArc/2, curAttackDuration/maxAttackDuration), 0);
        Quaternion verticalOffset = Quaternion.Euler(angle, 0, 0);
        weapon.transform.rotation = Quaternion.LookRotation(_laserDirectionCenter) * horizontalOffset * verticalOffset; 
        GetEquippedWeaponComponent().AttemptFire(angle,0);
    }

    bool ShouldFinishAttack() 
    {
        return curAttackDuration >= maxAttackDuration;   
    }

        //called when curAttackTime > maxAttackTime
    void FinishAttack() 
    {
        //unfreeze animation 
        //continue attack animation till complete               
        animator.SetFloat("attackSpeed", animationAttackSpeed);              
        GetEquippedWeaponComponent().FireStop(0,0); 
    }

    public override void Die() 
    {
        isAlive = false;
        Debug.Log($"Drop: {dnaWorth}");
        if(dnaWorth != 0) {
            DropDna();
        }
        ScoreManager.GetInstance().OnEnemyDied(this);
        WaveManager.GetInstance().OnEnemyDied(this);
        GameObject.Destroy(gameObject);
    }

    protected override void DropDna() 
    {
        int numberOfDna = 35;
        float spreadRange = 5;
        float eachDnaWorth = Mathf.Round(dnaWorth/numberOfDna);

        for(int i=0; i<numberOfDna; i++) 
        {
            Vector3 dropPosition = this.transform.position + Quaternion.Euler(0, Random.Range(0, 359), 0) * new Vector3(0, 0, Random.Range(0,spreadRange));
            GameObject dnaObject = GameObject.Instantiate(dnaPrefab, dropPosition , Quaternion.identity);
            dnaObject.GetComponent<DnaItem>().SetWorth(eachDnaWorth);
        }        
    }
}
