using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour 
{
    protected float _maxHealth = 100;
	protected float _curHealth = 100;

    [SerializeField]
    protected float movementSpeed;

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
        return this.transform.Find("Weapon").gameObject;
    }

    public Weapon GetWeaponComponent() {
        return this.transform.Find("Weapon").GetComponent<Weapon>();
    }
    
	public abstract void Die();
    // public void ApplyEffect(Effect effect);
	
    // public event Action <float> onHealthChanged = delegate{};
}