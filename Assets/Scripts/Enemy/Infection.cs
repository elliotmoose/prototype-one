using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Infection : Enemy 
{
    public float pounceRange = 3f;
    public float infectionRange = 0.1f;
    private bool _isPouncing = false;

    protected override void MainBehaviour() 
    {
        if (Vector3.Distance(_target.transform.position, this.transform.position) < pounceRange)
        {
            _isPouncing = true;
            SetNavMeshAgentEnabled(false);
        }
        else 
        {
            Chase();
        }

        if(_isPouncing) 
        {
            Pounce();

            if (Vector3.Distance(_target.transform.position, this.transform.position) < infectionRange)
            {
                Infect();
            }
        }
    }

    public override void LoadFromEnemyData(EnemyGroupData enemyGroupData) 
    {
        this.SetMovementSpeed(enemyGroupData.movementSpeed);
        this.SetMaxHealth(enemyGroupData.health);
        this.dnaWorth = enemyGroupData.dnaWorth;
        this.scoreWorth = enemyGroupData.scoreWorth;
        this.type = enemyGroupData.type;
    }

    //jumps on to target, infects them, suicides
    private void Pounce()
    {
        Vector3 vToTarget = _target.transform.position - this.transform.position;
        float pounceSpeed = 7;
        this.transform.position += vToTarget.normalized * pounceSpeed * Time.deltaTime;
    }

    private void Infect() 
    {
        if(_target == null) 
        {
            return;
        }


        Entity player = _target.GetComponent<Entity>();
        InfectedEffect slowEffect = new InfectedEffect(player, -0.2f, 3.2f);
        player.TakeEffect(slowEffect);
        
        GameObject.Destroy(this.gameObject);
    }
}