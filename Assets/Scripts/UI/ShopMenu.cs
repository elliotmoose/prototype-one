using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    //tabs
    public GameObject equippedTab;
    public GameObject buyWeaponTab;
    public GameObject upgradePlayerTab;
    public Text equippedTabButtonText;
    public Text buyWeaponTabButtonText;
    public Text upgradePlayerTabButtonText;

    //equip weapon
    public Text selectEquipWeapon1ButtonText;
    public Text selectEquipWeapon2ButtonText;
    public Text selectedEquipWeaponText;
    public Text weaponUpgradeDescriptionText;
    public Text upgradeEquipWeaponPriceText;
    

    // private WeaponData selectedEquipWeapon;
    private int selectedEquipWeaponIndex = 0;

    //sell weapon
    public GameObject sellWeaponButton;
    public Text sellWeaponCostText;

    //buy weapon
    public GameObject weaponsScrollViewContentPanel;
    public GameObject weaponButtonPrefab;
    public Button buyWeaponButton;
    public Text buyWeaponNameText;
    public Text buyWeaponDescriptionText;
    public Text buyWeaponPriceText;

    //dna
    Player player;
    public GameObject dnaTextObject;
    Text dnaText;

    private List<WeaponData> weapons = new List<WeaponData>();
    private List<GameObject> weaponButtons = new List<GameObject>();

    private Shop shop;

    private WeaponData selectedWeaponToBuy;

    #region Colors 
    private Color green;
    private Color gray;
    private Color red;
    private Color blue;
    private Color yellow;
    private Color white;
    private Color black;
    #endregion

    void Start()
    {
        InitializeColors();
        shop = Shop.GetInstance();
        buyWeaponButton.onClick.AddListener(BuyWeapon);
        UpdateWeaponsScrollView();
        SelectTab(0);
        SelectEquippedWeapon(0);
        player = Player.GetInstance();
        dnaText = dnaTextObject.GetComponent<Text>();
    }

    void Update()
    {
        
        //float dnaNumber = player.dnaAmount;
        dnaText.text = "DNA: " + player.dnaAmount;
    }

    #region Tab Selector 
    public void SelectTab(int index)
    {
        switch (index)
        {
            //equipped
            case 0:
                equippedTab.SetActive(true);
                buyWeaponTab.SetActive(false);
                upgradePlayerTab.SetActive(false);

                equippedTabButtonText.color = red;
                buyWeaponTabButtonText.color = gray;
                upgradePlayerTabButtonText.color = gray;
                break;

            //buy
            case 1:
                equippedTab.SetActive(false);
                buyWeaponTab.SetActive(true);
                upgradePlayerTab.SetActive(false);

                equippedTabButtonText.color = gray;
                buyWeaponTabButtonText.color = red;
                upgradePlayerTabButtonText.color = gray;
                break;
            case 2:
                equippedTab.SetActive(false);
                buyWeaponTab.SetActive(false);
                upgradePlayerTab.SetActive(true);

                equippedTabButtonText.color = gray;
                buyWeaponTabButtonText.color = gray;
                upgradePlayerTabButtonText.color = red;
                break;

            default:
                break;
        }
    }
    #endregion

    #region Equip Weapons
    public void SelectEquippedWeapon(int index)
    {
        selectedEquipWeaponIndex = index;
        UpdateEquippedView();
    }

    public void UpdateEquippedView()
    {   
        Player player = Player.GetInstance();
        WeaponData selectedEquipWeapon = player.GetActiveWeaponAtIndex(selectedEquipWeaponIndex);
        
        if (selectedEquipWeapon != null)
        {
            UpgradeDescription upgradeDescription = selectedEquipWeapon.GetNextUpgradeDescription();
            
            selectedEquipWeaponText.text = selectedEquipWeapon.name;
            weaponUpgradeDescriptionText.text = "Upgrade Description:\n";
            foreach(KeyValuePair<string, string> property in  upgradeDescription.properties ){
                weaponUpgradeDescriptionText.text += property.Key +": " + property.Value + "\n";
            }
            
            if(upgradeDescription.cost == -1) 
            {
                upgradeEquipWeaponPriceText.text = "MAX";
            }
            else 
            {
                upgradeEquipWeaponPriceText.text = ((int)upgradeDescription.cost).ToString();
                
            }

            sellWeaponCostText.text = selectedEquipWeapon.GetSellWeaponCost().ToString();
        }
        else
        {
            selectedEquipWeaponText.text = "Empty Slot";
            upgradeEquipWeaponPriceText.text = "-";
            sellWeaponCostText.text = "-";
        }

        selectEquipWeapon1ButtonText.color = selectedEquipWeaponIndex == 0 ? green : gray;
        selectEquipWeapon2ButtonText.color = selectedEquipWeaponIndex == 1 ? green : gray;
    }

    public void BuyUpgradeForSelectedEquipWeapon()
    {
        Player player = Player.GetInstance();
        WeaponData selectedEquipWeapon = player.GetActiveWeaponAtIndex(selectedEquipWeaponIndex);
        UpgradeDescription upgradeDescription = selectedEquipWeapon.GetNextUpgradeDescription();
        if (selectedEquipWeapon != null)
        {
            shop.BuyNextUpgradeForWeapon(selectedEquipWeapon);
            //player.dnaAmount -= upgradeDescription.cost;
            UpdateEquippedView();
        }
        else 
        {
            Debug.LogWarning("BuyUpgradeForSelectedEquipWeapon: No selected equip weapon");
        }
    }

    public void SellSelectedEquipWeapon()
    {
        Player player = Player.GetInstance();
        WeaponData selectedEquipWeapon = player.GetActiveWeaponAtIndex(selectedEquipWeaponIndex);
        UpgradeDescription upgradeDescription = selectedEquipWeapon.GetNextUpgradeDescription();
        if (selectedEquipWeapon != null)
        {
            shop.SellWeapon(selectedEquipWeapon);
            //player.dnaAmount += upgradeDescription.cost*(float)0.7;
            UpdateEquippedView();
        }
        else
        {
            Debug.LogWarning("SellSelectedEquipWeapon: No selected equip weapon");
        }
        
    }

    #endregion

    #region Buy Weapons Tab
    public void UpdateWeaponsScrollView()
    {
        foreach (Transform child in weaponsScrollViewContentPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        weapons = shop.WeaponsForSale();

        weaponButtons.Clear();
        foreach (WeaponData weapon in weapons)
        {
            GameObject weaponButton = GameObject.Instantiate(weaponButtonPrefab, Vector3.zero, Quaternion.identity, weaponsScrollViewContentPanel.transform);

            weaponButton.GetComponentInChildren<Text>().text = weapon.name;
            weaponButton.GetComponent<Image>().sprite = weapon.weaponSprite;
            weaponButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectWeaponData(weapon);
            });

            weaponButtons.Add(weaponButton);
            SelectWeaponData(weapons[0]);
        }
    }

    public void SelectWeaponData(WeaponData weapon)
    {
        selectedWeaponToBuy = weapon;
        UpdateBuyView();
    }

    public void BuyWeapon()
    {
        if (selectedWeaponToBuy == null)
        {
            Debug.LogWarning("BuyWeapon: NO WEAPON SELECTED");
            return;
        }

        shop.PurchaseWeapon(selectedWeaponToBuy);
        UpdateBuyView();
    }

    private void UpdateBuyView() 
    {

        Player player = Player.GetInstance();        
        bool isOwned = player.OwnsWeapon(selectedWeaponToBuy);
        buyWeaponNameText.text = selectedWeaponToBuy.name;
        buyWeaponNameText.color = isOwned ? red : green;
        buyWeaponPriceText.color = isOwned ? red : green;
        buyWeaponPriceText.text = isOwned ? "OWN" : ((int)selectedWeaponToBuy.dnaWorth).ToString();

        //ui
        for (int i = 0; i < weaponButtons.Count; i++)
        {
            WeaponData thisWeapon = weapons[i];
            Image weaponButtonImage = weaponButtons[i].GetComponent<Image>();
            Text weaponButtonText = weaponButtons[i].GetComponentInChildren<Text>();

            bool isSelected = thisWeapon == selectedWeaponToBuy;
            weaponButtonImage.color = isSelected ? (isOwned ? red : green) : white;
            weaponButtonText.color = isSelected ? white : black;
        }
    }


    #endregion

    #region Colors 
    private void InitializeColors() 
    {
        ColorUtility.TryParseHtmlString("#42DF48", out green);
        ColorUtility.TryParseHtmlString("#717171", out gray);
        ColorUtility.TryParseHtmlString("#F24545", out red);
        ColorUtility.TryParseHtmlString("#FFB802", out yellow);
        ColorUtility.TryParseHtmlString("#7EA2FF", out blue);
        ColorUtility.TryParseHtmlString("#FFFFFF", out white);
        ColorUtility.TryParseHtmlString("#000000", out black);
    }

    #endregion
}