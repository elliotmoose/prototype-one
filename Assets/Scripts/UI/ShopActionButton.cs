using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopActionButton : MonoBehaviour
{
    public Text valueText;
    public Text titleText;
    public Image buttonIcon;

    public void SetTitle(string text) 
    {
        titleText.text = text;
    }
    
    public void SetValue(float value) 
    {
        valueText.text = ((int)value).ToString();        
    }
    
    public void SetValue(string value) 
    {
        valueText.text = value;        
    }

    public void SetColor(Color color) 
    {
        valueText.color = color;
        this.GetComponent<Image>().color = color;
    }
}
