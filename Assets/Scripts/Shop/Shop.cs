using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static float SELL_WORTH_FACTOR = 0.7f;

    public List<ShopItem> shopItems = new List<ShopItem>();
    public GameObject shopButton;
    public GameObject shopMenu;
    private bool _shopDisplayed = false;

    //Test
    public void TestPurchaseWeapon()
    {
        // WeaponsForSale().ForEach((WeaponData data)=> {
        //     Debug.Log(data.name);
        // });
        WeaponData weaponData = WeaponData.NewWeaponDataForType(WeaponType.FLAMETHROWER);
        PurchaseWeapon(weaponData);
    }


    public void PurchaseWeapon(WeaponData weaponData) 
    {
        //Considerations
        //1. Player has enough DNA
        //2. Player has an empty weapon slot
        //3. Player does not already have this weapon
        Player player = Player.GetInstance();
        //1. 
        if(player.dnaAmount < weaponData.dnaWorth)
        {
            Debug.LogWarning($"Insufficient DNA to purchase item: {weaponData.type.ToString()}");
            return;
        }
        
        //2.
        if(player.activeWeapons[0] != null && player.activeWeapons[1] != null)
        {
            Debug.LogWarning($"Player has no weapon slot available");
            return;
        }

        //3.
        if(player.OwnsWeapon(weaponData))
        {
            Debug.LogWarning($"Player already has this weapon");
            return;
        }

        //if all pass, then we can purchase it and allocate the weapon to the slot
        if(player.activeWeapons[0] == null)
        {
            player.activeWeapons[0] = weaponData;
        }
        else
        {
            player.activeWeapons[1] = weaponData;
        }

        //charge the player DNA for the purchase
        player.dnaAmount -= weaponData.dnaWorth;
    }

    public void SellWeapon(WeaponData weaponData) 
    {
        Player player = Player.GetInstance();
        for(int i=0; i < player.activeWeapons.Length; i++)
        {
            WeaponData weapon = player.activeWeapons[i];
            if(weaponData == weapon)
            {
                player.activeWeapons[i] = null; //delete
                player.dnaAmount += weapon.GetSellWeaponCost();                

                //TODO: unequip
                return;
            }
        }
    }

    public void BuyNextUpgradeForWeapon(WeaponData weaponData)
    {
        //Considerations
        //1. Player has enough DNA
        //2. Player does not own weapon
        //3. Weapon already max upgrade

        Player player = Player.GetInstance();
        UpgradeDescription upgrade = weaponData.GetNextUpgradeDescription();
        //1. 
        if(player.dnaAmount < upgrade.cost)
        {
            Debug.LogWarning($"BuyNextUpgradeForWeapon: Insufficient DNA to purchase item: {weaponData.type.ToString()} cost:{upgrade.cost} current:{player.dnaAmount}");
            return;
        }

        //2.
        if(!player.OwnsWeapon(weaponData))
        {
            Debug.LogWarning($"BuyNextUpgradeForWeapon: Player does not own this weapon: {weaponData.type.ToString()}");
            return;
        }
        
        //3.
        if(!weaponData.CanUpgrade())
        {
            Debug.LogWarning("BuyNextUpgradeForWeapon: Weapon already max level");
            return;
        }

        player.dnaAmount -= upgrade.cost;
        weaponData.Upgrade();
    }

    //Weapons to sell
    public List<WeaponData> WeaponsForSale() 
    {
        List<WeaponData> weapons = new List<WeaponData>();
        foreach (WeaponType weaponType in (WeaponType[]) Enum.GetValues(typeof(WeaponType)))
        {
            //not for sale:
            if(weaponType == WeaponType.NULL || weaponType == WeaponType.TOXIN || weaponType == WeaponType.MELEE || weaponType == WeaponType.BOSSLASER)
            {
                continue;
            }

            WeaponData weaponData = WeaponData.NewWeaponDataForType(weaponType);
            weapons.Add(weaponData);
        }        

        return weapons;
    }

    public static Shop GetInstance()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        
        if(gameManagerObject == null) 
        {            
            return null;
        }

        return gameManagerObject.GetComponent<Shop>();
    }
}
