using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAlive = true;

    void Start()
    {
        // StartCoroutine(TestDie());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die() {
        isAlive = false;
        WaveManager.GetInstance().OnEnemyDied(this);
        GameObject.Destroy(gameObject);
    }

    #region 
    IEnumerator TestDie() {
        yield return new WaitForSeconds(3);
        Die();
    }
    #endregion
}
