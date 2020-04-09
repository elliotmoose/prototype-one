using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void WaveManagerEvent();

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

    // public GameObject enemyPrefab;
    // public GameObject rapidEnemyPrefab;
    
    [SerializeField]
    private float _cameraShakeOnEnemyDied = 0.17f;
    private float _cameraShakeOnBossDied = 0.6f;

    [SerializeField]
    private int _waveLevel = 0;
    [SerializeField]
    bool _isDowntime = true;

    [SerializeField]
    private float _maxDowntime = 3; //time in between waves
    private float _curDowntime = 0;
    
    [SerializeField]
    private float _spawnDistance = 14; //distance from specified centre (player position)
    private float _spawnRate = 2f;//spawn 2 per second
    
    [SerializeField]
    private float _waveMaxHealth = 0; //total enemy max health
    [SerializeField]
    private float _waveCurHealth = 0; //total enemy current health
    
    private float _baseTimeTillInfection = 1; //8
    private float _timeTillInfectionIncrement = 1; //7
    private float _maxTimeTillInfection = 10; //time till infection
    private float _curTimeTillInfection = 0; 
    private bool _infected = false; 
    private float _maxInfectedSpawnInterval = 4.5f;//infection spawns every X seconds if infected 
    private float _curInfectedSpawnInterval = 0;
    
    private WaveData _currentWave;
    private WaveData _infectionWave;
    private float _timeSinceLastSpawn = 0; //staggering of spawn within wave

    public WaveManagerEvent onInfected;
    public WaveManagerEvent onReachingInfected;
    public WaveManagerEvent onWaveBegin;
    public WaveManagerEvent onWaveEnd;

    // Start is called before the first frame update
    void Start()
    {
        StartSpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIsInfected();

        if(_isDowntime) {
            UpdateDowntime();
        }
        else {
            SpawnWaveIfNeeded();
        }

        if(_infected) {
            AddInfectionWaveIfNeeded();
            SpawnInfectionIfNeeded();
        }
    }
    
    #region Wave Spawning
    private void StartSpawnWave()
    {
        if(GameObject.Find("TutorialManager").activeSelf){
            Debug.Log("WaveManager: Tutorial Wave Started");
            _waveLevel += 0;
            _isDowntime = false;
            _currentWave = WaveData.WaveDataForLevel(this._waveLevel);   //new wave for current level     
            _waveMaxHealth = _currentWave.GetMaxHealth();
            _waveCurHealth = _waveMaxHealth;        

            //increment time till infection
            _maxTimeTillInfection = _baseTimeTillInfection + _waveLevel*_timeTillInfectionIncrement;
            _curTimeTillInfection = 0;

            UIManager.GetInstance().UpdateWaveNumber(_waveLevel);
            return;
        }
        Debug.Log("WaveManager: Wave Started");
        _waveLevel += 1;
        _isDowntime = false;
        _currentWave = WaveData.WaveDataForLevel(this._waveLevel);   //new wave for current level     
        _waveMaxHealth = _currentWave.GetMaxHealth();
        _waveCurHealth = _waveMaxHealth;        

        //increment time till infection
        _maxTimeTillInfection = _baseTimeTillInfection + _waveLevel*_timeTillInfectionIncrement;
        _curTimeTillInfection = 0;
        
        OnWaveBegin();
    }

    //checks if there are enemy groups in the queue left to spawn
    private void SpawnWaveIfNeeded()
    {                
        if(_waveLevel > 10) {
            _spawnRate = 5;
        }
        
        if(_waveLevel > 20) {
            _spawnRate = 10;
        }

        _timeSinceLastSpawn += Time.deltaTime;

        if(_currentWave == null) 
        {
            return;
        }

        if(_currentWave.enemyGroups.Count == 0) 
        {
            
            _currentWave = null;
            return;
        }

        if(!_currentWave.IsEmpty()) 
        {
            if (_timeSinceLastSpawn >= 1 / _spawnRate)
            {
                _timeSinceLastSpawn = 0;
                EnemyGroupData nextSpawnGroupData = _currentWave.PopEnemyFromGroup();
                if(nextSpawnGroupData != null)
                {
                    SpawnEnemy(nextSpawnGroupData);
                }
            }
        }
    }

    //Enemy group data to specify the enemy stats
    void SpawnEnemy(EnemyGroupData enemyGroupData) 
    {
        float spawnHeight = 0;
        float spawnAngle = Random.Range(0, 359); //randomize the angle in which enemy is spawned
        Player player = Player.GetInstance();
        if(player == null) 
        {
            Debug.LogWarning("Player is dead");
            return;
        }
        Vector3 spawnReferenceCenter = player.transform.position; 
        Quaternion spawnDirection = Quaternion.AngleAxis(spawnAngle, Vector3.up); 
        Vector3 spawnPosition = spawnReferenceCenter + spawnDirection * Vector3.forward.normalized * _spawnDistance;

        //Get a valid spawn position based on center + direction
        bool clockwise = true;
        int attempts = 0;
        while(!MapManager.IsInMap(spawnPosition))
        {
            spawnAngle += clockwise ? 90 : -90;
            spawnDirection = Quaternion.AngleAxis(spawnAngle, Vector3.up); 
            spawnPosition = spawnReferenceCenter + spawnDirection * Vector3.forward.normalized * _spawnDistance;
            attempts++;

            //after trying half a circle, go other direction instead so that enemies don't keep spawning on one side of the map
            if(attempts == 2) 
            {
                spawnAngle -= 180; 
                clockwise = false;
            }
        }        

        GameObject enemyPrefab = enemyGroupData.GetPrefab();
        GameObject enemyObj = GameObject.Instantiate(enemyPrefab, new Vector3(spawnPosition.x,spawnHeight,spawnPosition.z), Quaternion.identity);    
        enemyObj.GetComponent<Enemy>().LoadFromEnemyData(enemyGroupData); //load movement speed, max health etc        
    }
    #endregion
    
    public void OnEnemyTakeDamage(float damage) 
    {
        if(this._waveCurHealth > 0)
        {
            this._waveCurHealth -= damage;
        }
    }

    public void OnEnemyDied(Enemy enemy) 
    {
        Camera.main.GetComponent<StressReceiver>().InduceStress(enemy.type == EnemyType.BOSS ? _cameraShakeOnBossDied : _cameraShakeOnEnemyDied);
        CheckShouldStartDowntime();
    }

    public float GetWavePercentageHealth()
    {
        if(_waveCurHealth == 0)
        {
            return 0;
        }

        return _waveCurHealth/_waveMaxHealth;
    }

    #region Downtime
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
            if(enemy.isAlive && enemy.type != EnemyType.INFECTION) 
            {
                allEnemiesDead = false;
                break;
            }
        }

        if(allEnemiesDead && !_isDowntime && _currentWave == null) 
        {
            StartDowntime();
        }
    }

    void StartDowntime() 
    {
        _infected = false;
        _curTimeTillInfection = 0;
        _isDowntime = true;
        _curDowntime = 0;                
        Debug.Log($"WaveManager: Downtime Started: {_maxDowntime}s");
        OnWaveEnd();
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
    #endregion


    #region Infection
    void UpdateIsInfected() {
        if(_infected || _isDowntime) {
            return;
        }

        _curTimeTillInfection += Time.deltaTime;
        

          //check start blinking
        if(_maxTimeTillInfection-_curTimeTillInfection < 3) 
        {
            OnReachingInfected();
        }        



        if(_curTimeTillInfection >= _maxTimeTillInfection) {
            _infected = true;
            OnInfected();
        }
    }

    void AddInfectionWaveIfNeeded() {
        if(!_infected) {
            _infectionWave = null;
            return;
        }

        if(_curInfectedSpawnInterval >= _maxInfectedSpawnInterval) {
            //WAITS TILL WAVE ENDS BEFORE SPAWNING INFECTED
            if(_currentWave == null) {
                _infectionWave = WaveData.WaveDataForInfection(_waveLevel);
                _curInfectedSpawnInterval = 0;  
                Debug.Log("INFECTION WAVE IN QUEUE");
            }
        }
        else {
            _curInfectedSpawnInterval += Time.deltaTime;
        }
    }

    //spawns infection till all enemies are dead
    private void SpawnInfectionIfNeeded()
    {                
        if(!_infected) {
            _infectionWave = null;
            return;
        }

        if(_infectionWave == null) {
            return;
        }
        
        //we reuse the time variables because you cannot spawn wave and infection at same time
        _timeSinceLastSpawn += Time.deltaTime;

        if(!_infectionWave.IsEmpty()) {
            if (_timeSinceLastSpawn >= 1 / _spawnRate)
                {
                    _timeSinceLastSpawn = 0;
                    EnemyGroupData nextSpawnGroupData = _infectionWave.PopEnemyFromGroup();
                    if(nextSpawnGroupData != null)
                    {
                        SpawnEnemy(nextSpawnGroupData);
                    }
                }
        }

    }


    public float GetInfectionPercentage() {
        return _curTimeTillInfection/_maxTimeTillInfection;
    }

    public float GetTimeTillInfection() {
        return _maxTimeTillInfection-_curTimeTillInfection;
    }

    #endregion


    #region EVENTS
    private void OnInfected() {
        Debug.Log("INFECTED");
        //make sure infection spawns immediately
        _curInfectedSpawnInterval = _maxInfectedSpawnInterval;


        if(onInfected != null) 
        {
            onInfected();
        }
    }

    private void OnReachingInfected() 
    {
        if(onReachingInfected != null) 
        {
            onReachingInfected();
        }
    }
     
    
    private void OnWaveBegin() {
        if(onWaveBegin != null) 
        {
            onWaveBegin();
        }
    }

    private void OnWaveEnd() {
        if(onWaveEnd != null) 
        {
            onWaveEnd();
        }
    }

    #endregion

    public int GetWaveLevel() 
    {
        return _waveLevel;
    }
}
