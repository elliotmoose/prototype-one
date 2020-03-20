using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Boss : Enemy
{
    private enum BossState 
    {
        CHASING,
        ATTACKING
    }

    private float maxAttackTime = 1;
    private float curAttackTime = 0;
    private float maxAttackCooldown = 3;
    private float curAttackCooldown = 0;
    private BossState state = BossState.CHASING;
    private float startAttackThreshold = 6; //if below threshold will start charging attack

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

                if (ShouldStartAttack())
                {
                    StartAttack();
                }
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

    bool ShouldStartAttack() 
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

    void StartAttack() 
    {
        SetNavMeshAgentEnabled(false);                
        SetAttackAnimation(true);
        curAttackTime = 0;
        this.state = BossState.ATTACKING;
    }

    protected override void OnAnimationExecute(string key) {        
        if(key == "attack") 
        {
            // GetComponentInChildren<Animator>().speed = 0;
            this.Attack();
        }
    }
    
    void FinishAttack() 
    {
        // GetComponentInChildren<Animator>().speed = 1;
    }

    protected override void OnAnimationEnd(string key) {        
        if(key == "attack") 
        {   
            //finish attack
            this.state = BossState.CHASING;
            SetAttackAnimation(false);
            // SetNavMeshAgentEnabled(true);            
        }
    }

    void Attack(){          
        curAttackCooldown = maxAttackCooldown;
        GetEquippedWeaponComponent().AttemptFire();
    }

    bool ShouldFinishAttack() 
    {
        return curAttackTime >= maxAttackTime;   
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

    void SetAttackAnimation(bool attacking) 
    {
        Animator animator = GetComponentInChildren<Animator>();        
        animator.SetBool("attack", attacking);
    }
}
