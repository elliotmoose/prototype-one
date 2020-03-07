using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Zone : MonoBehaviour {
	public bool instantiated = false;
	public string name;
	public float duration;
	public bool active;
	// public GameObject zoneGameObject;


	public GameObject player;

	void Start(){
		active = true;
		player = GameObject.Find("Player");
	}

	void Update(){
		UpdateDurarion();
		if(duration < 0){
			active = false;
		}
	}

	void UpdateDurarion() 
	{
		duration -= Time.deltaTime;     
	}

	void OnTriggerEnter(Collider target){	
		if(target.gameObject.name == "Player"){
			OnEnterZone();
		}
	}

	void OnTriggerExit(Collider target){
		if(target.gameObject.name == "Player"){
			OnExitZone();
		}
	}

	public abstract void OnEnterZone();
	public abstract void OnExitZone();

}