abstract class Entity : MonoBehaviour 
{
    private float _maxHealth;
	private float _curHealth;
    private float _curHealthPct; //percentage of health left 
    float movementSpeed;

	public List<Effect> effects; //immunity, or slow effects
	
    //getter methods 
    public float GetMaxHealth()
    {
        return _maxHealth;
    }
    public float GetCurHealth()
    {
        return _curHealth;
    }

    public float GetCurHealthPct() 
    {
        return _curHealthPct;
    }

    //setter methods 
	public void SetMaxHealth(float health){
        _maxHealth = health;
    }

    public void TakeDamage(float damage)
    {
        _curHealth -= damage; //input how much to decrease by-- POSITIVE VALUE
        _curHealthPct = (float) _curHealth/ (float) _maxHealth;    
    }	

    public void Heal(float heal)
    {
        _curHealth += heal;
        _curHealthPct = (float) _curHealth/ (float) _maxHealth;    
    }

	public virtual void Die();
    public void ApplyEffect(Effect effect);
	
    public event Action <float> onHealthChanged = delegate{};

    private void OnEnable()
    {
        _curHealth = _maxHealth;
    }

    
}