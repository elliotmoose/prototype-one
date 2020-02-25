using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Entity
{
   
    public GameObject moveJoystick;
    public GameObject attackJoystick;
    
    public float dnaAmount = 0;    
    
    private Joystick _moveJoystickComponent;
    private Joystick _attackJoystickComponent;
    private NavMeshAgent _navMeshAgent;

    private List<InventoryItem> inventory = new List<InventoryItem>();

    private bool _isAttacking = false;

    void Start()
    {
        _moveJoystickComponent = moveJoystick.GetComponent<Joystick>();
        _attackJoystickComponent = attackJoystick.GetComponent<Joystick>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _moveJoystickComponent.joystickMovedEvent += UpdatePlayerPosition;
        _attackJoystickComponent.joystickMovedEvent += Attack;
        _attackJoystickComponent.joystickReleasedEvent += StopAttack;
    }

    void Update()
    {
        GetWeaponComponent().SetWeaponHighlighted(_attackJoystickComponent.isActive);
        
    }

    private void UpdatePlayerPosition(float angle)
    {
        Vector3 position = this.transform.position;
        Vector3 delta = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
        if (_navMeshAgent.enabled)
        {
            _navMeshAgent.speed = this.movementSpeed;
            position += (delta * 10);
            _navMeshAgent.destination = position;
        }
        else
        {
            StartCoroutine(SetNavMeshAgentEnabled(true));
        }
        //this.transform.position += delta * Time.deltaTime * this.movementSpeed;

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

    IEnumerator SetNavMeshAgentEnabled(bool enabled)
    {
        if (_navMeshAgent.enabled != enabled)
        {
            //_navMeshObstacle.enabled = false;
            _navMeshAgent.enabled = false;
            //_navMeshObstacle.enabled = !enabled;
            yield return new WaitForSeconds(0.01f);
            _navMeshAgent.enabled = enabled;
        }
    }
}
