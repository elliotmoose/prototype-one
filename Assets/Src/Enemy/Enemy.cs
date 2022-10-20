using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
  public float aggroRange = 7;
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
    Chase();
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

  public override void Die()
  {
    GameObject.Destroy(this.gameObject);
  }

}
