using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movingPlane: MonoBehaviour{
	 void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "Player")
		{
			Destroy(this.gameObject);
		}
   }
}