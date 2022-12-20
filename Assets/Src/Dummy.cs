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

  public override void Die(TakeDamageInfo damageInfo)
  {
  }

}
