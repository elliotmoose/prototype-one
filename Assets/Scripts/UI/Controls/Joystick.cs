using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void JoystickEvent(float angle);

public class Joystick : EventTrigger
{
    public bool isActive = false;
    
    //settings
    //The joystick must move more than 30% of the radius to active
    //so that it will not active when touch is in center    
    private float _activateThreshold = 0.2f; 
    private float _innerRadius = 60f;
    private float _outerRadius = 140f;

    private Sprite _innerSprite;
    private Sprite _outerSprite;
    private GameObject _innerJoystickObj;

    private float _outputAngle = 0;

    public event JoystickEvent joystickMovedEvent;
    public event JoystickEvent joystickReleasedEvent;

    // Use this for initialization
    void Start()
    {
        // transform.name = "outer";
        SpawnJoystickInner();
        SetJoystickSprites();
        SetJoystickRadius();
    }

    #region Initialize
    private void SpawnJoystickInner()
    {
        _innerJoystickObj = new GameObject("inner");
        _innerJoystickObj.AddComponent<Image>();
        _innerJoystickObj.transform.SetParent(transform);
        _innerJoystickObj.transform.localPosition = Vector3.zero;
    }

    private void SetJoystickSprites()
    {
        _outerSprite = Resources.Load<Sprite>("Sprites/joystick_outer");   
        _innerSprite = Resources.Load<Sprite>("Sprites/joystick_inner"); 
        if (_outerSprite != null)
        {
            gameObject.GetComponent<Image>().sprite = _outerSprite;
        }
        else
        {
            Debug.LogWarning("No joystick_outer found in Resources/Sprites");
        }

        if (_innerSprite != null)
        {
            _innerJoystickObj.GetComponent<Image>().sprite = _innerSprite;
        }
        else
        {
            Debug.LogWarning("No joystick_inner found in Resources/Sprites");
        }

    }

    private void SetJoystickRadius()
    {
        RectTransform outer_rt = GetComponent<RectTransform>();
        outer_rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _outerRadius * 2);
        outer_rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _outerRadius * 2);

        RectTransform inner_rt = _innerJoystickObj.GetComponent<RectTransform>();
        inner_rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _innerRadius * 2);
        inner_rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _innerRadius * 2);

        _innerJoystickObj.transform.localPosition = Vector3.zero;
    }
    #endregion

    public override void OnDrag(PointerEventData touch)
    {        

        Vector2 joystickCenter = transform.position;
        float maxDistance = (_outerRadius - _innerRadius);

        // Uses vectors to find new position
        Vector2 newInnerPosition = joystickCenter - Vector2.ClampMagnitude((joystickCenter - touch.position), maxDistance);

        //set new position
        _innerJoystickObj.transform.position = (Vector3)newInnerPosition;

        //set output angle variables
        Vector2 vectorFromCenterToFinger = newInnerPosition - joystickCenter;
        _outputAngle = Mathf.Rad2Deg * Mathf.Atan2(vectorFromCenterToFinger.x, vectorFromCenterToFinger.y);


        //normalize for 8 point
        float x = Mathf.Sin(_outputAngle * Mathf.Deg2Rad);
        float y = Mathf.Cos(_outputAngle * Mathf.Deg2Rad);

        if (Mathf.Abs(x) > 0.5)
        {
            x = x / Mathf.Abs(x);
        }
        else
        {
            x = 0;
        }

        if (Mathf.Abs(y) > 0.5)
        {
            y = y / Mathf.Abs(y);
        }
        else
        {
            y = 0;
        }
            
        if (vectorFromCenterToFinger.magnitude > _activateThreshold * _outerRadius)
        {            
            isActive = true;            
        }

        if (joystickMovedEvent != null)
        {
            joystickMovedEvent(_outputAngle);            
        }
    }

    public override void OnPointerUp(PointerEventData touch)
    {
        //reset inner position
        _innerJoystickObj.transform.position = transform.position;

        if (joystickReleasedEvent != null)
        {
            joystickReleasedEvent(_outputAngle);            
        }

        //reset output
        isActive = false;
        _outputAngle = 0; 
    }

    public override void OnInitializePotentialDrag(PointerEventData touch)
    {
        this.OnDrag(touch);
    }

    public float GetOutputAngle()
    {
        return this._outputAngle;
    }	
}
