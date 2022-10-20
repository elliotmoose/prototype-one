using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName = "Knockback", menuName = "ScriptableObjects/KnockbackModifier", order = 3)]
public class KnockbackModifier : OnHitModifier
{
  public float knockbackDistance = 1;
  public float knockbackDuration = 0.2f;
  public override void OnHit(Weapon weapon, TakeDamageInfo[] takeDamageInfos)
  {
    foreach (var takeDamageInfo in takeDamageInfos)
    {
      takeDamageInfo.receiver.TakeEffect(new KnockbackEffect(takeDamageInfo.attacker, takeDamageInfo.receiver, takeDamageInfo.attacker.transform.position, knockbackDistance, knockbackDuration));
    }
  }
}