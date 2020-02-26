using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData
{
    public List<EnemyGroupData> enemyGroups = new List<EnemyGroupData>();
    
    public WaveData(int level) 
    {
        float baseEnemyMovementSpeed = 3;
        float baseEnemyHealth = 100;
        switch(level)
        {
            case 1:
                AddEnemyGroup(EnemyType.BACTERIA, 10, WeaponType.TOXIN, baseEnemyHealth, baseEnemyMovementSpeed);
                AddEnemyGroup(EnemyType.VIRUS, 4, WeaponType.STANDARD, baseEnemyHealth, baseEnemyMovementSpeed);
                break;
            
            case 2:
                AddEnemyGroup(EnemyType.BACTERIA, 10, WeaponType.STANDARD, baseEnemyHealth, baseEnemyMovementSpeed);
                AddEnemyGroup(EnemyType.VIRUS, 4, WeaponType.STANDARD, baseEnemyHealth, baseEnemyMovementSpeed);
                break;

            default:
                AddEnemyGroup(EnemyType.BACTERIA, level * 5, WeaponType.STANDARD, baseEnemyHealth, baseEnemyMovementSpeed);
                break;
        }
        
    }

    public void AddEnemyGroup(EnemyType type, int count, WeaponType weaponType, float health, float movementSpeed)
    {
        EnemyGroupData groupData = new EnemyGroupData();
        
        groupData.weaponType = weaponType;
        groupData.health = health;
        groupData.movementSpeed = movementSpeed;
        groupData.type = type;
        groupData.count = count;

        enemyGroups.Add(groupData);
    }
}