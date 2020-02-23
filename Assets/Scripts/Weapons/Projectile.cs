using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col){
    	if(col.gameObject.tag == "enemy"){
    		 Debug.Log("Hit enemy");
	    	Destroy(gameObject);    		
    	}
    	else if(col.gameObject.tag == "enemy"){
    		 Debug.Log("Hit enemy");
	    	Destroy(gameObject);    		
    	}
    	// Debug.Log("Bullet gone due to hitting: " + col.gameObject.tag);
    }
    void OnBecameInvisible() {
         Destroy(gameObject);
    }
}
