using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
   
    public GameObject moveJoystick;
    public GameObject attackJoystick;
    
    public float dnaAmount = 0;    
    
    private Joystick _moveJoystickComponent;
    private Joystick _attackJoystickComponent;

    // Start is called before the first frame update
    void Start()
    {
        _moveJoystickComponent = moveJoystick.GetComponent<Joystick>();
        _attackJoystickComponent = attackJoystick.GetComponent<Joystick>();
        _moveJoystickComponent.joystickMovedEvent += UpdatePlayerPosition;
        _attackJoystickComponent.joystickMovedEvent += RotatePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        GetWeaponComponent().SetWeaponHighlighted(_attackJoystickComponent.isActive);
        
        if(_attackJoystickComponent.isActive) {
            GetWeaponComponent().Fire();
        }
    }

    private void UpdatePlayerPosition(float angle)
    {
        Vector3 position = this.transform.position;
        Quaternion rotation = this.transform.rotation;
        Vector3 delta = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
        this.transform.position += delta * Time.deltaTime * this.movementSpeed;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

    }

    private void RotatePlayer(float angle)
    {
        Quaternion rotation = this.transform.rotation;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    override public void Die() 
    {
        //gameover
    }

    public void AddDna(float amount) {
        dnaAmount += amount;
    }
}
