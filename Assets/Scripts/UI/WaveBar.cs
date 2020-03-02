using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBar : MonoBehaviour
{
    WaveManager waveManager;
    Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        waveManager = WaveManager.GetInstance();        
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float wavePercentage = waveManager.GetWavePercentageHealth();
        image.fillAmount = wavePercentage;
    }
}
