using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movingPlane: MonoBehaviour{
	GameObject vfxPrefab;
	public int health = 150;
	void OnTriggerEnter(Collider col){
		Debug.Log(col.gameObject.name);
		if(col.gameObject.name == "Player" && this.gameObject.name == "TutorialPlane(Clone)")
		{
			OnDestroyed();
		}

		if(this.gameObject.name == "TutorialTarget(Clone)" && col.gameObject.tag == "projectile")
		{
			OnDestroyed();
		}
		if(this.gameObject.name == "TutorialTargetSwitchState(Clone)" && col.gameObject.tag == "projectile" && col.gameObject.name != "Projectile(Clone)")
		{
			OnDestroyed();
		}
   }

   	void Start(){
   		vfxPrefab = Resources.Load<GameObject>("Prefabs/Enemies/vfx/death_vfx");
   	}
   	void Update(){
   		if(health <= 0){
   			OnDestroyed();
   		}
   	}

   	public void reduceHealth(){
   		health -= 1;
   	}

   	public void OnDestroyed(){
   		GameObject deathVfx = GameObject.Instantiate(vfxPrefab, transform.position, transform.rotation);
        deathVfx.GetComponent<ParticleSystemRenderer>().material = this.gameObject.GetComponent<Renderer>().material;
        var main = deathVfx.GetComponent<ParticleSystem>().main;                    
        GameObject.Destroy(deathVfx, main.startLifetime.constant);
        Destroy(this.gameObject);
   	}
}