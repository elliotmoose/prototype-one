using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    // Start is called before the first frame update
    public bool isAlive = true;
    public float dnaWorth = 20f; //worth in dna
    public float scoreWorth = 20f; //worth in score

    public GameObject dnaPrefab;

    private NavMeshAgent _navMeshAgent;
    private NavMeshObstacle _navMeshObstacle;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        //StartCoroutine(TestDie());
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");

        //TODO: Attach enemy weapon and get weapon range
        float weaponRange = 2f;

        if (Vector3.Distance(player.transform.position, this.transform.position) < weaponRange)
        {
            Attack();
        }
        else 
        {
            Chase(player);
        }
    }

    void Chase(GameObject target) 
    {                        
        if(_navMeshAgent.enabled) {
            _navMeshAgent.speed = this.movementSpeed;        
            _navMeshAgent.destination = target.transform.position;
        }
        else {
            StartCoroutine(SetNavMeshAgentEnabled(true));        
        }
    }

    void Attack(){
        StartCoroutine(SetNavMeshAgentEnabled(false));
    }

    public override void Die() 
    {
        isAlive = false;
        DropDna();
        ScoreManager.GetInstance().OnEnemyDied(this);
        WaveManager.GetInstance().OnEnemyDied(this);
        GameObject.Destroy(gameObject);
    }

    void DropDna() 
    {
        GameObject.Instantiate(dnaPrefab, this.transform.position, Quaternion.identity);
    }

    //this has to be staggered so that the enemy won't teleport when the agent is reactivated
    IEnumerator SetNavMeshAgentEnabled(bool enabled) {
        if(_navMeshAgent.enabled != enabled) 
        {
            _navMeshObstacle.enabled = false;
            _navMeshAgent.enabled = false;            
            _navMeshObstacle.enabled = !enabled;
            yield return new WaitForSeconds(0.01f);
            _navMeshAgent.enabled = enabled;
        }
    }

    #region Enemy
    IEnumerator TestDie() 
    {
        yield return new WaitForSeconds(3);
        Die();
    }
    #endregion
}
