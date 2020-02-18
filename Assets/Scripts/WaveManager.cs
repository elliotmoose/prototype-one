using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    int waveLevel = 1;
    int remaindingSpawnQuota = 10;
    float spawnRate = 5f;//spawn 2 per second
    float timeSinceLastSpawn = 0; 
    float spawnDistance = 10; //distance from specified centre (player position)



    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(remaindingSpawnQuota > 0) {
            timeSinceLastSpawn += Time.deltaTime;
            
            if(timeSinceLastSpawn >= 1/spawnRate) {
                timeSinceLastSpawn = 0;
                remaindingSpawnQuota -= 1;
                SpawnEnemy();
            }
        }
    }

    void StartSpawnWave() {
        remaindingSpawnQuota = 10;
    }

    void SpawnEnemy() {
        float spawnHeight = 0;
        float spawnAngle = Random.Range(0, 359); //randomize the angle in which enemy is spawned
        Vector3 spawnReferenceCentre = GameObject.Find("Player").transform.position; 
        Quaternion spawnDirection = Quaternion.AngleAxis(spawnAngle, Vector3.up); 
        Vector3 spawnPosition = spawnReferenceCentre + spawnDirection * Vector3.forward.normalized * spawnDistance;
        GameObject.Instantiate(enemyPrefab, new Vector3(spawnPosition.x,spawnHeight,spawnPosition.z), Quaternion.identity);
    }

    //If all enemies are dead, we can start next round
    void CheckEnemiesDead() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if(enemies.Length == 0) {
            //all enemies dead
        }
    }
}
