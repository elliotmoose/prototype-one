using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UpgradeDescription
{
    public WeaponData weaponData;
    public List<KeyValuePair<string, string>> properties;
    public float cost;
}
