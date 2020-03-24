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

    private float maxAttackTime = 1;
    private float curAttackTime = 0;

    private float maxChargeTime = 2;
    private float curChargeTime = 0;
    
    private float maxAttackCooldown = 3;
    private float curAttackCooldown = 0;

    public BossState state = BossState.CHASING;
    private float startAttackThreshold = 6; //if below threshold will start charging attack

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
                    curChargeTime = 0;
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
                curAttackTime += Time.deltaTime;
                if(ShouldFinishAttack()) 
                {
                    FinishAttack();
                }

                break;
            
        }
    }

    bool ShouldStartCharging() 
    {
        return Vector3.Distance(_target.transform.position, this.transform.position) < startAttackThreshold && curAttackCooldown <= 0;
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

    protected override void OnAnimationExecute(string key) {        
        if(key == "attack") 
        {       
            Debug.Log("ATTACK PEAK state: " + this.state.ToString());                 
            if(this.state == BossState.ATTACKING)
            {
                //freeze animation                                    
                animator.SetFloat("attackSpeed", 0.02f);
                curAttackTime = 0;
            }            
        }
    }
    


    protected override void OnAnimationEnd(string key) {        
        if(key == "attack") 
        {   
            Debug.Log("ANIMATION ENDED IN STATE" + this.state.ToString());
            //finished chargin
            if(this.state == BossState.CHARGING) 
            {
                Debug.Log("Charge anim end");
                curAttackTime = 0;
                this.state = BossState.ATTACKING; //go on to next state
                animator.SetFloat("attackSpeed",animationAttackSpeed);                                
            }
            else if (this.state == BossState.ATTACKING) 
            {
                Debug.Log("Attack anim end");
                this.state = BossState.CHASING;
                animator.SetBool("attack", false);
                Debug.Log("ANIMATION ENDED");
            }
        }
    }

    void Attack(){                  
        GetEquippedWeaponComponent().AttemptFire();
    }

    bool ShouldFinishAttack() 
    {
        return curAttackTime >= maxAttackTime;   
    }

        //called when curAttackTime > maxAttackTime
    void FinishAttack() 
    {
        //unfreeze animation 
        //continue attack animation till complete
        curAttackCooldown = maxAttackCooldown; //start cd
        Debug.Log("start attack animation outro");
        animator.SetFloat("attackSpeed", animationAttackSpeed);              
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

    // void DropDna() 
    // {
    //     GameObject dnaObject = GameObject.Instantiate(dnaPrefab, this.transform.position, Quaternion.identity);
    //     dnaObject.GetComponent<DnaItem>().SetWorth(dnaWorth);
    // }
}
