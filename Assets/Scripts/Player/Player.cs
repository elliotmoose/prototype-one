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

    private List<InventoryItem> inventory = new List<InventoryItem>();

    private bool _isAttacking = false;

    public WeaponData[] activeWeapons = new WeaponData[2];
    private int CurrentWp = 0;

    void Awake()
    {
        _moveJoystickComponent = moveJoystick.GetComponent<Joystick>();
        _attackJoystickComponent = attackJoystick.GetComponent<Joystick>();
        _moveJoystickComponent.joystickMovedEvent += UpdatePlayerPosition;
        _attackJoystickComponent.joystickMovedEvent += Attack;
        _attackJoystickComponent.joystickReleasedEvent += StopAttack;        

        SetMovementSpeed(4);
        
        //set 
        activeWeapons[0] = WeaponData.StandardWeaponData();
        activeWeapons[1] = WeaponData.RapidWeaponData();
        EquipWeapon(activeWeapons[0]); //equip first weapon
    }

    void Update()
    {
        Weapon weaponComponent = GetEquippedWeaponComponent();

        if(weaponComponent != null)
        {
            // weaponComponent.SetWeaponHighlighted(_attackJoystickComponent.isActive);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            ChangeEquippedWeapon();
        }
    }

    public void SetWeaponActive(WeaponData weaponData, int slot) 
    {
        if(slot < 0 || slot > activeWeapons.Length-1)
        {
            Debug.LogWarning($"SetWeaponActive: Slot {slot} out of range");
            return;
        }

        activeWeapons[slot] = weaponData;        
    }

    private void ChangeEquippedWeapon(){
        Weapon weaponComponent = GetEquippedWeaponComponent();
        WeaponData equippedWeaponData = weaponComponent.GetWeaponData();

        foreach (WeaponData weaponData in activeWeapons)
        {
            if(equippedWeaponData.type != weaponData.type)
            {
                EquipWeapon(weaponData);
                break;
            }
        }
    }

    private void UpdatePlayerPosition(float angle)
    {
        Vector3 position = this.transform.position;
        Vector3 delta = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
        this.transform.position += delta * Time.deltaTime * this._movementSpeed;

        if(!_isAttacking) {
            Quaternion rotation = this.transform.rotation;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }

    private void Attack(float angle)
    {
        _isAttacking = true;
        Quaternion rotation = this.transform.rotation;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        GetEquippedWeaponComponent().AttemptFire();
    }

    private void StopAttack(float angle)
    {
        this._isAttacking = false;
    }

    override public void Die() 
    {
        //gameover
        Destroy(this.gameObject);
    }

    public void AddDna(float amount) {
        dnaAmount += amount;
    }
}
