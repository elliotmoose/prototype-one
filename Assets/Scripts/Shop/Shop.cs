using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static float SELL_WORTH_FACTOR = 0.7f;    

    public void PurchaseWeaponOfType(WeaponType type) 
    {
        //Considerations
        //1. Player has enough DNA
        //2. Player has an empty weapon slot
        //3. Player does not already have this weapon
        Player player = Player.GetInstance();

        //create new instance
        WeaponData newWeapon = WeaponData.NewWeaponDataForType(type);

        //1. 
        if(player.dnaAmount < newWeapon.GetBuyWeaponCost())
        {
            NotificationManager.GetInstance().Notify("Insufficient DNA to buy Weapon");
            Debug.LogWarning($"Insufficient DNA to purchase item: {type.ToString()}");
            return;
        }
        
        //2.
        if(player.activeWeapons[0] != null && player.activeWeapons[1] != null)
        {
            NotificationManager.GetInstance().Notify("Max of two weapons. Please sell a weapon");
            Debug.LogWarning($"Player has no weapon slot available");
            return;
        }

        //3.
        if(player.OwnsWeaponOfType(type))
        {
            Debug.LogWarning($"Player already has this weapon");
            return;
        }

        //if all pass, then we can purchase it and allocate the weapon to the slot
        if(player.activeWeapons[0] == null)
        {            
            player.activeWeapons[0] = newWeapon;
        }
        else
        {
            player.activeWeapons[1] = newWeapon;
        }

        //charge the player DNA for the purchase
        player.dnaAmount -= newWeapon.GetBuyWeaponCost();
    }

    public void SellWeaponOfType(WeaponType weaponType) 
    {
        Player player = Player.GetInstance();
        //check if is last weapon
        if(player.activeWeapons[0] == null || player.activeWeapons[1] == null)
        {
            NotificationManager.GetInstance().Notify("You cannot sell your last weapon. Please buy another");
            return;
        }
        bool sold = false;
        for(int i=0; i < player.activeWeapons.Length; i++)
        {
            if(weaponType == player.activeWeapons[i].type)
            {
                player.dnaAmount += player.activeWeapons[i].GetSellWeaponCost();                
                player.activeWeapons[i] = null; //delete
                
                //TODO: unequip

                return;
            }
        }

        if(!sold) 
        {
            Debug.Log("Tried to sell weapon player does not own");
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
            NotificationManager.GetInstance().Notify("Insufficient DNA to buy Upgrade");
            Debug.LogWarning($"BuyNextUpgradeForWeapon: Insufficient DNA to purchase item: {weaponData.type.ToString()} cost:{upgrade.cost} current:{player.dnaAmount}");
            return;
        }

        //2.
        if(!player.OwnsWeaponOfType(weaponData.type))
        {
            NotificationManager.GetInstance().Notify("You must buy this weapon to upgrade it");
            Debug.LogWarning($"BuyNextUpgradeForWeapon: Player does not own this weapon: {weaponData.type.ToString()}");
            return;
        }
        
        //3.
        if(!weaponData.CanUpgrade())
        {
            NotificationManager.GetInstance().Notify("Weapon has reached max upgrade");
            Debug.LogWarning("BuyNextUpgradeForWeapon: Weapon already max level");
            return;
        }

        player.dnaAmount -= upgrade.cost;
        weaponData.Upgrade();
    }

    //Weapons to sell
    public List<WeaponType> WeaponTypesForSale() 
    {
        List<WeaponType> weaponTypes = new List<WeaponType>();
        foreach (WeaponType weaponType in (WeaponType[]) Enum.GetValues(typeof(WeaponType)))
        {
            //not for sale:
            if(weaponType == WeaponType.NULL || weaponType == WeaponType.TOXIN || weaponType == WeaponType.MELEE || weaponType == WeaponType.BOSSLASER)
            {
                continue;
            }

            // WeaponData weaponData = WeaponData.NewWeaponDataForType(weaponType);
            weaponTypes.Add(weaponType);
        }        

        return weaponTypes;
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
