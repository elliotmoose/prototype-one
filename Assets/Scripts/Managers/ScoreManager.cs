using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private float _score = 0;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnemyDied(Enemy enemy) 
    {
        _score += enemy.scoreWorth;
    }

    public float GetScore()
    {
        return _score;
    }
}
