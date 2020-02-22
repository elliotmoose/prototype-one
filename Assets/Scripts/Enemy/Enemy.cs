using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAlive = true;
    public float dnaWorth = 20f; //worth in dna
    public float scoreWorth = 20f; //worth in score

    public GameObject dnaToDrop;
    public Vector3 playerPosition;
    public Vector3 enemyPosition;

    void Start()
    {
        //StartCoroutine(TestDie());
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.Find("Player").transform.position;
        enemyPosition = GameObject.Find("Enemy").transform.position;

        if (playerPosition == enemyPosition){
            Attack();
        }
    }

    void Attack(){
        
    }

    void Die() {
        isAlive = false;
        DropDna();
        ScoreManager.GetInstance().OnEnemyDied(this);
        WaveManager.GetInstance().OnEnemyDied(this);
        GameObject.Destroy(gameObject);
    }

    void DropDna() {
        GameObject.Instantiate(dnaToDrop, this.transform.position, Quaternion.identity);
    }

    #region Enemy
    IEnumerator TestDie() {
        yield return new WaitForSeconds(3);
        Die();
    }
    #endregion
}
