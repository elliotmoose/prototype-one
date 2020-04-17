using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData
{
    public List<EnemyGroupData> enemyGroups = new List<EnemyGroupData>();
    
    public static WaveData WaveDataForLevel(int level) {
        return new WaveData(level);
    }

    public static WaveData WaveDataForInfection(int level) {
        WaveData waveData = new WaveData();        
        int baseInfectionCount = 3;//number of infected = multiplier * level + base
        float infectionCountIncrementFactor = 0.5f;
        int infectionCountIncrement = (int)((float) level * infectionCountIncrementFactor);
        float infectionBaseDamage = 1;
        float infectionDamageIncrement = 0.3f;
        float infectionDamage = infectionDamageIncrement*level + infectionBaseDamage;
        float infectionMovementSpeed = 5f;
        float infectionHealth = 25;
        waveData.AddEnemyGroup(EnemyType.INFECTION, infectionCountIncrement+baseInfectionCount, WeaponType.NULL, infectionHealth, infectionMovementSpeed, 0, 0, infectionDamage);
        return waveData;
    }

    private WaveData() {
        
    }

    public float TimeNeededForSpawn(float spawnRate)
    {
        int numberOfEnemies = 0;
        bool isBossWave = false;
        enemyGroups.ForEach((EnemyGroupData enemyGroup)=>{
            numberOfEnemies += enemyGroup.count;

            if(enemyGroup.type == EnemyType.BOSS) 
            {
                isBossWave = true;
            }
        });   

        //-1 indicates boss wave
        return isBossWave ? -1 : numberOfEnemies * (1/spawnRate);
    }

    private WaveData(int level) 
    {
        float baseEnemyMovementSpeed = 3.2f;
        float baseEnemyHealth = 55;
        float healthIncrement = 8;

        float baseEnemyMeleeDamage = 4;
        float meleeDamageIncrement = 0.8f;
        float baseEnemyRangeDamage = 3;
        float rangeDamageIncrement = 0.6f;
        
        float baseDnaWorth = 8;
        float dnaIncrement = 3;

        float baseScoreWorth = 50;
        float scoreIncrement = 5;

        float meleeDamage = baseEnemyMeleeDamage + meleeDamageIncrement*level;
        float rangeDamage = baseEnemyRangeDamage + rangeDamageIncrement*level;
        float dnaWorth = baseDnaWorth + dnaIncrement*level;
        float scoreWorth = baseScoreWorth + scoreIncrement*level;
        float enemyHealth = baseEnemyHealth + healthIncrement*level;         

        //BOSS WAVES: every 10 waves
        if((level+6) % 10 == 0 || (PlayerPrefs.GetInt("hack") == 1)) {

            float bossBaseHealth = 1000;
            float bossHealthIncrement = 300; 
            float bossBaseDamage = 300;
            float bossDamageIncrement = 100;
            float bossNumber = ((level+4)/10);
            float bossDamage = bossBaseDamage + bossDamageIncrement * bossNumber;
            AddEnemyGroup(EnemyType.BOSS, 1, WeaponType.BOSSLASER, bossBaseHealth + bossHealthIncrement * level, 4, 100 * level, 100 * level, bossDamage);
            
            if (PlayerPrefs.GetInt("hack") == 1)
            {
                PlayerPrefs.SetInt("hack", 0);
            }
            return;
        }



        switch(level)
        {
            case 0:
                AddEnemyGroup(EnemyType.VIRUS, 2, WeaponType.MELEE, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, meleeDamage);                
                AddEnemyGroup(EnemyType.BACTERIA, 2, WeaponType.TOXIN, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, rangeDamage);                
                break;

            case 1:                
                // AddEnemyGroup(EnemyType.BACTERIA, 3, WeaponType.TOXIN, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, rangeDamage);                
                AddEnemyGroup(EnemyType.VIRUS, 12, WeaponType.MELEE, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, meleeDamage);                
                break;
            
            case 2:
                AddEnemyGroup(EnemyType.VIRUS, 18, WeaponType.MELEE, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, meleeDamage);
                break;
            
            case 3:
                AddEnemyGroup(EnemyType.VIRUS, 25, WeaponType.MELEE, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, meleeDamage);
                AddEnemyGroup(EnemyType.BACTERIA, 3, WeaponType.TOXIN, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, rangeDamage);                
                break;
            
            case 4:
                AddEnemyGroup(EnemyType.VIRUS, 32, WeaponType.MELEE, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, meleeDamage);
                AddEnemyGroup(EnemyType.BACTERIA, 5, WeaponType.TOXIN, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, rangeDamage);                
                break;
            
            case 5:
                AddEnemyGroup(EnemyType.VIRUS, 40, WeaponType.MELEE, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, meleeDamage);
                AddEnemyGroup(EnemyType.BACTERIA, 7, WeaponType.TOXIN, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, rangeDamage);                
                break;
            
            case 6:
                AddEnemyGroup(EnemyType.VIRUS, 48, WeaponType.MELEE, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, meleeDamage);
                AddEnemyGroup(EnemyType.BACTERIA, 9, WeaponType.TOXIN, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, rangeDamage);                
                break;
            
            case 7:
                AddEnemyGroup(EnemyType.VIRUS, 56, WeaponType.MELEE, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, meleeDamage);
                AddEnemyGroup(EnemyType.BACTERIA, 11, WeaponType.TOXIN, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, rangeDamage);                
                break;

            default:
                AddEnemyGroup(EnemyType.VIRUS, level*8, WeaponType.MELEE, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, meleeDamage);
                AddEnemyGroup(EnemyType.BACTERIA, level * 2, WeaponType.TOXIN, enemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth, rangeDamage);
                break;
        }        
    }

    public float GetMaxHealth() 
    {
        float maxHealth = 0;
        foreach(EnemyGroupData groupData in enemyGroups)
        {
            maxHealth += groupData.health * groupData.count;
        }        
        return maxHealth;
    }

    public void AddEnemyGroup(EnemyType type, int count, WeaponType weaponType, float health, float movementSpeed, float dnaWorth, float scoreWorth, float damage)
    {
        EnemyGroupData groupData = new EnemyGroupData();
        
        groupData.type = type;
        groupData.count = count;
        groupData.weaponType = weaponType;
        groupData.health = health;
        groupData.movementSpeed = movementSpeed;
        groupData.dnaWorth = dnaWorth;
        groupData.scoreWorth = scoreWorth;
        groupData.damage = damage;

        enemyGroups.Add(groupData);
    }

    public bool IsEmpty()
    {
        foreach(EnemyGroupData enemyGroup in enemyGroups)
        {
            if(enemyGroup.count != 0)
            {
                return false;
            }
        }

        return true;
    }

    //reduce enemy count by 1, and check if group is empty and should be removed. 
    //If enemy group is empty, automatically pops from groups
    public EnemyGroupData PopEnemyFromGroup() 
    {
        if(enemyGroups.Count == 0)
        {
            return null;
        }

        //safety check if this group is empty
        if(enemyGroups[0].count == 0)
        {
            enemyGroups.RemoveAt(0);
            return PopEnemyFromGroup();            
        }

        EnemyGroupData enemyGroup = enemyGroups[0];
        enemyGroups[0].count -= 1;
        
        //clean up if 0
        if(enemyGroups[0].count == 0)
        {
            enemyGroups.RemoveAt(0);
        }

        return enemyGroup;
    }
}