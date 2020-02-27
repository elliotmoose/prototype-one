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

    void Start()
    {
        _moveJoystickComponent = moveJoystick.GetComponent<Joystick>();
        _attackJoystickComponent = attackJoystick.GetComponent<Joystick>();
        _moveJoystickComponent.joystickMovedEvent += UpdatePlayerPosition;
        _attackJoystickComponent.joystickMovedEvent += Attack;
        _attackJoystickComponent.joystickReleasedEvent += StopAttack;
    }

    void Update()
    {
        GetWeaponComponent().SetWeaponHighlighted(_attackJoystickComponent.isActive);
        Debug.Log(MapManager.IsInMap(this.transform.position));
    }

    private void UpdatePlayerPosition(float angle)
    {
        Vector3 position = this.transform.position;
        Vector3 delta = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
        Vector3 newPosition = position + delta;
        float mapMaxBounds = 100;
        Vector3 newPositionConstsrained = new Vector3(Mathf.Max(Mathf.Min(newPosition.x, mapMaxBounds),-mapMaxBounds), newPosition.y, Mathf.Max(Mathf.Min(newPosition.z, mapMaxBounds), -mapMaxBounds));
        if (IsInMap(newPosition))
        {
            this.transform.position += delta * Time.deltaTime * this.movementSpeed;
        }

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

    public static bool IsInMap(Vector3 position)
    {
        MapManager mapManager = MapManager.GetInstance();
        Vector3 mapCenter = mapManager.GetMap().transform.position;
        float mapSize = mapManager.mapSize;
        bool isXBounded = Mathf.Abs(position.x - mapCenter.x) < (mapSize * 10) / 2;
        bool isZBounded = Mathf.Abs(position.z - mapCenter.z) < (mapSize * 10) / 2;
        return isXBounded && isZBounded;
    }
}
