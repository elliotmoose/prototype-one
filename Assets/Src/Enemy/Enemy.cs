using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    public float aggroRange = 7;
    public float attackRange = 3;
    float attackCd = 1;
    float curAttackCd = 0;
    public Entity target;
    private NavMeshAgent agent;

    void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        AcquireTargetIfNeeded();

        if (target && Vector3.Distance(target.transform.position, this.transform.position) < attackRange)
        {
            Debug.Log("Attacking");
            TryAttack();
        }
        else
        {
            Chase();
        }

        curAttackCd -= Time.fixedDeltaTime;
    }

    private void AcquireTargetIfNeeded()
    {

        if (target && !target.isAlive) target = null;
        if (target) return;

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, aggroRange);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                target = collider.gameObject.GetComponent<Entity>();
                return;
            }
        }
    }

    private void Chase()
    {
        if (!target) return;
        if (isDisabled) return;
        agent.SetDestination(target.transform.position);
        this.agent.nextPosition = this.transform.position;
        Vector3 moveDir = agent.desiredVelocity.normalized;
        Vector3 delta3 = new Vector3(moveDir.x, 0, moveDir.z).normalized;
        Quaternion rotation = this.transform.rotation;
        this.transform.rotation = Quaternion.LookRotation(delta3);

        var cc = GetComponent<CharacterController>();
        cc.Move(delta3 * this.movementSpeed * Time.fixedDeltaTime);
    }

    private void TryAttack()
    {
        if (!target) return;
        if (isDisabled) return;

        if (curAttackCd <= 0)
        {
            target.TakeDamage(new TakeDamageInfo(this, target, 10, DamageType.NORMAL));
            curAttackCd = attackCd;
        }
    }

    public override void Die(TakeDamageInfo damageInfo)
    {
        GameObject.Destroy(this.gameObject);

        if (damageInfo.attacker.GetComponent<PlayerLevel>())
        {
            damageInfo.attacker.GetComponent<PlayerLevel>().AddExp(100);
        }
    }

}
