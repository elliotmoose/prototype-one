using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public WeaponType type = WeaponType.NULL; //helps to identify the prefab to spawn when equipped
	public int damageType = 0;
	public float damage = 0;
	public float fireRate = 0;
	public float range = 0;
	public float dnaWorth = 0;
    public int weaponLevel = 1;
    public float[] weaponUpgradeCost = {};

    public bool IsMaxUpgrade() 
    {
        return weaponLevel >= weaponUpgradeCost.Length-1;
    }

    public float GetNextUpgradeCost() 
    {
        if(weaponLevel > weaponUpgradeCost.Length-1)
        {
            return -1;
        }

        return weaponUpgradeCost[weaponLevel];
    }

    public void Upgrade() 
    {
        weaponLevel += 1;
    }

    public static WeaponData NullWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.NULL;
        newWeaponData.damage = 0;
        newWeaponData.fireRate = 1;
        newWeaponData.range = 0;
        newWeaponData.dnaWorth = 0;
        newWeaponData.weaponUpgradeCost = new float[]{100, 200};
        return newWeaponData;
    }

    public static WeaponData ToxinWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.TOXIN;
        newWeaponData.damage = 2;
        newWeaponData.fireRate = 1;
        newWeaponData.range = 6.5f;
        newWeaponData.dnaWorth = 0;
        newWeaponData.weaponUpgradeCost = new float[]{100, 200};;
        return newWeaponData;
    }

    public static WeaponData StandardWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.STANDARD;
        newWeaponData.damage = 70;
        newWeaponData.fireRate = 1.8f;
        newWeaponData.range = 6;
        newWeaponData.dnaWorth = 100;
        newWeaponData.weaponUpgradeCost = new float[]{100, 200};;
        return newWeaponData;
    }

    public static WeaponData RapidWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.RAPID;
        newWeaponData.damage = 25;
        newWeaponData.fireRate = 10;
        newWeaponData.range = 8;
        newWeaponData.dnaWorth = 400;
        newWeaponData.weaponUpgradeCost = new float[]{100, 200};;
        return newWeaponData;
    }

    public static WeaponData MeleeWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.MELEE;
        newWeaponData.damage = 2;
        newWeaponData.fireRate = 1f;
        newWeaponData.range = 2;
        newWeaponData.dnaWorth = 0;
        newWeaponData.weaponUpgradeCost = new float[]{100, 200};;
        return newWeaponData;
    }

    public static WeaponData BombWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.BOMB;
        newWeaponData.damage = 100;
        newWeaponData.fireRate = 0.7f;
        newWeaponData.range = 7;
        newWeaponData.dnaWorth = 300;
        newWeaponData.weaponUpgradeCost = new float[]{100, 200};;
        return newWeaponData;
    }
    
    public static WeaponData FlameThrowerWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.FLAMETHROWER;
        newWeaponData.damage = 200; //20 damage per second
        // newWeaponData.fireRate = 20f;
        newWeaponData.range = 5;//flame thrower minimum range should be 3
        newWeaponData.dnaWorth = 300;
        newWeaponData.weaponUpgradeCost = new float[]{100, 200};;
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
