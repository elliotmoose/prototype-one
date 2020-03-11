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

    //buy weapon
    public GameObject weaponsScrollViewContentPanel;
    public GameObject weaponButtonPrefab;
    public Button buyWeaponButton;
    public Text purchaseWeaponNameText;
    public Text purchaseWeaponDescriptionText;
    public Text purchaseWeaponPriceText;

    private List<WeaponData> weapons = new List<WeaponData>();
    private List<GameObject> weaponButtons = new List<GameObject>();

    private Shop shop;

    private WeaponData selectedWeapon;

    void Start() 
    {
        shop = Shop.GetInstance();
        buyWeaponButton.onClick.AddListener(BuyWeapon);
        UpdateWeaponsScrollView();
        SelectTab(0);
    }

    #region Tab Selector 
    public void SelectTab(int index) 
    {
        Color tabSelectedColor;
        Color tabDeselectedColor;
        ColorUtility.TryParseHtmlString("#F24545", out tabSelectedColor);
        ColorUtility.TryParseHtmlString("#717171", out tabDeselectedColor);

        switch(index) 
        {
            //equipped
            case 0:
                equippedTab.SetActive(true);
                buyWeaponTab.SetActive(false);
                upgradePlayerTab.SetActive(false);

                equippedTabButtonText.color = tabSelectedColor;
                buyWeaponTabButtonText.color = tabDeselectedColor;
                upgradePlayerTabButtonText.color = tabDeselectedColor;
                break;
            
            //buy
            case 1:
                equippedTab.SetActive(false);
                buyWeaponTab.SetActive(true);
                upgradePlayerTab.SetActive(false);

                equippedTabButtonText.color = tabDeselectedColor;
                buyWeaponTabButtonText.color = tabSelectedColor;
                upgradePlayerTabButtonText.color = tabDeselectedColor;
                break;
            case 2:
                equippedTab.SetActive(false);
                buyWeaponTab.SetActive(false);
                upgradePlayerTab.SetActive(true);            

                equippedTabButtonText.color = tabDeselectedColor;
                buyWeaponTabButtonText.color = tabDeselectedColor;
                upgradePlayerTabButtonText.color = tabSelectedColor;
                break;

            default: 
                break;
        }
    }
    #endregion

    #region Buy Weapons Tab
    public void UpdateWeaponsScrollView() 
    {
        foreach (Transform child in weaponsScrollViewContentPanel.transform) {
            GameObject.Destroy(child.gameObject);
        }

        weapons = shop.WeaponsForSale();
        
        weaponButtons.Clear();
        foreach(WeaponData weapon in weapons) {
            GameObject weaponButton = GameObject.Instantiate(weaponButtonPrefab, Vector3.zero, Quaternion.identity, weaponsScrollViewContentPanel.transform);

            weaponButton.GetComponentInChildren<Text>().text = weapon.name;
            weaponButton.GetComponent<Button>().onClick.AddListener(()=> {
                SelectWeaponData(weapon);
            });

            weaponButtons.Add(weaponButton);
            SelectWeaponData(weapons[0]);
        }
    }

    public void SelectWeaponData(WeaponData weapon) 
    {
        selectedWeapon = weapon;
        purchaseWeaponNameText.text = weapon.name;
        purchaseWeaponPriceText.text = ((int) weapon.dnaWorth).ToString();

        //ui
        for (int i=0; i < weaponButtons.Count; i++)
        {
            WeaponData thisWeapon = weapons[i];
            Image weaponButtonImage = weaponButtons[i].GetComponent<Image>();
            Text weaponButtonText = weaponButtons[i].GetComponentInChildren<Text>();
            if(thisWeapon == weapon) {
                weaponButtonImage.color = new Color32(66,223,72,255);
                weaponButtonText.color = new Color(1,1,1,1);
            }
            else {
                weaponButtonImage.color = new Color(1,1,1,1);
                weaponButtonText.color = new Color(0,0,0,1);
            }
        }
    }

    public void BuyWeapon() {
        if(selectedWeapon == null) {
            Debug.LogWarning("BuyWeapon: NO WEAPON SELECTED");
            return;
        }

        shop.PurchaseWeapon(selectedWeapon);
    }

    #endregion
}