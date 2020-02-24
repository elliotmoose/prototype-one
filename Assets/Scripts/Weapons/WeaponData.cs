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

    public static WeaponData StandardWeapon() 
    {
        WeaponData newWeaponData = new WeaponData();
        newWeaponData.weaponId = "STANDARD";
        newWeaponData.damage = 50;
        newWeaponData.cooldown = 0.2f;
        newWeaponData.range = 3;
        return newWeaponData;
    }
}
