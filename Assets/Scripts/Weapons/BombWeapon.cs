using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : Weapon
{
    public GameObject bomb;
    public Transform bombSpawnPoint;

    protected override void Fire()
    {
        // Transform newPos = GetTargetLocation()
        // BallisticVelocity(newPos, 30f)
        GameObject bombObj = GameObject.Instantiate(bomb, bombSpawnPoint.transform.position, bombSpawnPoint.transform.rotation);

        Projectile bombScript = bombObj.GetComponent<Projectile>();
        
    }

    private void BallisticVelocity(Transform newPos, float angle)
    {
        float heightDiff = newPos.position.y;
        float distance = bombSpawnPoint.position.x - newPos.position.x;
        float a = angle * Mathf.Deg2Rad;
        // havent finished

    }

}
