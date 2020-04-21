using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager singleton = null;
    public static UIManager GetInstance() 
    {   
        if(singleton == null) {
            GameObject uiObject = GameObject.Find("UI");
            if(uiObject == null) 
            {
                Debug.LogError("UI (Object) has not been instantiated yet");
                return null;
            }

            singleton = uiObject.GetComponent<UIManager>();
        }

        return singleton;
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
    public Text dnaText;

    public GameObject healthBarObject;
    public GameObject waveBarObject;
    public GameObject infectionBarObject;
    public GameObject scoreTextObject;
    public GameObject waveProgressDisplayed;
    public Text waveNumberText;
    public Text waveCompleteText;
    public Text waveCompleteText2;
    public Text scoreTextGameOver;
    public Text hiScoreTextGameOver;
    public Toggle staticJoystickToggle;
    public Image damageOverlayImage;
    public Animator dnaUIAnimator;

    
    private bool canFade;
    private Color alphaColor;
    private float timeToFade = 1.0f;

    //shop
    public GameObject shopButton;
    public GameObject shopMenu;
    private bool _shopDisplayed = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        waveManager = WaveManager.GetInstance();  
        score = ScoreManager.GetInstance();
        player = Player.GetInstance();
        shop = Shop.GetInstance();

        waveManagerImage = waveBarObject.GetComponent<Image>();
        healthBarImage = healthBarObject.GetComponent<Image>();
        infectionBarImage = infectionBarObject.GetComponent<Image>();
        scoreText = scoreTextObject.GetComponent<Text>();
        //scoreTextGameOver = gameOverScreen.GetComponent<Text>();
        

        SubscribeToWaveEvents();
    }

    float maxDisplayTime = 3f;
    float fadeTime = 0.7f;

    IEnumerator BlinkInfectionBar() 
    {
        float blinkInterval = 0.1f;
        while(true) 
        {
            infectionBarImage.color = Colors.red;
            yield return new WaitForSeconds(blinkInterval);
            infectionBarImage.color = Colors.yellow;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    Coroutine blinkInfectionBarCoroutine;
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

        scoreTextGameOver.text = "SCORE: " + scoreNumber;

        float hiScoreNumber = score.GetHiScore();
        hiScoreTextGameOver.text = "HI-SCORE: " + hiScoreNumber;
        
    }

    private void UpdateWaveNumber(int waveNumber) 
    {
        waveNumberText.text = $"Wave: {waveNumber}";
    }
    
    private void ShowWaveEnded(int waveNumber) {
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
        shopButton.GetComponentInChildren<Text>().text = _shopDisplayed ? "Close Shop" : "Open Shop";
        Time.timeScale = _shopDisplayed ? 0 : 1;
    }

    public void SetStaticJoystickToggle(bool isStatic) 
    {
        staticJoystickToggle.isOn = isStatic;
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


    #region WAVE EVENTS

    private void SubscribeToWaveEvents() 
    {
        WaveManager waveManager = WaveManager.GetInstance();
        waveManager.onInfected += OnInfected;
        waveManager.onReachingInfected += OnReachingInfected;
        waveManager.onWaveBegin += OnWaveBegin;
        waveManager.onWaveEnd += OnWaveEnd;
    }

    private void OnWaveBegin()
    {
        int waveLevel = WaveManager.GetInstance().GetWaveLevel();
        UpdateWaveNumber(waveLevel);
    }

    private void OnWaveEnd()
    {
        //stop bliknking if needed
        if(blinkInfectionBarCoroutine != null) 
        {
            StopCoroutine(blinkInfectionBarCoroutine);
            blinkInfectionBarCoroutine = null;
        }                    

        infectionBarImage.color = Colors.yellow;
        
        int waveLevel = WaveManager.GetInstance().GetWaveLevel();
        ShowWaveEnded(waveLevel);        
    }

    private void OnInfected()
    {        
        infectionBarImage.color = Colors.red;

        //stop bliknking if needed
        if(blinkInfectionBarCoroutine != null) 
        {
            StopCoroutine(blinkInfectionBarCoroutine);
            blinkInfectionBarCoroutine = null;
        }                    
    }

    private void OnReachingInfected()
    {   
        //start blinking     
        if(blinkInfectionBarCoroutine == null) 
        {
            blinkInfectionBarCoroutine = StartCoroutine(BlinkInfectionBar());
        }
    }

    #endregion

    public void OnDnaPickedUp() {
        dnaUIAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        dnaUIAnimator.Play("DnaOnCollectAnimation");
    }

    private Coroutine _damageOverlayFadeCoroutine;
    public void OnPlayerTakeDamage(float remainderHealthPercentage) {
        if(_damageOverlayFadeCoroutine != null)
        {
            // StopCoroutine(_damageOverlayFadeCoroutine);
            return;
        }

        _damageOverlayFadeCoroutine = StartCoroutine(AnimateDamageOverlay(1-remainderHealthPercentage));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="severity"> severity of 0-1</param>
    /// <returns></returns>
    private IEnumerator AnimateDamageOverlay(float severity) 
    {
        float fadeDuration = 0.45f;
        float curDuration = 0f;
		float quadraticSeverity = 1-Mathf.Pow(severity-1, 2);
        float startingOpactiy = 0.8f * quadraticSeverity;

        while (curDuration < fadeDuration)
        {
            curDuration += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(startingOpactiy, 0, curDuration / fadeDuration);
            damageOverlayImage.color = new Color(damageOverlayImage.color.r, damageOverlayImage.color.g, damageOverlayImage.color.b, alpha);
            _damageOverlayFadeCoroutine = null;
            yield return null;
        }
    }
}
