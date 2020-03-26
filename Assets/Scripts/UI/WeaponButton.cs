using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public Image weaponImage;
    public Text weaponTitle;
    public Text weaponSubTitle;

    public void SetSelected(bool selected) 
    {

    }

    public void SetData(WeaponData weaponData, bool owned, bool selected) 
    {
        this.weaponImage.sprite = weaponData.weaponSprite;
        this.weaponTitle.text = weaponData.name;
        this.weaponSubTitle.text = owned ? "OWNED" : "FOR SALE";

        this.GetComponent<Image>().color = selected ? Colors.green : Colors.white;
        this.weaponTitle.color = selected ? Colors.white : Colors.darkgray; 
        this.weaponSubTitle.color = selected ? Colors.darkgray : (owned ? Colors.red : Colors.green); 
    }
}
