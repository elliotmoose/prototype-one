using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
  public float aggroRange = 7;
  public Entity target;

  void Start()
  {
    Initialize();
  }

  public virtual void Initialize()
  {

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

    Vector3 moveDir = target.transform.position - this.transform.position;
    Vector3 position = this.transform.position;
    Vector3 delta3 = new Vector3(moveDir.x, 0, moveDir.z).normalized;
    Quaternion rotation = this.transform.rotation;
    this.transform.rotation = Quaternion.LookRotation(delta3);

    GetComponent<CharacterController>().Move(delta3 * this.movementSpeed * Time.fixedDeltaTime);
  }

  public override void Die()
  {
  }

}
