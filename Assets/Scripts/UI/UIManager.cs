using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //object references 
    WaveManager waveManager;
    ScoreManager score;
    Player player;
    Shop shop;

    //UI 
    Image waveManagerImage;
    Image healthBarImage;
    Text scoreText;
    Text dnaText;

    public GameObject healthBarObject;
    public GameObject waveBarObject;
    public GameObject scoreTextObject;
    public GameObject dnaTextObject;

    //shop
    public GameObject shopButton;
    public GameObject shopMenu;
    private bool _shopDisplayed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        waveManager = WaveManager.GetInstance();  
        score = ScoreManager.GetInstance();
        player = Player.GetInstance();
        shop = Shop.GetInstance();

        waveManagerImage = waveBarObject.GetComponent<Image>();
        healthBarImage = healthBarObject.GetComponent<Image>();
        scoreText = scoreTextObject.GetComponent<Text>();
        dnaText = dnaTextObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float wavePercentage = waveManager.GetWavePercentageHealth();
        waveManagerImage.fillAmount = wavePercentage;

        float scoreNumber = score.GetScore();
        scoreText.text = "SCORE: " + scoreNumber;
        
        float dnaNumber = player.dnaAmount;
        dnaText.text = "Dna: " + dnaNumber;

        float healthPercentage = player.GetCurHealth()/player.GetMaxHealth();
        healthBarImage.fillAmount = healthPercentage;
        
        bool isOpen = shop.IsShopOpen();
        shopButton.GetComponent<Button>().interactable = isOpen;
    }

    public void ToggleShopDisplayed()
    {        
        _shopDisplayed = !_shopDisplayed;
        shopMenu.SetActive(_shopDisplayed);
        shopButton.GetComponentInChildren<Text>().text = _shopDisplayed ? "Close\nShop" : "Open\nShop";
        Time.timeScale = _shopDisplayed ? 0 : 1;
    }
}
