using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlsManager : EventTrigger
{
    Joystick moveJoystick;
    Joystick attackJoystick;

    private static Vector3 HIDE_VECTOR_POSITION = new Vector3(-100, -100, -100);
    private static int NO_POINTER_INT = -999;
    private int movePointerId = NO_POINTER_INT;
    private int attackPointerId = NO_POINTER_INT;

    void Start() {
        moveJoystick = GameObject.Find("MoveJoystick").GetComponent<Joystick>();
        attackJoystick = GameObject.Find("AttackJoystick").GetComponent<Joystick>();;        
    }

    public override void OnDrag(PointerEventData touch)
    {
        if(touch.pointerId == movePointerId) {
            moveJoystick.OnDrag(touch);
        }

        if(touch.pointerId == attackPointerId) {
            attackJoystick.OnDrag(touch);
        }
    }

    public override void OnPointerUp(PointerEventData touch)
    {
        if(touch.pointerId == movePointerId) {
            moveJoystick.OnPointerUp(touch);
            movePointerId = NO_POINTER_INT;
            moveJoystick.transform.position = HIDE_VECTOR_POSITION;
        }

        if(touch.pointerId == attackPointerId) {
            attackJoystick.OnPointerUp(touch);
            attackPointerId = NO_POINTER_INT;
            attackJoystick.transform.position = HIDE_VECTOR_POSITION;
        }
    }

    public override void OnInitializePotentialDrag(PointerEventData touch)
    {
        bool isLeft = (touch.position.x < Screen.width / 2);
        //left side set joystick position
        if (isLeft)
        {   
            if(movePointerId == NO_POINTER_INT) {
                moveJoystick.transform.position = new Vector3(touch.position.x, touch.position.y, 0);
                moveJoystick.OnInitializePotentialDrag(touch);
                movePointerId = touch.pointerId;
            }
        }
        else
        {
            if(attackPointerId == NO_POINTER_INT) {
                attackJoystick.transform.position = new Vector3(touch.position.x, touch.position.y, 0);
                attackJoystick.OnInitializePotentialDrag(touch);
                attackPointerId = touch.pointerId;
            }
        }
    }
}
