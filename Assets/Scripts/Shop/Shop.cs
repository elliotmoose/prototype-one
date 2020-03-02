using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    const float SELL_WORTH_FACTOR = 0.7f;
    private bool isOpen = false;
    private float shopOpenRange = 3f;

    public List<ShopItem> shopItems = new List<ShopItem>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShopOpen();

        //TEST
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PurchaseWeapon(WeaponData.NewWeaponDataForType(WeaponType.TOXIN));
        }
    }

    private void UpdateShopOpen() 
    {
        Player player = Player.GetInstance();
        isOpen = Vector3.Distance(player.transform.position, this.transform.position) <= shopOpenRange;
        GetComponent<Renderer>().material.color = isOpen ? new Color32(0,245,132,255) : new Color32(245,111,0,255);
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
        if((player.activeWeapons[0] != null && player.activeWeapons[0].type == weaponData.type) || (player.activeWeapons[1] != null && player.activeWeapons[1].type == weaponData.type))
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
                player.dnaAmount += weapon.dnaWorth * SELL_WORTH_FACTOR;
                break;
            }
        }
    }


    public static Shop GetInstance()
    {
        GameObject shopObject = GameObject.Find("Shop");
        if(shopObject == null) 
        {
            return null;
        }

        return shopObject.GetComponent<Shop>();
    }
}
