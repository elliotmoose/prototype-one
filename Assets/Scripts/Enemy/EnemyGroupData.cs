using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupData 
{
    public EnemyType type;   
    public WeaponType weaponType;
    
    public float health;
    public float movementSpeed;
    public float dnaWorth = 0;
    public float scoreWorth = 0;
    public float damage = 0;
    
    public int count;

    //TODO: switch enemy type
    public GameObject GetPrefab()
    {        
        return (GameObject)Resources.Load($"Prefabs/Enemies/{type.ToString()}");
    }
}

