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
    public int weaponLevel = 1;
    public List<AttackProperty> attackProperties = new List<AttackProperty>();
    public float[] attackUpgradeCost = {};

    public bool CanUpgrade() 
    {
        //check if max already
        return weaponLevel >= attackUpgradeCost.Length-1;
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
        foreach(AttackProperty attackProperty in attackProperties)
        {
            //every property has a name and a value description
            //there is also a cost attached to this set of properties (upgrade)
            KeyValuePair<string, string> propertyDescription = new KeyValuePair<string, string>(attackProperty.name, $"{attackProperty.ValueForWeaponLevel(weaponLevel)} -> {attackProperty.ValueForWeaponLevel(weaponLevel+1)}");
            upgradeDescription.properties.Add(propertyDescription);
        }

        return upgradeDescription;
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
        return newWeaponData;
    }

    public static WeaponData StandardWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Normie Gun";
        newWeaponData.type = WeaponType.STANDARD;
        newWeaponData.damage = 50;
        newWeaponData.fireRate = 4f;
        newWeaponData.range = 6;
        newWeaponData.dnaWorth = 100;



        newWeaponData.attackUpgradeCost = new float[]{100, 200};
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
        newWeaponData.attackUpgradeCost = new float[]{100, 200};;
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
        newWeaponData.attackUpgradeCost = new float[]{100, 200};;
        return newWeaponData;
    }

    public static WeaponData BombWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.name = "Rocket Launcher";
        newWeaponData.type = WeaponType.BOMB;
        newWeaponData.damage = 100;
        newWeaponData.fireRate = 0.7f;
        newWeaponData.range = 7;
        newWeaponData.dnaWorth = 300;        
        newWeaponData.attackProperties.Add(new AttackProperty("EXPLOSION_DAMAGE", "Explosion Damage", new float[]{0.3f, 0.5f, 0.7f}, PropertyRepresentationType.PERCENTAGE));
        newWeaponData.attackProperties.Add(new AttackProperty("EXPLOSION_RADIUS", "Explosion Radius", new float[]{0.3f, 0.5f, 0.9f}, PropertyRepresentationType.RAW));
        newWeaponData.attackUpgradeCost = new float[]{600, 850, 1200};
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

            case WeaponType.BOMB:
                return BombWeaponData();
            
            case WeaponType.FLAMETHROWER:
                return FlameThrowerWeaponData();

            default:
                Debug.LogWarning($"No WeaponData with ID: {type.ToString()}");
                return NullWeaponData();
        }
    }
}