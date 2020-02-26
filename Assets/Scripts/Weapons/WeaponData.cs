using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public string weaponId = "NULL"; //helps to identify the prefab to spawn when equipped
	public int damageType = 0;
	public float damage = 0;
	public float cooldown = 0;
	public float range = 0;

    public static WeaponData NullWeaponData()
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.weaponId = "NULL";
        newWeaponData.damage = 0;
        newWeaponData.cooldown = 1;
        newWeaponData.range = 0;
        return newWeaponData;
    }
    public static WeaponData StandardWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.weaponId = "STANDARD";
        newWeaponData.damage = 100;
        newWeaponData.cooldown = 0.6f;
        newWeaponData.range = 6;
        return newWeaponData;
    }

    public static WeaponData RapidWeaponData() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.weaponId = "RAPID";
        newWeaponData.damage = 25;
        newWeaponData.cooldown = 0.1f;
        newWeaponData.range = 4;
        return newWeaponData;
    }
}
