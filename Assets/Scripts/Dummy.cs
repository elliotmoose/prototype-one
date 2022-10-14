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

  public virtual void Initialize()
  {

    this.OnTakeDamageEvent += (TakeDamageInfo damageInfo) =>
    {
      Debug.Log(damageInfo.effectiveDamage);
      Debug.Log(_curHealth);
    };
  }


  // Update is called once per frame
  void Update()
  {
    UpdateEffects();
  }

  public override void Die()
  {
  }

}
