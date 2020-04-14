using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlsManager : EventTrigger
{
    public bool joystickShouldAdapt = false;
    Joystick moveJoystick;
    Joystick attackJoystick;

    private Vector3 moveJoystickInitialPos;
    private Vector3 attackJoystickInitialPos;
    private static int NO_POINTER_INT = -999;
    private int movePointerId = NO_POINTER_INT;
    private int attackPointerId = NO_POINTER_INT;
    private float _unselectedJoystickAlpha = 0.4f;

    void Start() {
        //load shouldAdapt
        joystickShouldAdapt = (PlayerPrefs.GetInt("JOYSTICK_SHOULD_ADAPT", 0) == 1);
        UIManager.GetInstance().SetStaticJoystickToggle(!joystickShouldAdapt);


        moveJoystick = GameObject.Find("MoveJoystick").GetComponent<Joystick>();
        attackJoystick = GameObject.Find("AttackJoystick").GetComponent<Joystick>();;        
        moveJoystickInitialPos = moveJoystick.transform.position;
        attackJoystickInitialPos = attackJoystick.transform.position;    
        moveJoystick.SetJoystickShouldAdapt(joystickShouldAdapt);
        attackJoystick.SetJoystickShouldAdapt(joystickShouldAdapt);
        SetJoystickActive(moveJoystick, false);
        SetJoystickActive(attackJoystick, false);
    }

    public void SetJoystickShouldAdapt(bool isStatic)
    {
        PlayerPrefs.SetInt("JOYSTICK_SHOULD_ADAPT", isStatic ? 0 : 1 );
        joystickShouldAdapt = !isStatic;
        moveJoystick.SetJoystickShouldAdapt(joystickShouldAdapt);
        attackJoystick.SetJoystickShouldAdapt(joystickShouldAdapt);
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

    //reset
    public override void OnPointerUp(PointerEventData touch)
    {
        if(touch.pointerId == movePointerId) {
            moveJoystick.OnPointerUp(touch);
            movePointerId = NO_POINTER_INT;

            if(joystickShouldAdapt)
            {
                moveJoystick.transform.position = moveJoystickInitialPos;
            }

            SetJoystickActive(moveJoystick, false);
        }

        if(touch.pointerId == attackPointerId) {
            attackJoystick.OnPointerUp(touch);
            attackPointerId = NO_POINTER_INT;

            if(joystickShouldAdapt)
            {
                attackJoystick.transform.position = attackJoystickInitialPos;
            }
            
            SetJoystickActive(attackJoystick, false);
        }
    }

    private void SetJoystickActive(Joystick Joystick, bool active) {
        Image joystickImage = Joystick.GetComponent<Image>();
        joystickImage.color = new Color(joystickImage.color.r,joystickImage.color.g,joystickImage.color.b, active ? 1 : _unselectedJoystickAlpha);

        Image childJoystickImage = Joystick.transform.GetChild(0).GetComponent<Image>();
        childJoystickImage.color = new Color(childJoystickImage.color.r,childJoystickImage.color.g,childJoystickImage.color.b, active ? 1 : _unselectedJoystickAlpha);
    }

    public override void OnInitializePotentialDrag(PointerEventData touch)
    {
        bool isLeft = (touch.position.x < Screen.width / 2);
        //left side set joystick position
        if (isLeft)
        {   
            if(movePointerId == NO_POINTER_INT) {

                if(joystickShouldAdapt) 
                {
                    moveJoystick.transform.position = new Vector3(touch.position.x, touch.position.y, 0);                    
                }

                moveJoystick.OnInitializePotentialDrag(touch);
                movePointerId = touch.pointerId;
                SetJoystickActive(moveJoystick, true);
            }
        }
        else
        {
            if(attackPointerId == NO_POINTER_INT) {
                
                if(joystickShouldAdapt) 
                {
                    attackJoystick.transform.position = new Vector3(touch.position.x, touch.position.y, 0);                    
                }

                attackJoystick.OnInitializePotentialDrag(touch);
                attackPointerId = touch.pointerId;
                SetJoystickActive(attackJoystick, true);
            }
        }
    }
}
