using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private float _score = 0;
    private float _hiScore = 0;
    
    void Awake()
    {
        LoadHighscore();
    }

    public static ScoreManager GetInstance()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager == null)
        {
            Debug.LogError("GameManager has not been instantiated yet");
            return null;
        }

        ScoreManager scoreManager = gameManager.GetComponent<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogError("GameManager has no component ScoreManager");
            return null;
        }

        return scoreManager;
    }

    void LoadHighscore() 
    {
        _hiScore = PlayerPrefs.GetFloat("hiScore");
    }

    public void UpdateHighscoreIfNeeded() 
    {
        if (_score > _hiScore) {
            PlayerPrefs.SetFloat("hiScore", _score);
        }
    }

    public void OnEnemyDied(Enemy enemy) 
    {
        _score += enemy.scoreWorth;
    }

    public float GetScore()
    {
        return _score;
    }

    public float GetHiScore()
    {
        return PlayerPrefs.GetFloat("hiScore");
    }
}
