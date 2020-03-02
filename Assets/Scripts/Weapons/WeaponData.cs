using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public WeaponType type = WeaponType.NULL; //helps to identify the prefab to spawn when equipped
	public int damageType = 0;
	public float damage = 0;
	public float cooldown = 0;
	public float range = 0;
	public float dnaWorth = 0;

    public static WeaponData NullWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.NULL;
        newWeaponData.damage = 0;
        newWeaponData.cooldown = 1;
        newWeaponData.range = 0;
        newWeaponData.dnaWorth = 0;
        return newWeaponData;
    }

    public static WeaponData ToxinWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.TOXIN;
        newWeaponData.damage = 2;
        newWeaponData.cooldown = 0.8f;
        newWeaponData.range = 6.5f;
        newWeaponData.dnaWorth = 0;
        return newWeaponData;
    }

    public static WeaponData StandardWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.STANDARD;
        newWeaponData.damage = 70;
        newWeaponData.cooldown = 0.5f;
        newWeaponData.range = 6;
        newWeaponData.dnaWorth = 100;
        return newWeaponData;
    }

    public static WeaponData RapidWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.RAPID;
        newWeaponData.damage = 25;
        newWeaponData.cooldown = 0.1f;
        newWeaponData.range = 8;
        newWeaponData.dnaWorth = 400;
        return newWeaponData;
    }

    public static WeaponData MeleeWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.MELEE;
        newWeaponData.damage = 30;
        newWeaponData.cooldown = 1f;
        newWeaponData.range = 1;
        newWeaponData.dnaWorth = 0;
        return newWeaponData;
    }

    public static WeaponData BombWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.type = WeaponType.BOMB;
        newWeaponData.damage = 100;
        newWeaponData.cooldown = 0.1f;
        newWeaponData.range = 7;
        newWeaponData.dnaWorth = 300;
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

            default:
                Debug.LogWarning($"No WeaponData with ID: {type.ToString()}");
                return NullWeaponData();
        }
    }
}
