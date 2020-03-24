using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public WeaponType type = WeaponType.NULL; //helps to identify the prefab to spawn when equipped
    public string name = ""; //display name
	public int damageType = 0;
	public float damage = 0;
	public float fireRate = 0;
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

    public float GetAttackPropertyValue(string key) {
        AttackProperty attackProperty;
        attackProperties.TryGetValue(key, out attackProperty);
        if(attackProperty != null) {
            return attackProperty.GetValueForWeaponLevel(weaponLevel);
        }
        else {
            return 0;
        }
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
        newWeaponData.damage = 0;
        newWeaponData.fireRate = 1;
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
        newWeaponData.damage = 2;
        newWeaponData.fireRate = 1;
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
        newWeaponData.damage = 100;
        newWeaponData.fireRate = 4f;
        newWeaponData.range = 6;
        newWeaponData.dnaWorth = 100;
        newWeaponData.attackUpgradeCost = new float[]{100, 200};
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/Normie");
        return newWeaponData;
    }

    public static WeaponData RapidWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Rapid Gun";
        newWeaponData.type = WeaponType.RAPID;
        newWeaponData.damage = 25;
        newWeaponData.fireRate = 10;
        newWeaponData.range = 8;
        newWeaponData.dnaWorth = 400;
        newWeaponData.attackUpgradeCost = new float[]{100, 200};
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/null");
        return newWeaponData;
    }

    public static WeaponData MeleeWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Fist";
        newWeaponData.type = WeaponType.MELEE;
        newWeaponData.damage = 2;
        newWeaponData.fireRate = 1f;
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
        newWeaponData.damage = 50;
        newWeaponData.fireRate = 1.1f;
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
        newWeaponData.damage = 200; //20 damage per second
        // newWeaponData.fireRate = 20f;
        newWeaponData.range = 5;//flame thrower minimum range should be 3
        newWeaponData.dnaWorth = 300;
        newWeaponData.attackUpgradeCost = new float[]{100, 200};;
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/Flamethrower");
        return newWeaponData;
    }

    public static WeaponData LaserWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Laser";
        newWeaponData.type = WeaponType.LASER;
        newWeaponData.damage = 3; 
        newWeaponData.fireRate = 80;
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
        newWeaponData.damage = 4; 
        newWeaponData.fireRate = 80;
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
            
            case WeaponType.RAPID:
                return RapidWeaponData();
            
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