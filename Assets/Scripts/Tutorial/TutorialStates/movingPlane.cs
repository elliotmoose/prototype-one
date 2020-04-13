using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movingPlane: MonoBehaviour{
	 void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "Player" && this.gameObject.name == "TutorialPlane(Clone)")
		{
			Destroy(this.gameObject);
		}

		if(this.gameObject.name == "TutorialTarget(Clone)" && col.gameObject.tag == "projectile")
		{
			Destroy(this.gameObject);
		}
   }
}