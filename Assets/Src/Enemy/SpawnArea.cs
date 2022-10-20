using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnArea : MonoBehaviour
{
  public float spawnRadius = 20;
  public List<SpawnableWave> spawnableWaves;
  public List<GameObject> spawnedEntities = new List<GameObject>();
  int currentWave = 0;

  // Start is called before the first frame update
  void Start()
  {

  }

  void Spawn()
  {
    if (currentWave >= spawnableWaves.Count)
    {
      return;
    }
    var wave = spawnableWaves[currentWave];
    foreach (var group in wave.spawnableGroups)
    {
      for (int i = 0; i < group.count; i++)
      {
        Vector3 spawnRootPos = transform.position + Random.insideUnitSphere * spawnRadius;
        NavMesh.SamplePosition(spawnRootPos, out NavMeshHit hit, spawnRadius, NavMesh.AllAreas);
        Vector3 spawnPos = hit.position;
        var go = GameObject.Instantiate(group.prefab, spawnPos, Quaternion.identity);
        go.GetComponent<Enemy>().aggroRange = spawnRadius;
        spawnedEntities.Add(go);
      }
    }
  }

  // Update is called once per frame
  void Update()
  {

    if (spawnedEntities.Count != 0)
    {
      return;
    }
    // keep checking if a player has stepped into spawn radius
    // if so, spawn a new wave
    var colliders = Physics.OverlapSphere(transform.position, spawnRadius);
    foreach (var collider in colliders)
    {
      if (collider.gameObject.CompareTag("Player"))
      {
        Spawn();
        currentWave++;
        return;
      }
    }
  }
}
