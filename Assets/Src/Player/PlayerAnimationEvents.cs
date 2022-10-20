using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerAnimEventDelegate(int comboIndex);
public class PlayerAnimationEvents : MonoBehaviour
{
  public event PlayerAnimEventDelegate OnAttackKingEvent;
  public event PlayerAnimEventDelegate OnAttackStartEvent;
  public event PlayerAnimEventDelegate OnAttackEndEvent;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void OnAttackKing(int comboIndex)
  {
    if (OnAttackKingEvent != null)
    {
      OnAttackKingEvent(comboIndex);
    }
  }
  public void OnAttackStart(int comboIndex)
  {
    if (OnAttackStartEvent != null)
    {
      OnAttackStartEvent(comboIndex);
    }
  }
  public void OnAttackEnd(int comboIndex)
  {
    if (OnAttackEndEvent != null)
    {
      OnAttackEndEvent(comboIndex);
    }
  }
}
