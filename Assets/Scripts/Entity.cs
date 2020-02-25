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
        
        string weaponId = weaponData.weaponId;
        string weaponPath = "Prefabs/" + weaponId;
        return (GameObject)Instantiate(Resources.Load(weaponPath));
        //1. Instantiate weapon prefab (based on weaponId) and attach as child
        //2. Update weapon's weaponData
        //3. Assign game object to equipped weapon
        
    }

	// public List<Effect> effects; //immunity, or slow effects
	
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

    public void TakeDamage(float damage)
    {
        _curHealth -= damage; //input how much to decrease by-- POSITIVE VALUE        

        if(_curHealth <= 0) {
            this.Die();
        }
    }	

    public void Heal(float heal)
    {
        _curHealth += heal;
    }

    public GameObject GetWeaponGameObject() {
        for (int i = 0; i < this.transform.Find("WeaponSlot").transform.childCount; i++)
        {
            if(this.transform.Find("WeaponSlot").transform.GetChild(i).gameObject.activeSelf == true)
            {
                return this.transform.Find("WeaponSlot").transform.GetChild(i).gameObject;
            }
        }
        return null;
    }


    public Weapon GetWeaponComponent() {
        return GetWeaponGameObject().GetComponent<Weapon>();
    }
    
	public abstract void Die();
    // public void ApplyEffect(Effect effect);
	
    // public event Action <float> onHealthChanged = delegate{};
}