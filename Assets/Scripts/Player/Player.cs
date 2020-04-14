using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Player : Entity
{   
    ScoreManager score;
   
    public GameObject moveJoystick;
    public GameObject attackJoystick;
    public GameObject gameOverScreen;

    //UI for GameOver Screen
    

    public Vector3 targetPosition;
    
    public float dnaAmount = 300;    
    
    private Joystick _moveJoystickComponent;
    private Joystick _attackJoystickComponent;

    private bool _isAttacking = false;

    public WeaponData[] activeWeapons = new WeaponData[2];

    public GameObject GameOverScreen { get => gameOverScreen; set => gameOverScreen = value; }

    public Material highlightMaterial;

    void Awake()
    {
        _moveJoystickComponent = moveJoystick.GetComponent<Joystick>();
        _attackJoystickComponent = attackJoystick.GetComponent<Joystick>();
        _moveJoystickComponent.joystickMovedEvent += UpdatePlayerPosition;
        _attackJoystickComponent.joystickMovedEvent += Attack;
        _attackJoystickComponent.joystickReleasedEvent += StopAttack;        

        SetMovementSpeed(4.5f);
        //set 
        activeWeapons[0] = WeaponData.StandardWeaponData();
        // activeWeapons[1] = WeaponData.StandardWeaponData();
        EquipWeapon(activeWeapons[0]); //equip first weapon

    }

    void OnDestroy()
    {
        _moveJoystickComponent.joystickMovedEvent -= UpdatePlayerPosition;
        _attackJoystickComponent.joystickMovedEvent -= Attack;
        _attackJoystickComponent.joystickReleasedEvent -= StopAttack;        
    }

    void Update()
    {
        UpdateEffects();    
        if (Input.GetKeyUp(KeyCode.X))
        {
            ChangeEquippedWeapon();
        }
    }

    protected override void OnDisabledChanged(bool disabled) {
        if(disabled) {
            if(_isAttacking) {
                StopAttack(0,0);
            }
        }
    }

    #region Weapon
    public void SetWeaponActive(WeaponData weaponData, int slot) 
    {
        if(slot < 0 || slot > activeWeapons.Length-1)
        {
            Debug.LogWarning($"SetWeaponActive: Slot {slot} out of range");
            return;
        }

        activeWeapons[slot] = weaponData;        
    }

    public WeaponData GetActiveWeaponAtIndex(int i) 
    {
        if(i < activeWeapons.Length && i >= 0) 
        {
            return activeWeapons[i];
        }
        else 
        {
            Debug.LogWarning($"GetActiveWeaponAtIndex: attempted to get weapon of invalid index {i}");
            return null;
        }
    }

    public bool OwnsWeaponOfType(WeaponType type) 
    {
        return (activeWeapons[0] != null && activeWeapons[0].type == type) || (activeWeapons[1] != null && activeWeapons[1].type == type);
    }
    
    public void ChangeEquippedWeapon(){
        Weapon weaponComponent = GetEquippedWeaponComponent();
        WeaponData equippedWeaponData = weaponComponent.GetWeaponData();

        bool changed = false;
        foreach (WeaponData weaponData in activeWeapons)
        {
            if(weaponData != null && equippedWeaponData.type != weaponData.type)
            {
                changed = true;
                EquipWeapon(weaponData);
                break;
            }
        }

        if(!changed) 
        {
            NotificationManager.GetInstance().Notify("No weapon to change to. Buy a weapon in shop");
        }
    }

    #endregion
    private void UpdatePlayerPosition(float angle, float distance)
    {
        if(_disabled) {
            return;
        }
        Vector3 position = this.transform.position;
        Vector3 delta = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
        Vector3 newPosition = position + (delta * Time.deltaTime * this._movementSpeed);
        targetPosition = newPosition;
        MapManager mapManager = MapManager.GetInstance();
        Vector3 mapCenter = mapManager.GetMap().transform.position;
        float mapSize = mapManager.mapSize;
        if (newPosition.x + 0.6f - mapCenter.x > (mapSize * 10) / 2)
        {
            newPosition.x = position.x;
        }
        if (newPosition.x - 0.6f - mapCenter.x < -(mapSize * 10) / 2)
        {
            newPosition.x = position.x;
        }
        if (newPosition.z + 0.6f - mapCenter.z > (mapSize * 10) / 2)
        {
            newPosition.z = position.z;
        }
        if (newPosition.z - 0.6f - mapCenter.z < -(mapSize * 10) / 2)
        {
            newPosition.z = position.z;
        }
        this.transform.position = newPosition;


        if (!_isAttacking) {
            Quaternion rotation = this.transform.rotation;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }

    private void Attack(float angle, float joystickDistanceRatio)
    {
        if(_disabled) {
            return;
        }
        _isAttacking = true;
        Quaternion rotation = this.transform.rotation;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        GetEquippedWeaponComponent().AttemptFire(angle, joystickDistanceRatio);
    }

    private void StopAttack(float angle, float joystickDistanceRatio)
    {
        this._isAttacking = false;
        GetEquippedWeaponComponent().FireStop(angle, joystickDistanceRatio);
    }

    override public void Die() 
    {  
        //gameover and invoke gameOver screen
        Destroy(this.gameObject);
        gameOverScreen.SetActive(true);
        
    }

    public void AddDna(float amount) {
        dnaAmount += amount;
    }

    public static Player GetInstance()
    {
        GameObject playerObject = GameObject.Find("Player");
        if(playerObject == null) 
        {
            return null;
        }

        return playerObject.GetComponent<Player>();
    }
}
