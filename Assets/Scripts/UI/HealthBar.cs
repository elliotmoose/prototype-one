using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Player player;
    Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetInstance();        
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercentage = player.GetCurHealth()/player.GetMaxHealth();
        image.fillAmount = healthPercentage;
    }
}
