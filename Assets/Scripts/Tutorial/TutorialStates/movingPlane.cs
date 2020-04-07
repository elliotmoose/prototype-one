using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movingPlane: MonoBehaviour{
	 void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "Player" || col.gameObject.tag == "projectile")
		{
			Destroy(this.gameObject);
		}
   }
}