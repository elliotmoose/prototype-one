using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{

    // private WeaponData selectedEquipWeapon;
    private int selectedEquipWeaponIndex = 0;

    public Text dnaText;
    public Text weaponNameText;
    public Text weaponUpgradeDescriptionText;
    public GameObject weaponsScrollViewContentPanel;
    public GameObject weaponButtonPrefab;    

    public ShopActionButton upgradeWeaponButton;
    public ShopActionButton buySellWeaponButton;
    //dna
    Player player;

    private List<WeaponData> weapons = new List<WeaponData>();
    private List<GameObject> weaponButtons = new List<GameObject>();

    private Shop shop;

    private WeaponData selectedWeaponData;

    void Start()
    {
        shop = Shop.GetInstance();
        player = Player.GetInstance();

        buySellWeaponButton.GetComponent<Button>().onClick.AddListener(BuySellWeaponPressed);
        upgradeWeaponButton.GetComponent<Button>().onClick.AddListener(BuyUpgradeForSelectedEquipWeapon);

        weapons = shop.WeaponsForSale();
        UpdateShopView();
    }

    void Update()
    {        
        //float dnaNumber = player.dnaAmount;
        dnaText.text = "DNA: " + player.dnaAmount;
        
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
        //1. 
        UpdateWeaponsScrollView();

        if(selectedWeaponData == null) 
        {
            SelectWeaponData(weapons[0]);   
        }

        //2. 
        weaponNameText.text = selectedWeaponData.name;
        UpgradeDescription upgradeDescription = selectedWeaponData.GetNextUpgradeDescription();
            
        weaponUpgradeDescriptionText.text = "Upgrade Description:\n";
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
        bool isOwned = player.OwnsWeapon(selectedWeaponData);

        if (isOwned) 
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
        WeaponData selectedEquipWeapon = player.GetActiveWeaponAtIndex(selectedEquipWeaponIndex);
        UpgradeDescription upgradeDescription = selectedEquipWeapon.GetNextUpgradeDescription();
        if (selectedEquipWeapon != null)
        {
            shop.BuyNextUpgradeForWeapon(selectedEquipWeapon);
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
        foreach (WeaponData weapon in weapons)
        {
            GameObject weaponButton = GameObject.Instantiate(weaponButtonPrefab, Vector3.zero, Quaternion.identity, weaponsScrollViewContentPanel.transform);
            bool isOwned = player.OwnsWeapon(weapon);
            bool selected = selectedWeaponData == weapon;
            weaponButton.GetComponent<WeaponButton>().SetData(weapon, isOwned, selected);
            weaponButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectWeaponData(weapon);
            });

            weaponButtons.Add(weaponButton);
        }
    }

    public void SelectWeaponData(WeaponData weapon)
    {
        selectedWeaponData = weapon;
        UpdateShopView();
    }

    public void BuySellWeaponPressed()
    {
        if (selectedWeaponData == null)
        {
            Debug.LogWarning("BuySellWeapon: NO WEAPON SELECTED");
            return;
        }

        bool isOwned = player.OwnsWeapon(selectedWeaponData);
        
        if(isOwned) 
        {
            //sell
            //1. get weapon index
            int index = 0;
            if(player.GetActiveWeaponAtIndex(index).type != selectedWeaponData.type) 
            {
                index = 1;
            }
            
            //2. sell weaponData player holds
            WeaponData selectedEquipWeapon = player.GetActiveWeaponAtIndex(selectedEquipWeaponIndex);
            // UpgradeDescription upgradeDescription = selectedEquipWeapon.GetNextUpgradeDescription();
            if (selectedEquipWeapon != null)
            {
                shop.SellWeapon(selectedEquipWeapon);
            }
            else 
            {
                Debug.LogError("BuySellWeapon: Could not find weapon in player to sell");
            }
        }
        else 
        {
            //buy
            shop.PurchaseWeapon(selectedWeaponData);
        }

        UpdateShopView();
    }

    // private void UpdateBuyView() 
    // {

    //     Player player = Player.GetInstance();        
    //     bool isOwned = player.OwnsWeapon(selectedWeaponToBuy);
    //     buyWeaponNameText.text = selectedWeaponToBuy.name;
    //     buyWeaponNameText.color = isOwned ? red : green;
    //     buyWeaponPriceText.color = isOwned ? red : green;
    //     buyWeaponPriceText.text = isOwned ? "OWN" : ((int)selectedWeaponToBuy.dnaWorth).ToString();

    //     //ui
    //     for (int i = 0; i < weaponButtons.Count; i++)
    //     {
    //         WeaponData thisWeapon = weapons[i];
    //         Image weaponButtonImage = weaponButtons[i].GetComponent<Image>();
    //         Text weaponButtonText = weaponButtons[i].GetComponentInChildren<Text>();

    //         bool isSelected = thisWeapon == selectedWeaponToBuy;
    //         weaponButtonImage.color = isSelected ? (isOwned ? red : green) : white;
    //         weaponButtonText.color = isSelected ? white : black;
    //     }
    // }
}