using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager GetInstance() 
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if(gameManager == null) 
        {
            Debug.LogError("GameManager has not been instantiated yet");
            return null;
        }

        WaveManager waveManager = gameManager.GetComponent<WaveManager>();

        if(waveManager == null) 
        {
            Debug.LogError("GameManager has no component WaveManager");
            return null;
        }

        return waveManager;
    }

    public GameObject enemyPrefab;

    [SerializeField]
    private int _waveLevel = 0;
    [SerializeField]
    bool _isDowntime = true;

    [SerializeField]
    private float _maxDowntime = 3; //time in between waves
    private float _curDowntime = 0;
    
    [SerializeField]
    private float _spawnDistance = 10; //distance from specified centre (player position)
    [SerializeField]
    private float _spawnRate = 5f;//spawn 2 per second
    
    [SerializeField]
    private int _remaindingSpawnQuota = 10; //number of enemies left to spawn in this wave
    private float _timeSinceLastSpawn = 0; //staggering of spawn within wave


    // Start is called before the first frame update
    void Start()
    {
        StartSpawnWave();
        //test
        // StartCoroutine(TestKillAllEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDowntime) {
            UpdateDowntime();
        }
        else {
            SpawnIfNeeded();
        }
    }

    void UpdateDowntime() 
    {
        if(_curDowntime < _maxDowntime) 
        {
            _curDowntime += Time.deltaTime;
        }
        else 
        {
            //begin next wave
            StartSpawnWave();
        }
    }

    void SpawnIfNeeded() 
    {
        if(_remaindingSpawnQuota > 0) 
        {
            _timeSinceLastSpawn += Time.deltaTime;
            
            if(_timeSinceLastSpawn >= 1/_spawnRate) 
            {
                _timeSinceLastSpawn = 0;
                _remaindingSpawnQuota -= 1;
                SpawnEnemy();
            }
        }
    }

    void StartSpawnWave() 
    {
        Debug.Log("WaveManager: Wave Started");
        _waveLevel += 1;
        _isDowntime = false;
        _remaindingSpawnQuota = 10;
    }

    void SpawnEnemy() 
    {
        float spawnHeight = 0;
        float spawnAngle = Random.Range(0, 359); //randomize the angle in which enemy is spawned
        Vector3 spawnReferenceCentre = GameObject.Find("Player").transform.position; 
        Quaternion spawnDirection = Quaternion.AngleAxis(spawnAngle, Vector3.up); 
        Vector3 spawnPosition = spawnReferenceCentre + spawnDirection * Vector3.forward.normalized * _spawnDistance;
        GameObject enemyObj = GameObject.Instantiate(enemyPrefab, new Vector3(spawnPosition.x,spawnHeight,spawnPosition.z), Quaternion.identity);
        
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        // enemy.level = waveLevel;
    }

    public void OnEnemyDied(Enemy enemy) 
    {
        CheckShouldStartDowntime();
    }

    //If all enemies are dead, we can start next round
    void CheckShouldStartDowntime() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        //all enemies dead, no more to spawn, begin downtime
        bool allEnemiesDead = true;
        foreach (GameObject enemyObj in enemies)
        {
            Enemy enemy = enemyObj.GetComponent<Enemy>();

            //as long as 1 enemy is alive, should not start downtime
            if(enemy.isAlive) 
            {
                allEnemiesDead = false;
                break;
            }
        }

        if(allEnemiesDead && !_isDowntime && _remaindingSpawnQuota == 0) 
        {
            StartDowntime();
        }
    }

    void StartDowntime() 
    {
        _isDowntime = true;
        _curDowntime = 0;        
        Debug.Log($"WaveManager: Downtime Started: {_maxDowntime}s");
    }

    #region Tests
    IEnumerator TestKillAllEnemies() 
    {
        yield return new WaitForSeconds(3);
        Debug.Log("WaveManager: Clearing enemies...");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        //all enemies dead, begin downtime
        foreach(GameObject enemy in enemies) 
        {
            GameObject.Destroy(enemy);
        }        
    }
    #endregion
}
