using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

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
    }

    private void UpdateShopOpen() 
    {
        Player player = Player.GetInstance();
        isOpen = Vector3.Distance(player.transform.position, this.transform.position) <= shopOpenRange;
        GetComponent<Renderer>().material.color = isOpen ? new Color32(0,245,132,255) : new Color32(245,111,0,255);
    }

    public void PurchaseItem(ShopItem shopItem) 
    {
        Player player = Player.GetInstance();
        if(player.dnaAmount < shopItem.price)
        {
            Debug.LogWarning($"Insufficient DNA to purchase item: {shopItem.id}");
            return;
        }

        player.OnPurchasedShopItem(shopItem);
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
