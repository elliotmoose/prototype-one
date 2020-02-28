using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    public ShopItemType type = ShopItemType.WEAPON;
    public string id = "NULL";
    public float price = 0;
}

public enum ShopItemType 
{
    WEAPON,
    UPGRADE
}
