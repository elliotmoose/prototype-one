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
        int baseInfectionCount = 3;//number of infected = multiplier * level
        waveData.AddEnemyGroup(EnemyType.INFECTION, level+baseInfectionCount, WeaponType.MELEE, 100, 7, 0, 0);
        return waveData;
    }

    private WaveData() {

    }

    private WaveData(int level) 
    {
        float baseEnemyMovementSpeed = 5f;
        float baseEnemyHealth = 100;
        float baseDnaWorth = 10;
        float dnaIncrement = 3;
        float baseScoreWorth = 50;
        float scoreIncrement = 5;


        float dnaWorth = baseDnaWorth + dnaIncrement*level;
        float scoreWorth = baseScoreWorth + scoreIncrement*level;
        switch(level)
        {
            case 1:                
                AddEnemyGroup(EnemyType.VIRUS, 7, WeaponType.MELEE, baseEnemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth);                
                break;
            
            case 2:
                AddEnemyGroup(EnemyType.BACTERIA, 10, WeaponType.MELEE, baseEnemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth);
                AddEnemyGroup(EnemyType.BACTERIA, 4, WeaponType.TOXIN, baseEnemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth);                
                break;
            
            case 3:
                AddEnemyGroup(EnemyType.BACTERIA, 14, WeaponType.MELEE, baseEnemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth);
                AddEnemyGroup(EnemyType.BACTERIA, 7, WeaponType.TOXIN, baseEnemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth);                
                break;
            
            case 4:
                AddEnemyGroup(EnemyType.BACTERIA, 18, WeaponType.MELEE, baseEnemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth);
                AddEnemyGroup(EnemyType.BACTERIA, 12, WeaponType.TOXIN, baseEnemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth);                
                break;

            default:
                AddEnemyGroup(EnemyType.BACTERIA, level*4, WeaponType.MELEE, baseEnemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth);
                AddEnemyGroup(EnemyType.BACTERIA, level * 5, WeaponType.TOXIN, baseEnemyHealth, baseEnemyMovementSpeed, dnaWorth, scoreWorth);
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

    public void AddEnemyGroup(EnemyType type, int count, WeaponType weaponType, float health, float movementSpeed, float dnaWorth, float scoreWorth)
    {
        EnemyGroupData groupData = new EnemyGroupData();
        
        groupData.type = type;
        groupData.count = count;
        groupData.weaponType = weaponType;
        groupData.health = health;
        groupData.movementSpeed = movementSpeed;
        groupData.dnaWorth = dnaWorth;
        groupData.scoreWorth = scoreWorth;

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