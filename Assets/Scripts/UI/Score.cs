using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    ScoreManager score;
    Text textScore;
    
    // Start is called before the first frame update
    void Start()
    {
        score = ScoreManager.GetInstance();        
        textScore = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float scoreNumber = score.GetScore();
        textScore.text = "SCORE: " + scoreNumber;
    }
}
