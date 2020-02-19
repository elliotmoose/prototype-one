using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAlive = true;
    public float worth = 20f; //worth in dna

    public GameObject dnaToDrop;

    void Start()
    {
        // StartCoroutine(TestDie());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die() {
        DropDna();

        isAlive = false;
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
