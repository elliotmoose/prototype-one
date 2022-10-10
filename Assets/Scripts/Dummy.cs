using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dummy : Entity
{
  void Start()
  {
    Initialize();
  }

  public virtual void Initialize() { }


  // Update is called once per frame
  void Update()
  {
    UpdateEffects();
  }

  protected override void OnTakeDamage(float damage)
  {
    Debug.Log(damage);
    Debug.Log(_curHealth);
  }

  public override void Die()
  {
  }

}
