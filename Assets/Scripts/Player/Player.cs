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

    private WeaponData _primaryWeapon;
    private WeaponData _secondaryWeapon;

    public GameObject[] weapons = new GameObject[2];
    public WeaponData[] weaponData = new WeaponData[2];
    private int CurrentWp = 0;

    void Start()
    {
        _moveJoystickComponent = moveJoystick.GetComponent<Joystick>();
        _attackJoystickComponent = attackJoystick.GetComponent<Joystick>();
        _moveJoystickComponent.joystickMovedEvent += UpdatePlayerPosition;
        _attackJoystickComponent.joystickMovedEvent += Attack;
        _attackJoystickComponent.joystickReleasedEvent += StopAttack;
        weaponData[0] =  WeaponData.StandardWeaponData();
        weaponData[1] =  WeaponData.StandardWeaponData();
        Init_Wp();

    }

    void Update()
    {
        GetWeaponComponent().SetWeaponHighlighted(_attackJoystickComponent.isActive);
        Debug.Log(MapManager.IsInMap(this.transform.position));
        if (Input.GetKeyUp(KeyCode.X))
        {
            // WeaponData tisWp = StandardWeaponData();
            // EquipWeapon(tisWp);
            Debug.Log("ChangeWeapon");
            ChangeWeapon();

        }
    }

    public void Init_Wp(){
        for(int i = 0; i < weapons.Length; i++){
            weapons[i] = EquipWeapon(weaponData[i]);
            weapons[i].transform.parent = this.transform.Find("WeaponSlot").gameObject.transform;
            if(i != CurrentWp){
                weapons[i].SetActive(false);
            }
        }
        
    }
    private void ChangeWeapon(){
        for(int i = 0; i < weapons.Length; i++) {
            if(this.transform.Find("WeaponSlot").transform.GetChild(i).gameObject.activeSelf == true){
                this.transform.Find("WeaponSlot").transform.GetChild(i).gameObject.SetActive(false);
            }
            else{
                this.transform.Find("WeaponSlot").transform.GetChild(i).gameObject.SetActive(true);
                CurrentWp = i;
            }
        }
    }

    private void UpdatePlayerPosition(float angle)
    {
        Vector3 position = this.transform.position;
        Vector3 delta = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
        this.transform.position += delta * Time.deltaTime * this.movementSpeed;

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
        GetWeaponComponent().AttemptFire();
    }

    private void StopAttack(float angle)
    {
        this._isAttacking = false;
    }

    override public void Die() 
    {
        //gameover
    }

    public void AddDna(float amount) {
        dnaAmount += amount;
    }
}
