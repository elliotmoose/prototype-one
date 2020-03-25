using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public WeaponType type = WeaponType.NULL; //helps to identify the prefab to spawn when equipped
    public string name = ""; //display name
	public int damageType = 0;
	// public float damage = 0;
	// public float fireRate = 0;
	public float range = 0;
	public float dnaWorth = 0;
    public int weaponLevel = 0;
    public Sprite weaponSprite = null;
    // public List<AttackProperty> attackProperties = new List<AttackProperty>();
    private Dictionary<string, AttackProperty> attackProperties = new Dictionary<string, AttackProperty>();
    public float[] attackUpgradeCost = {};

    public bool CanUpgrade() 
    {
        //check if max already
        return weaponLevel < attackUpgradeCost.Length;
    }

    private float GetNextUpgradeCost() 
    {
        if(weaponLevel > attackUpgradeCost.Length-1)
        {
            return -1;
        }

        return attackUpgradeCost[weaponLevel];
    }

    public UpgradeDescription GetNextUpgradeDescription()
    {
        UpgradeDescription upgradeDescription = new UpgradeDescription();
        upgradeDescription.weaponData = this;
        upgradeDescription.cost = GetNextUpgradeCost();
        upgradeDescription.properties = new List<KeyValuePair<string, string>>();
        foreach(AttackProperty attackProperty in attackProperties.Values)
        {
            //every property has a name and a value description
            //there is also a cost attached to this set of properties (upgrade)
            KeyValuePair<string, string> propertyDescription = new KeyValuePair<string, string>(attackProperty.name, $"{attackProperty.GetValueForWeaponLevel(weaponLevel)} -> {attackProperty.GetValueForWeaponLevel(weaponLevel+1)}");
            upgradeDescription.properties.Add(propertyDescription);
        }

        return upgradeDescription;
    }

    public float GetSellWeaponCost()
    {
        if (weaponLevel == 0)
        {
            return this.dnaWorth;
        }
        float sumUpgradeDnaWorth = 0;
        for (int i = 1; i < weaponLevel+1; i++)
        {
            sumUpgradeDnaWorth += attackUpgradeCost[weaponLevel];
        }
        return sumUpgradeDnaWorth + this.dnaWorth;
    }

    public float GetAttackPropertyValue(string key) {
        AttackProperty attackProperty;
        attackProperties.TryGetValue(key, out attackProperty);
        if(attackProperty != null) {
            return attackProperty.GetValueForWeaponLevel(weaponLevel);
        }
        else {
            Debug.LogError("WeaponData is missing key: " + key);
            return 0;
        }
    }

    public float GetDamage() 
    {
        return GetAttackPropertyValue("DAMAGE");
    }

    public void Upgrade() 
    {
        weaponLevel += 1;
    }

    public static WeaponData NullWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "nil";
        newWeaponData.type = WeaponType.NULL;
        newWeaponData.attackProperties.Add("DAMAGE",new AttackProperty("DAMAGE", "Damage", new float[]{0}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("FIRE_RATE",new AttackProperty("FIRE_RATE", "Fire Rate", new float[]{1}, PropertyRepresentationType.RAW));
        newWeaponData.range = 0;
        newWeaponData.dnaWorth = 0;
        newWeaponData.attackUpgradeCost = new float[]{100, 200};
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/null");
        return newWeaponData;
    }

    public static WeaponData ToxinWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Toxin";
        newWeaponData.type = WeaponType.TOXIN;
        newWeaponData.attackProperties.Add("DAMAGE",new AttackProperty("DAMAGE", "Damage", new float[]{2}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("FIRE_RATE",new AttackProperty("FIRE_RATE", "Fire Rate", new float[]{1}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("BULLET_SPLIT",new AttackProperty("BULLET_SPLIT", "Bullet split", new float[]{1}, PropertyRepresentationType.SPLIT));
        newWeaponData.range = 6.5f;
        newWeaponData.dnaWorth = 0;
        newWeaponData.attackUpgradeCost = new float[]{100, 200};;
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/null");
        return newWeaponData;
    }

    public static WeaponData StandardWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Normie Gun";
        newWeaponData.type = WeaponType.STANDARD;
        newWeaponData.range = 6;
        newWeaponData.dnaWorth = 100;
        newWeaponData.attackProperties.Add("DAMAGE",new AttackProperty("DAMAGE", "Damage", new float[]{35f, 38f, 40f, 42f, 45f, 50f, 60f}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("FIRE_RATE",new AttackProperty("FIRE_RATE", "Fire Rate", new float[]{4f, 4.5f, 4.9f, 5.5f, 6.3f,7.0f, 7.5f}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("BULLET_SPLIT",new AttackProperty("BULLET_SPLIT", "Bullet split", new float[]{1f, 1f, 2f, 2f, 3f, 3f,4f}, PropertyRepresentationType.SPLIT));
        newWeaponData.attackUpgradeCost = new float[]{400, 500,      600, 700,    800, 1200,     1500};
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/Normie");
        return newWeaponData;
    }

    public static WeaponData MeleeWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Fist";
        newWeaponData.type = WeaponType.MELEE;
        newWeaponData.attackProperties.Add("DAMAGE",new AttackProperty("DAMAGE", "Damage", new float[]{2}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("FIRE_RATE",new AttackProperty("FIRE_RATE", "Fire Rate", new float[]{1}, PropertyRepresentationType.RAW));
        newWeaponData.range = 2;
        newWeaponData.dnaWorth = 0;
        newWeaponData.attackUpgradeCost = new float[]{100, 200};
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/null");
        return newWeaponData;
    }

    public static WeaponData MissileWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Rocket Launcher";
        newWeaponData.type = WeaponType.MISSILE;
        newWeaponData.attackProperties.Add("DAMAGE",new AttackProperty("DAMAGE", "Damage", new float[]{50}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("FIRE_RATE",new AttackProperty("FIRE_RATE", "Fire Rate", new float[]{1.1f}, PropertyRepresentationType.RAW));
        newWeaponData.range = 7;
        newWeaponData.dnaWorth = 300;        
        newWeaponData.attackProperties.Add("EXPLOSION_DAMAGE",new AttackProperty("EXPLOSION_DAMAGE", "Explosion Damage", new float[]{0f, 0.5f, 0.7f}, PropertyRepresentationType.PERCENTAGE));
        newWeaponData.attackProperties.Add("EXPLOSION_RADIUS",new AttackProperty("EXPLOSION_RADIUS", "Explosion Radius", new float[]{0f, 2f, 2.8f}, PropertyRepresentationType.RAW));
        newWeaponData.attackUpgradeCost = new float[]{600, 850, 1200};
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/Missle");
        return newWeaponData;
    }
    
    public static WeaponData FlameThrowerWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Flame Thrower";
        newWeaponData.type = WeaponType.FLAMETHROWER;
        newWeaponData.attackProperties.Add("DAMAGE",new AttackProperty("DAMAGE", "Damage", new float[]{60, 80, 100, 120, 150, 180, 220, 250, 280}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("BURN_DAMAGE",new AttackProperty("BURN_DAMAGE", "Burn Damage/s", new float[]{0, 0, 0, 4, 6, 8, 10, 14, 16}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("BURN_DURATION",new AttackProperty("BURN_DURATION", "Burn Duration", new float[]{0, 0, 0, 3, 3, 3, 4, 5, 6}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("FIRE_RATE",new AttackProperty("FIRE_RATE", "Fire Rate", new float[]{1f}, PropertyRepresentationType.RAW));
        newWeaponData.attackUpgradeCost = new float[]{650, 900, 1350, 1550, 1850, 2000, 2150, 2300, 2500};
        newWeaponData.range = 5;//flame thrower minimum range should be 3
        newWeaponData.dnaWorth = 300;
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/Flamethrower");
        return newWeaponData;
    }

    public static WeaponData LaserWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Laser";
        newWeaponData.type = WeaponType.LASER;
        newWeaponData.attackProperties.Add("DAMAGE",new AttackProperty("DAMAGE", "Damage", new float[]{100, 120, 140, 150, 160, 170, 180, 200, 220, 240}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("PIERCING",new AttackProperty("PIERCING", "Piercing", new float[]{1, 1, 2, 2, 3, 3, 4, 4, 5, 5}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("FIRE_RATE",new AttackProperty("FIRE_RATE", "Fire Rate", new float[]{80f}, PropertyRepresentationType.RAW));
        newWeaponData.range = 7;//flame thrower minimum range should be 3
        newWeaponData.dnaWorth = 320;
        newWeaponData.attackUpgradeCost = new float[]{100, 200};;
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/Laser");
        return newWeaponData;
    }
    public static WeaponData BossLaserWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Boss Laser";
        newWeaponData.type = WeaponType.BOSSLASER;
        newWeaponData.attackProperties.Add("DAMAGE",new AttackProperty("DAMAGE", "Damage", new float[]{6}, PropertyRepresentationType.RAW));
        newWeaponData.attackProperties.Add("FIRE_RATE",new AttackProperty("FIRE_RATE", "Fire Rate", new float[]{80f}, PropertyRepresentationType.RAW));
        newWeaponData.range = 15;
        newWeaponData.dnaWorth = 320;
        newWeaponData.attackUpgradeCost = new float[]{100, 200};;
        return newWeaponData;
    }

    public static WeaponData NewWeaponDataForType(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.STANDARD:
                return StandardWeaponData();
            
            case WeaponType.TOXIN:
                return ToxinWeaponData();

            case WeaponType.MELEE:
                return MeleeWeaponData();

            case WeaponType.MISSILE:
                return MissileWeaponData();
            
            case WeaponType.FLAMETHROWER:
                return FlameThrowerWeaponData();

            case WeaponType.LASER:
                return LaserWeaponData();
            
            case WeaponType.BOSSLASER:
                return BossLaserWeaponData();

            default:
                Debug.LogWarning($"No WeaponData with ID: {type.ToString()}");
                return NullWeaponData();
        }
    }
}