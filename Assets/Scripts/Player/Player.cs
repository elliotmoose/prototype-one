using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float dnaAmount = 0;
    public void AddDna(float amount) {
        dnaAmount += amount;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // this.gameobject.GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
