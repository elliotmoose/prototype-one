using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.InputSystem;

public class Player : Entity
{
  ScoreManager score;

  //UI for GameOver Screen
  public Vector3 targetPosition;

  public float movementSpeed = 10;
  public float dnaAmount = 300;

  private bool _isAttacking = false;

  public WeaponData[] activeWeapons = new WeaponData[2];

  public Material highlightMaterial;

  // controls
  private Vector2 _moveDir = Vector2.zero;
  PlayerControls playerControls;

  void Awake()
  {
    SetMovementSpeed(movementSpeed);
    //set 
    activeWeapons[0] = WeaponData.MeleeWeaponData();
    EquipWeapon(activeWeapons[0]);

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


    this.GetComponentInChildren<PlayerAnimationEvents>().OnAttackKingEvent += () =>
    {
      this.GetComponentInChildren<Animator>().SetBool("isAttackTrigger", false);
      var curWeapon = GetEquippedWeaponComponent();
      if (curWeapon != null)
      {
        curWeapon.AttemptFire();
      }
      else
      {
        Debug.LogWarning("No weapon equipped");
      }
      // playerControls.Player.Attack.canceled += ctx => _isAttacking = false;
    };

    this.GetComponentInChildren<PlayerAnimationEvents>().OnAttackStartEvent += () =>
    {
      TrailRenderer renderer = this.GetComponentInChildren<TrailRenderer>();
      if (renderer)
      {
        renderer.enabled = true;
      }
    };

    this.GetComponentInChildren<PlayerAnimationEvents>().OnAttackEndEvent += () =>
    {
      TrailRenderer renderer = this.GetComponentInChildren<TrailRenderer>();
      if (renderer)
      {
        renderer.enabled = false;
      }
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

  #region Weapon
  public void SetWeaponActive(WeaponData weaponData, int slot)
  {
    if (slot < 0 || slot > activeWeapons.Length - 1)
    {
      Debug.LogWarning($"SetWeaponActive: Slot {slot} out of range");
      return;
    }

    activeWeapons[slot] = weaponData;
  }

  public WeaponData GetActiveWeaponAtIndex(int i)
  {
    if (i < activeWeapons.Length && i >= 0)
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

  public void ChangeEquippedWeapon()
  {
    Weapon weaponComponent = GetEquippedWeaponComponent();
    WeaponData equippedWeaponData = weaponComponent.GetWeaponData();

    bool changed = false;
    foreach (WeaponData weaponData in activeWeapons)
    {
      if (weaponData != null && equippedWeaponData.type != weaponData.type)
      {
        changed = true;
        EquipWeapon(weaponData);
        NotificationManager.GetInstance().Notify($"Equipped {weaponData.name}");
        break;
      }
    }

    if (!changed)
    {
      NotificationManager.GetInstance().Notify("No weapon to change to. Buy a weapon in shop");
    }
  }

  #endregion
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

  protected override void OnTakeDamage(float damage)
  {
    ScoreManager.GetInstance().UpdateHighscoreIfNeeded();
    UIManager.GetInstance().OnPlayerTakeDamage(_curHealth / _maxHealth);
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
