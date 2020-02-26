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
                AddEnemyGroup(EnemyType.BACTERIA, 10, "TOXIN", baseEnemyHealth, baseEnemyMovementSpeed);
                AddEnemyGroup(EnemyType.VIRUS, 4, "STANDARD", baseEnemyHealth, baseEnemyMovementSpeed);
                break;
            
            case 2:
                AddEnemyGroup(EnemyType.BACTERIA, 10, "STANDARD", baseEnemyHealth, baseEnemyMovementSpeed);
                AddEnemyGroup(EnemyType.VIRUS, 4, "STANDARD", baseEnemyHealth, baseEnemyMovementSpeed);
                break;

            default:
                AddEnemyGroup(EnemyType.BACTERIA, level * 5, "STANDARD", baseEnemyHealth, baseEnemyMovementSpeed);
                break;
        }
        
    }

    public void AddEnemyGroup(EnemyType type, int count, string weaponId, float health, float movementSpeed)
    {
        EnemyGroupData groupData = new EnemyGroupData();

        groupData.weaponId = weaponId;
        groupData.health = health;
        groupData.movementSpeed = movementSpeed;
        groupData.type = type;
        groupData.count = count;

        enemyGroups.Add(groupData);
    }
}

public class EnemyGroupData 
{
    public EnemyType type;   
    public string weaponId;
    public float health;
    public float movementSpeed;
    public int count;

    //TODO: switch enemy type
    public GameObject GetPrefab()
    {        
        return (GameObject)Resources.Load($"Prefabs/Enemies/{type.ToString()}");
    }
}

public enum EnemyType
{
    BACTERIA,
    VIRUS
}
