using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerAnimEventDelegate();
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

  public void OnAttackKing()
  {
    if (OnAttackKingEvent != null)
    {
      OnAttackKingEvent();
    }
  }
  public void OnAttackStart()
  {
    if (OnAttackStartEvent != null)
    {
      OnAttackStartEvent();
    }
  }
  public void OnAttackEnd()
  {
    if (OnAttackEndEvent != null)
    {
      OnAttackEndEvent();
    }
  }
}
