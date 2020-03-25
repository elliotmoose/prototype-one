using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager GetInstance() 
    {
        GameObject uiObject = GameObject.Find("UI");
        if(uiObject == null) 
        {
            Debug.LogError("UI (Object) has not been instantiated yet");
            return null;
        }

        UIManager uiManager = uiObject.GetComponent<UIManager>();

        return uiManager;
    }
    //object references 
    WaveManager waveManager;
    ScoreManager score;
    Player player;
    Shop shop;

    //UI 
    Image waveManagerImage; //was 6555F8/54D796
    Image healthBarImage;
    Image infectionBarImage;
    Text scoreText;
    Text dnaText;

    public GameObject healthBarObject;
    public GameObject waveBarObject;
    public GameObject infectionBarObject;
    public GameObject scoreTextObject;
    public GameObject dnaTextObject;
    public GameObject waveProgressDisplayed;
    public Text waveCompleteText;
    public Text waveCompleteText2;

    private bool canFade;
    private Color alphaColor;
    private float timeToFade = 1.0f;

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
        infectionBarImage = infectionBarObject.GetComponent<Image>();
        scoreText = scoreTextObject.GetComponent<Text>();
        dnaText = dnaTextObject.GetComponent<Text>();


    }

    float maxDisplayTime = 3f;
    float fadeTime = 0.7f;


    // Update is called once per frame
    void Update()
    {
        float wavePercentage = waveManager.GetWavePercentageHealth();
        waveManagerImage.fillAmount = wavePercentage;
        
        float infectionPercentage = waveManager.GetInfectionPercentage();
        infectionBarImage.fillAmount = infectionPercentage;

        float scoreNumber = score.GetScore();
        scoreText.text = "SCORE: " + scoreNumber;
        
        float dnaNumber = player.dnaAmount;
        dnaText.text = "Dna: " + dnaNumber;

        float healthPercentage = player.GetCurHealth()/player.GetMaxHealth();
        healthBarImage.fillAmount = healthPercentage;
        
    }
    
    public void ShowWaveEnded(int waveNumber) {
        waveCompleteText.text = $"Wave {waveNumber} Complete";
        waveCompleteText2.text = $"Wave {waveNumber} Complete";
        waveProgressDisplayed.SetActive(true);

        foreach(Transform child in waveProgressDisplayed.transform) 
        {
            StartCoroutine(fadeInAndOut(child.gameObject, true, fadeTime));
            StartCoroutine(checkCloseMenu(child.gameObject, false, fadeTime));
        }
    }

    IEnumerator checkCloseMenu(GameObject objectToFade, bool fadeIn, float duration) {
        yield return new WaitForSeconds(maxDisplayTime);
        StartCoroutine(fadeInAndOut(objectToFade, false, duration));
        yield return new WaitForSeconds(duration);
        waveProgressDisplayed.SetActive(false);
    }

    public void ToggleShopDisplayed()
    {        
        _shopDisplayed = !_shopDisplayed;
        shopMenu.SetActive(_shopDisplayed);
        shopButton.GetComponentInChildren<Text>().text = _shopDisplayed ? "Close\nShop" : "Open\nShop";
        Time.timeScale = _shopDisplayed ? 0 : 1;
    }

    public IEnumerator FadeOut(SpriteRenderer MyRenderer, float duration)
    {
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            Debug.Log(alpha);

            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
    }

    public IEnumerator FadeIn(SpriteRenderer MyRenderer, float duration)
    {
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(0, 1, counter / duration);
            Debug.Log(alpha);

            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
    }

    IEnumerator fadeInAndOut(GameObject objectToFade, bool fadeIn, float duration)
    {
        Debug.Log("Fading");
        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0;
            b = 1;
        }
        else
        {
            a = 1;
            b = 0;
        }

        int mode = 0;
        Color currentColor = Color.clear;

        SpriteRenderer tempSPRenderer = objectToFade.GetComponent<SpriteRenderer>();
        Image tempImage = objectToFade.GetComponent<Image>();
        RawImage tempRawImage = objectToFade.GetComponent<RawImage>();
        MeshRenderer tempRenderer = objectToFade.GetComponent<MeshRenderer>();
        Text tempText = objectToFade.GetComponent<Text>();

        //Check if this is a Sprite
        if (tempSPRenderer != null)
        {
            currentColor = tempSPRenderer.color;
            mode = 0;
        }
        //Check if Image
        else if (tempImage != null)
        {
            currentColor = tempImage.color;
            mode = 1;
        }
        //Check if RawImage
        else if (tempRawImage != null)
        {
            currentColor = tempRawImage.color;
            mode = 2;
        }
        //Check if Text 
        else if (tempText != null)
        {
            currentColor = tempText.color;
            mode = 3;
        }

        //Check if 3D Object
        else if (tempRenderer != null)
        {
            currentColor = tempRenderer.material.color;
            mode = 4;

            //ENABLE FADE Mode on the material if not done already
            tempRenderer.material.SetFloat("_Mode", 2);
            tempRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            tempRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            tempRenderer.material.SetInt("_ZWrite", 0);
            tempRenderer.material.DisableKeyword("_ALPHATEST_ON");
            tempRenderer.material.EnableKeyword("_ALPHABLEND_ON");
            tempRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            tempRenderer.material.renderQueue = 3000;
        }
        else
        {
            yield break;
        }

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            switch (mode)
            {
                case 0:
                    tempSPRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                    break;
                case 1:
                    tempImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                    break;
                case 2:
                    tempRawImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                    break;
                case 3:
                    tempText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                    break;
                case 4:
                    tempRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                    break;
            }
            yield return null;
        }
    }
}
