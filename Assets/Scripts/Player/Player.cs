using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.InputSystem;

public class Player : Entity
{

  //UI for GameOver Screen
  public Vector3 targetPosition;

  public float dnaAmount = 300;

  private bool _isAttacking = false;

  public WeaponData curWeapon;

  public Material highlightMaterial;

  // controls
  private Vector2 _moveDir = Vector2.zero;
  PlayerControls playerControls;

  // player stats
  private float critChance = 0.5f;

  public float effectiveCritChance
  {
    get
    {
      return critChance;
    }
  }

  void Awake()
  {
    SetMovementSpeed(20);
    EquipWeapon(curWeapon);

    if (PlayerPrefs.GetInt("hack") == 1)
    {
      dnaAmount = 99999;
    }

    playerControls = new PlayerControls();
    playerControls.Player.Move.performed += ctx => _moveDir = ctx.ReadValue<Vector2>();
    playerControls.Player.Move.canceled += ctx => _moveDir = Vector2.zero;

    playerControls.Player.Attack.performed += ctx =>
    {
      this.GetComponentInChildren<Animator>().SetBool("isAttackTrigger", true);
      Attack();
    };


    this.GetComponentInChildren<PlayerAnimationEvents>().OnAttackKingEvent += (int comboIndex) =>
    {
      this.GetComponentInChildren<Animator>().SetBool("isAttackTrigger", false);
      var curWeapon = GetEquippedWeaponComponent();
      if (curWeapon != null)
      {
        curWeapon.AttemptFire(comboIndex);
      }
      else
      {
        Debug.LogWarning("No weapon equipped");
      }
      // playerControls.Player.Attack.canceled += ctx => _isAttacking = false;
    };

    this.GetComponentInChildren<PlayerAnimationEvents>().OnAttackStartEvent += (int comboIndex) =>
    {
      TrailRenderer renderer = this.GetComponentInChildren<TrailRenderer>();
      if (renderer)
      {
        renderer.enabled = true;
      }
    };

    this.GetComponentInChildren<PlayerAnimationEvents>().OnAttackEndEvent += (int comboIndex) =>
    {
      TrailRenderer renderer = this.GetComponentInChildren<TrailRenderer>();
      if (renderer)
      {
        renderer.enabled = false;
      }
    };

    this.OnTakeDamageEvent += (TakeDamageInfo damageInfo) =>
    {
      UIManager.GetInstance().OnPlayerTakeDamage(_curHealth / _maxHealth);
    };
  }

  void OnEnable()
  {
    playerControls.Player.Enable();
  }

  void OnDisable()
  {
    playerControls.Player.Disable();
  }

  void OnDestroy()
  {
    // _moveJoystickComponent.joystickMovedEvent -= UpdatePlayerPosition;
    // _attackJoystickComponent.joystickMovedEvent -= Attack;
    // _attackJoystickComponent.joystickReleasedEvent -= StopAttack;        
  }

  void Update()
  {
    UpdateEffects();

    bool isMoving = _moveDir != Vector2.zero;
    if (isMoving)
    {
      UpdatePlayerPosition(_moveDir);
    }

    this.GetComponentInChildren<Animator>().SetBool("isMoving", isMoving);
  }

  protected override void OnDisabledChanged(bool disabled)
  {
    if (disabled)
    {
      if (_isAttacking)
      {
        StopAttack();
      }
    }
  }

  private void UpdatePlayerPosition(Vector2 delta)
  {
    if (_disabled)
    {
      return;
    }
    Vector3 position = this.transform.position;
    Vector3 delta3 = new Vector3(delta.x, 0, delta.y).normalized;
    Vector3 newPosition = position + delta3 * Time.deltaTime * this._movementSpeed;
    targetPosition = newPosition;
    this.transform.position = newPosition;

    Quaternion rotation = this.transform.rotation;
    this.transform.rotation = Quaternion.LookRotation(delta3);
  }

  private void Attack()
  {
    if (_disabled)
    {
      return;
    }
    // GetEquippedWeaponComponent().AttemptFire(0, 1);
  }

  private void StopAttack()
  {
    this._isAttacking = false;
    GetEquippedWeaponComponent().FireStop();
  }

  override public void Die()
  {
    Time.timeScale = 0;
    //gameover and invoke gameOver screen
    this.gameObject.SetActive(false);
  }

  public void AddDna(float amount)
  {
    dnaAmount += amount;
  }

  public static Player GetInstance()
  {
    GameObject playerObject = GameObject.Find("Player");
    if (playerObject == null)
    {
      return null;
    }

    return playerObject.GetComponent<Player>();
  }
}
