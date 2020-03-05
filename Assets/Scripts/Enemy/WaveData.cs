using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData
{
    public List<EnemyGroupData> enemyGroups = new List<EnemyGroupData>();
    
    public WaveData(int level) 
    {
        float baseEnemyMovementSpeed = 5f;
        float baseEnemyHealth = 100;
        switch(level)
        {
            case 1:
                AddEnemyGroup(EnemyType.BACTERIA, 7, WeaponType.MELEE, baseEnemyHealth, baseEnemyMovementSpeed);                
                break;
            
            case 2:
                AddEnemyGroup(EnemyType.BACTERIA, 10, WeaponType.MELEE, baseEnemyHealth, baseEnemyMovementSpeed);
                AddEnemyGroup(EnemyType.BACTERIA, 4, WeaponType.TOXIN, baseEnemyHealth, baseEnemyMovementSpeed);                
                break;
            
            case 3:
                AddEnemyGroup(EnemyType.BACTERIA, 14, WeaponType.MELEE, baseEnemyHealth, baseEnemyMovementSpeed);
                AddEnemyGroup(EnemyType.BACTERIA, 7, WeaponType.TOXIN, baseEnemyHealth, baseEnemyMovementSpeed);                
                break;
            
            case 4:
                AddEnemyGroup(EnemyType.BACTERIA, 18, WeaponType.MELEE, baseEnemyHealth, baseEnemyMovementSpeed);
                AddEnemyGroup(EnemyType.BACTERIA, 12, WeaponType.TOXIN, baseEnemyHealth, baseEnemyMovementSpeed);                
                break;

            default:
                AddEnemyGroup(EnemyType.BACTERIA, level*4, WeaponType.MELEE, baseEnemyHealth, baseEnemyMovementSpeed);
                AddEnemyGroup(EnemyType.BACTERIA, level * 5, WeaponType.TOXIN, baseEnemyHealth, baseEnemyMovementSpeed);
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