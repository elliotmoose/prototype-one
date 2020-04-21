using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public Text dnaText;
    public Text weaponNameText;
    public Text weaponUpgradeDescriptionText;
    public Text weaponDescriptionText;
    public GameObject weaponsScrollViewContentPanel;
    public GameObject weaponButtonPrefab;    

    public ShopActionButton upgradeWeaponButton;
    public ShopActionButton buySellWeaponButton;
    //dna
    Player player;

    private List<WeaponType> weaponTypesForSale = new List<WeaponType>();
    private List<GameObject> weaponButtons = new List<GameObject>();

    private Shop shop;

    private WeaponType selectedWeaponType = WeaponType.STANDARD;

    void Start()
    {
        shop = Shop.GetInstance();
        player = Player.GetInstance();

        buySellWeaponButton.GetComponent<Button>().onClick.AddListener(BuySellWeaponPressed);
        upgradeWeaponButton.GetComponent<Button>().onClick.AddListener(BuyUpgradeForSelectedEquipWeapon);

        weaponTypesForSale = shop.WeaponTypesForSale();
        UpdateShopView();
    }

    void Update()
    {        
        //float dnaNumber = player.dnaAmount;
        dnaText.text = "DNA: " + player.dnaAmount;
        
    }

    /// <summary>
    /// This returns either 1. the weapon data of the active owned weapon or 2. a new one from shop
    /// </summary>
    /// <returns></returns>
    WeaponData GetShopSelectedWeaponData() 
    {
        if(player.OwnsWeaponOfType(selectedWeaponType))
        {
            //1. get weapon index
            int index = 0;
            if(player.GetActiveWeaponAtIndex(index) == null || player.GetActiveWeaponAtIndex(index).type != selectedWeaponType) 
            {
                index = 1;
            }
            
            //2. sell weaponData player holds
            return player.GetActiveWeaponAtIndex(index);
        }
        else 
        {
            return WeaponData.NewWeaponDataForType(selectedWeaponType);
        }
    }



    /// <summary>
    /// Update shop view has to update the following:
    /// 1. Weapon scroll view
    /// 2. Selected Weapon name and description 
    /// 3. Buy/Sell/Upgrade Buttons
    /// 4. DNA 
    /// </summary>
    public void UpdateShopView()
    {   
        UpdateWeaponsScrollView();

        WeaponData selectedWeaponData = GetShopSelectedWeaponData();
        //2. 
        weaponNameText.text = selectedWeaponData.name;
        weaponDescriptionText.text = selectedWeaponData.description;

        UpgradeDescription upgradeDescription = selectedWeaponData.GetNextUpgradeDescription();
            
        weaponUpgradeDescriptionText.text = "";
        foreach(KeyValuePair<string, string> property in  upgradeDescription.properties ){
            weaponUpgradeDescriptionText.text += property.Key +": " + property.Value + "\n";
        }
              
        if(upgradeDescription.cost == -1) 
        {
            upgradeWeaponButton.SetValue("MAX");
        }
        else 
        {
            upgradeWeaponButton.SetValue(upgradeDescription.cost);
        }

        //3. 
        if (player.OwnsWeaponOfType(selectedWeaponType)) 
        {
            //sell
            buySellWeaponButton.SetTitle("SELL");
            buySellWeaponButton.SetColor(Colors.red);
            buySellWeaponButton.SetValue(selectedWeaponData.GetSellWeaponCost());            
            
            upgradeWeaponButton.SetColor(Colors.limegreen);
        }
        else 
        {
            //buy
            buySellWeaponButton.SetTitle("BUY");
            buySellWeaponButton.SetColor(Colors.limegreen);
            buySellWeaponButton.SetValue(selectedWeaponData.GetBuyWeaponCost());            
            
            upgradeWeaponButton.SetColor(Colors.gray);        
        }                

        //4. 
        dnaText.text = $"DNA: {player.dnaAmount}";
    }

    public void BuyUpgradeForSelectedEquipWeapon()
    {                        
        if (player.OwnsWeaponOfType(selectedWeaponType))
        {
            shop.BuyNextUpgradeForWeapon(GetShopSelectedWeaponData());
            UpdateShopView();
        }
        else 
        {
            Debug.LogWarning("BuyUpgradeForSelectedEquipWeapon: No selected equip weapon");
        }
    }


    public void UpdateWeaponsScrollView()
    {
        foreach (Transform child in weaponsScrollViewContentPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }        

        weaponButtons.Clear();
        foreach (WeaponType weaponType in weaponTypesForSale)
        {
            GameObject weaponButton = GameObject.Instantiate(weaponButtonPrefab, Vector3.zero, Quaternion.identity, weaponsScrollViewContentPanel.transform);
            bool isOwned = player.OwnsWeaponOfType(weaponType);
            bool selected = selectedWeaponType == weaponType;
            WeaponData weaponData = WeaponData.NewWeaponDataForType(weaponType); //just for weapon title 
            weaponButton.GetComponent<WeaponButton>().SetData(weaponData, isOwned, selected);
            weaponButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectWeaponOfType(weaponType);
            });

            weaponButtons.Add(weaponButton);
        }
    }

    public void SelectWeaponOfType(WeaponType weaponType)
    {
        selectedWeaponType = weaponType;
        UpdateShopView();
    }

    public void BuySellWeaponPressed()
    {                
        if(player.OwnsWeaponOfType(selectedWeaponType)) 
        {
            //sell
            shop.SellWeaponOfType(selectedWeaponType);  
        }
        else 
        {
            //buy
            shop.PurchaseWeaponOfType(selectedWeaponType);            
        }

        UpdateShopView();
    }


}