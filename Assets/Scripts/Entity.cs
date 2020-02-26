using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour 
{
    protected float _maxHealth = 100;
	protected float _curHealth = 100;

    [SerializeField]
    protected float movementSpeed;

    protected GameObject _equippedWeapon;
    
    protected GameObject EquipWeapon(WeaponData weaponData) {
        //0. Clean up previously equipped weapon
        if(this._equippedWeapon != null) 
        {
            GameObject.Destroy(_equippedWeapon);
        }

        //1. Instantiate weapon prefab (based on weaponId) and attach as child
        GameObject weaponSlot = GetWeaponSlot();

        if(weaponSlot == null) 
        {
            Debug.LogWarning("EquipWeapon: This entity has no weapon slot");
            return null;
        }
        
        Object weaponPrefab = Resources.Load($"Prefabs/Weapons/{weaponData.weaponId}");
        GameObject newWeaponObject = (GameObject)GameObject.Instantiate(weaponPrefab, weaponSlot.transform.position, weaponSlot.transform.rotation, weaponSlot.transform);
        //2. Update weapon's weaponData
        Weapon weaponComponent = newWeaponObject.GetComponent<Weapon>();
        weaponComponent.Activate(weaponData, this.gameObject);
        //3. Assign game object to equipped weapon
        Debug.Log($"{weaponData.weaponId} equipped");
        this._equippedWeapon = newWeaponObject;
        return newWeaponObject;
        
    }

	// public List<Effect> effects; //immunity, or slow effects
	
    #region Health
    //getter methods 
    public float GetMaxHealth()
    {
        return _maxHealth;
    }
    public float GetCurHealth()
    {
        return _curHealth;
    }

    //setter methods 
	public void SetMaxHealth(float health){
        _maxHealth = health;
    }

    public void Heal(float heal)
    {
        _curHealth += heal;
    }

    public void TakeDamage(float damage)
    {
        _curHealth -= damage; //input how much to decrease by-- POSITIVE VALUE        

        if(_curHealth <= 0) {
            this.Die();
        }
    }	

    public abstract void Die();

    #endregion

    #region Weapon
    
    public GameObject GetWeaponSlot() 
    {
        return this.transform.Find("WeaponSlot").gameObject;
    }

    public GameObject GetEquippedWeaponGameObject() 
    {
        for (int i = 0; i < this.transform.Find("WeaponSlot").transform.childCount; i++)
        {
            if(this.transform.Find("WeaponSlot").transform.GetChild(i).gameObject.activeSelf == true)
            {
                return this.transform.Find("WeaponSlot").transform.GetChild(i).gameObject;
            }
        }
        return null;
    }


    public Weapon GetEquippedWeaponComponent() {
        GameObject weaponGameObject = GetEquippedWeaponGameObject();
        if(weaponGameObject == null)
        {
            Debug.LogWarning("GetWeaponComponent: No weapon gameobject on this entity");
            return null;
        }

        return weaponGameObject.GetComponent<Weapon>();        
    }

    public WeaponData GetEquippedWeaponData() 
    {
        Weapon weaponComponent = GetEquippedWeaponComponent();
        if(weaponComponent == null)
        {
            Debug.LogWarning("GetWeaponData: No weapon component on this entity");
            return null;
        }

        return weaponComponent.GetWeaponData();
    }
    
    #endregion
    

    // public void ApplyEffect(Effect effect);
	
    // public event Action <float> onHealthChanged = delegate{};
}