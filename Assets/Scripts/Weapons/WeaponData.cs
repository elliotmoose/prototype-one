using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public WeaponType type = WeaponType.NULL; //helps to identify the prefab to spawn when equipped
    public string name = ""; //display name
    public string description = ""; //summary description
	public int damageType = 0;	
    public int weaponLevel = 0;
    private Dictionary<string, WeaponProperty> weaponProperties = new Dictionary<string, WeaponProperty>();
    public Sprite weaponSprite = null;
    // public float[] attackUpgradeCost = {};

    public bool CanUpgrade() 
    {
        //check if max already
        return weaponLevel < weaponProperties["UPGRADE_COST"].GetValues().Count;
    }

    private float GetNextUpgradeCost() 
    {   
        if(weaponLevel > weaponProperties["UPGRADE_COST"].GetValues().Count-1) 
        {
            return -1;
        }
        return GetWeaponPropertyValue("UPGRADE_COST");
    }

    public UpgradeDescription GetNextUpgradeDescription()
    {
        UpgradeDescription upgradeDescription = new UpgradeDescription();
        upgradeDescription.weaponData = this;
        upgradeDescription.cost = GetNextUpgradeCost();
        upgradeDescription.properties = new List<KeyValuePair<string, string>>();
        foreach(WeaponProperty attackProperty in weaponProperties.Values)
        {
            //every property has a name and a value description
            //there is also a cost attached to this set of properties (upgrade)
            if (attackProperty.GetValueForWeaponLevel(weaponLevel) == attackProperty.GetValueForWeaponLevel(weaponLevel+1) || attackProperty.id == "UPGRADE_COST") 
            {
                //don't put it in if its the same
                continue;
            }

            KeyValuePair<string, string> propertyDescription = new KeyValuePair<string, string>(attackProperty.name, $"{attackProperty.GetValueForWeaponLevel(weaponLevel)} -> {attackProperty.GetValueForWeaponLevel(weaponLevel+1)}");
            upgradeDescription.properties.Add(propertyDescription);
        }

        return upgradeDescription;
    }

    public float GetBuyWeaponCost()
    {
        return this.GetWeaponPropertyValue("DNA_WORTH");
    }

    public float GetSellWeaponCost()
    {
        if (weaponLevel == 0)
        {
            return this.GetWeaponPropertyValue("DNA_WORTH");
        }

        float sumUpgradeDnaWorth = 0;
        for (int i = 0; i < weaponLevel; i++)
        {
            sumUpgradeDnaWorth += weaponProperties["UPGRADE_COST"].GetValues()[weaponLevel-1];
        }

        return (sumUpgradeDnaWorth + this.GetWeaponPropertyValue("DNA_WORTH"))*Shop.SELL_WORTH_FACTOR;
    }

    public float GetWeaponPropertyValue(string key) {
        WeaponProperty attackProperty;
        weaponProperties.TryGetValue(key, out attackProperty);
        if(attackProperty != null) {
            return attackProperty.GetValueForWeaponLevel(weaponLevel);
        }
        else {
            Debug.LogError("WeaponData is missing key: " + key);
            return 0;
        }
    }

    public void AddWeaponPropertyValue(string key, string name, float[] values, PropertyRepresentationType representationType) {
        if(this.weaponProperties.ContainsKey(key))
        {
            Debug.LogError($"WeaponData already has weaponProperty ${key}");
        }
        else 
        {
            this.weaponProperties.Add(key,new WeaponProperty(key, name, values, representationType));
        }
    }
    public void SetCurrentWeaponPropertyValue(string key, float value) {
        WeaponProperty attackProperty;
        weaponProperties.TryGetValue(key, out attackProperty);
        if(attackProperty != null) {
            attackProperty.SetValueForWeaponLevel(weaponLevel, value);
        }
        else {
            Debug.LogError("WeaponData is missing key: " + key);
        }
    }

    public float GetDamage() 
    {
        return GetWeaponPropertyValue("DAMAGE");
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
        newWeaponData.AddWeaponPropertyValue("UPGRADE_COST", "Upgrade Cost", new float[]{0}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("DAMAGE", "Damage", new float[]{0}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("FIRE_RATE","Fire Rate", new float[]{1}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("RANGE", "Range", new float[]{0}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("DNA_WORTH", "Base Cost", new float[]{0}, PropertyRepresentationType.RAW);
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/null");
        return newWeaponData;
    }

    public static WeaponData MeleeWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Fist";
        newWeaponData.type = WeaponType.MELEE;
        newWeaponData.AddWeaponPropertyValue("UPGRADE_COST", "Upgrade Cost", new float[]{0}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("DAMAGE","Damage", new float[]{5}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("FIRE_RATE", "Fire Rate", new float[]{1}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("RANGE", "Range", new float[]{2f}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("DNA_WORTH", "Base Cost", new float[]{0}, PropertyRepresentationType.RAW);
        
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/null");
        return newWeaponData;
    }

    public static WeaponData ToxinWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Toxin";
        newWeaponData.type = WeaponType.TOXIN;
        newWeaponData.AddWeaponPropertyValue("UPGRADE_COST", "Upgrade Cost", new float[]{0}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("DAMAGE","Damage", new float[]{4}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("FIRE_RATE", "Fire Rate", new float[]{1}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("BULLET_SPLIT", "Bullet split", new float[]{1}, PropertyRepresentationType.SPLIT);
        newWeaponData.AddWeaponPropertyValue("RANGE", "Range", new float[]{11f}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("DNA_WORTH", "Base Cost", new float[]{0}, PropertyRepresentationType.RAW);
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/null");
        return newWeaponData;
    }
    

    public static WeaponData StandardWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "First Aid \nShooter";
        newWeaponData.description = "Pew pew pew!";
        newWeaponData.type = WeaponType.STANDARD;
        newWeaponData.weaponProperties =  WeaponDataTableLoader.templateWeaponProperties["STANDARD"];
        // newWeaponData.range = 6;
        // newWeaponData.dnaWorth = 100;
        // newWeaponData.AddWeaponPropertyValue("DAMAGE", "Damage", new float[]{35f, 38f, 40f, 42f, 45f, 50f, 60f, 65f, 70f, 75f}, PropertyRepresentationType.RAW));
        // newWeaponData.AddWeaponPropertyValue("FIRE_RATE","Fire Rate", new float[]{4f, 4.5f, 4.9f, 5.5f, 6.3f,7.0f, 7.5f, 7.8f, 8.1f, 8.5f}, PropertyRepresentationType.RAW));
        // newWeaponData.AddWeaponPropertyValue("BULLET_SPLIT","Bullet split", new float[]{1f, 1f, 2f, 2f, 3f, 3f, 4f, 4f, 4f, 4f}, PropertyRepresentationType.SPLIT));
        // newWeaponData.attackUpgradeCost = new float[]{400, 500, 600, 700, 800, 1200, 1500, 2900, 3400, 4000};
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/Normie");
        return newWeaponData;
    }

    
    public static WeaponData MissileWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Antibiotics \nLauncher";
        newWeaponData.description = "Explosive goodness. \nVery effective against Badteria";
        newWeaponData.type = WeaponType.MISSILE;
        newWeaponData.weaponProperties =  WeaponDataTableLoader.templateWeaponProperties["MISSILE"];
        // newWeaponData.AddWeaponPropertyValue("DAMAGE","Damage", new float[]{65, 80, 95, 110, 125, 140, 155, 170, 185, 200}, PropertyRepresentationType.RAW);
        // newWeaponData.AddWeaponPropertyValue("FIRE_RATE","Fire Rate", new float[]{1.7f}, PropertyRepresentationType.RAW);
        // newWeaponData.range = 7;
        // newWeaponData.dnaWorth = 450;        
        // newWeaponData.AddWeaponPropertyValue("EXPLOSION_DAMAGE", "Explosion Damage", new float[]{0f, 0.5f, 0.7f, 0.8f, 0.8f, 0.8f, 0.9f, 0.9f, 0.9f, 1f}, PropertyRepresentationType.PERCENTAGE);
        // newWeaponData.AddWeaponPropertyValue("EXPLOSION_RADIUS", "Explosion Radius", new float[]{0f, 1f, 1.5f, 2f, 2.2f, 2.4f, 2.4f, 2.6f, 3f, 3.3f}, PropertyRepresentationType.RAW);
        // newWeaponData.attackUpgradeCost = new float[]{650, 800, 1000, 1200, 1400, 1600, 1900, 2300, 2700, 3100, 3500};
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/Missle");
        return newWeaponData;
    }
    
    public static WeaponData FlameThrowerWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Antiviral \nFlamethrower";
        newWeaponData.description = "Some weapons just want to watch the world burn! \nVery effective against Vivi-rus.";
        newWeaponData.type = WeaponType.FLAMETHROWER;
        newWeaponData.weaponProperties =  WeaponDataTableLoader.templateWeaponProperties["FLAMETHROWER"];

        // newWeaponData.AddWeaponPropertyValue("DAMAGE","Damage", new float[]{60, 80, 100, 120, 150, 180, 220, 250, 280}, PropertyRepresentationType.RAW);
        // newWeaponData.AddWeaponPropertyValue("BURN_DAMAGE","Burn Damage/s", new float[]{0, 0, 0, 4, 6, 8, 10, 14, 16}, PropertyRepresentationType.RAW);
        // newWeaponData.AddWeaponPropertyValue("BURN_DURATION","Burn Duration", new float[]{0, 0, 0, 3, 3, 3, 4, 5, 6}, PropertyRepresentationType.RAW);
        // newWeaponData.AddWeaponPropertyValue("FIRE_RATE", "Fire Rate", new float[]{1f}, PropertyRepresentationType.RAW));
        // newWeaponData.attackUpgradeCost = new float[]{650, 900, 1000, 1350, 1650, 2000, 2150, 2300, 2500};
        // newWeaponData.range = 7;//flame thrower minimum range should be 3
        // newWeaponData.dnaWorth = 600;
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/Flamethrower");
        return newWeaponData;
    }

    public static WeaponData LaserWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Natural Killer \nLaser";
        newWeaponData.description = "Zap! Naturally kills more when focusing the same target.";

        newWeaponData.type = WeaponType.LASER;
        newWeaponData.weaponProperties = WeaponDataTableLoader.templateWeaponProperties["LASER"];

        // newWeaponData.AddWeaponPropertyValue("DAMAGE",new WeaponProperty("DAMAGE", "Damage", new float[]{100, 120, 140, 150, 175, 200, 210, 225, 250, 260}, PropertyRepresentationType.RAW));
        // newWeaponData.AddWeaponPropertyValue("PIERCING",new WeaponProperty("PIERCING", "Piercing", new float[]{1, 1, 2, 3, 4, 5, 5, 6, 8, 10}, PropertyRepresentationType.RAW));
        // newWeaponData.AddWeaponPropertyValue("FIRE_RATE",new WeaponProperty("FIRE_RATE", "Fire Rate", new float[]{80f}, PropertyRepresentationType.RAW));
        // newWeaponData.range = 12f;//flame thrower minimum range should be 3
        // newWeaponData.dnaWorth = 550;
        // newWeaponData.attackUpgradeCost = new float[]{250, 300, 500, 700, 1200, 1500, 1750, 2250, 2500};;
        newWeaponData.weaponSprite = Resources.Load<Sprite>("Sprites/WeaponsSprite/Laser");
        return newWeaponData;
    }
    public static WeaponData BossLaserWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Boss Laser";
        newWeaponData.type = WeaponType.BOSSLASER;
        newWeaponData.AddWeaponPropertyValue("DAMAGE", "Damage", new float[]{750}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("FIRE_RATE", "Fire Rate", new float[]{80f}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("RANGE", "Range", new float[]{13f}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("DNA_WORTH", "Base Cost", new float[]{0}, PropertyRepresentationType.RAW);
        newWeaponData.AddWeaponPropertyValue("UPGRADE_COST", "Upgrade Cost", new float[]{0}, PropertyRepresentationType.RAW);
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