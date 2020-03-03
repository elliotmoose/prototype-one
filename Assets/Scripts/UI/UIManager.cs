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

    //UI 
    Image waveManagerImage;
    Image healthBarImage;
    Text scoreText;

    public GameObject healthBarObject;
    public GameObject waveBarObject;
    public GameObject scoreTextObject;
    
    // Start is called before the first frame update
    void Start()
    {
        waveManager = WaveManager.GetInstance();  
        score = ScoreManager.GetInstance();
        player = Player.GetInstance();

        waveManagerImage = waveBarObject.GetComponent<Image>();
        healthBarImage = healthBarObject.GetComponent<Image>();
        scoreText = scoreTextObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float wavePercentage = waveManager.GetWavePercentageHealth();
        waveManagerImage.fillAmount = wavePercentage;

        float scoreNumber = score.GetScore();
        scoreText.text = "SCORE: " + scoreNumber;

        float healthPercentage = player.GetCurHealth()/player.GetMaxHealth();
        healthBarImage.fillAmount = healthPercentage;
    }
}
